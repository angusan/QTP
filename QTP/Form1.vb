Imports System.Runtime.InteropServices
Public Class Form1

    Private Declare Function SKQuoteLib_Initialize Lib "C:\Capital\SKQuoteLib.dll" (ByVal ID As String, ByVal Password As String) As Integer
    Private Declare Function SKQuoteLib_EnterMonitor Lib "C:\Capital\SKQuoteLib.dll" () As Integer
    Private Declare Function SKQuoteLib_LeaveMonitor Lib "C:\Capital\SKQuoteLib.dll" () As Integer
    Private Declare Function SKQuoteLib_RequestTicks Lib "C:\Capital\SKQuoteLib.dll" (ByRef PageNo As Short, ByVal StockNos As String) As Integer
    Private Declare Function SKQuoteLib_GetTick Lib "C:\Capital\SKQuoteLib.dll" (ByVal MarketNo As Short, ByVal Index As Short, ByVal Ptr As Integer, ByRef tTick As TTick) As Integer

    Private Delegate Sub delegateOnConnection(ByVal Kind%, ByVal Code%)
    Private Declare Function SKQuoteLib_AttachConnectionCallBack Lib "C:\Capital\SKQuoteLib.dll" (ByVal Func As delegateOnConnection) As Integer
    Private ConnectionCB As New delegateOnConnection(AddressOf onConnection)

    Private Delegate Sub delegateOnTicks(ByVal MarketNo As Short, ByVal Stockidx As Short, ByVal Ptr%, ByVal tTime%, ByVal Bid%, ByVal Ask%, ByVal Close%, ByVal Qty%)
    Private Declare Function SKQuoteLib_AttachTicksGetCallBack Lib "C:\Capital\SKQuoteLib.dll" (ByVal Func As delegateOnTicks) As Integer
    Private OnTicksCB As New delegateOnTicks(AddressOf onTick)

    Dim dtTicks As New DataTable
    Dim userId = "", userPwd = ""
    Dim datLastTrading As Date
    Dim isConnected As Boolean = False
    Dim isHistoryMode As Boolean = True

    <StructLayout(LayoutKind.Sequential)>
    Structure TStock
        Friend Stockidx As Short       ' 系統自行定義的股票代碼
        Friend iDecimal As Short       ' 報價小數位數
        Friend TypeNo As Short         ' 類股分類
        Friend MarketNo As Byte        ' 市場代號 0x00 :上市; 0x01 :上櫃; 0x02 :期貨; 0x03 :選擇權 ; 0x04 :興櫃
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=20)>
        Friend StockNo As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=20)>
        Friend StockName As String
        Friend Open As Integer    ' 開盤價
        Friend High As Integer    ' 最高價
        Friend Low As Integer     ' 最低價
        Friend Close As Integer   ' 成交價
        Friend TickQty As Integer ' 單量
        Friend Ref As Integer     ' 昨收、參考價
        Friend Bid As Integer     ' 買價
        Friend Bc As Integer      ' 買量
        Friend Ask As Integer     ' 賣價
        Friend Ac As Integer      ' 賣量
        Friend TBc As Integer     ' 買盤量
        Friend TAc As Integer     ' 賣盤量
        Friend FutureOI As Integer ' 期貨未平倉 OI
        Friend TQty As Integer     ' 總量
        Friend YQty As Integer     ' 昨量
        Friend Up As Integer       ' 漲停
        Friend Down As Integer     ' 跌停
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Structure TTick
        Friend Ptr%
        Friend tTime%
        Friend Bid%
        Friend Ask%
        Friend Close%
        Friend Qty%
    End Structure

    '設定成交明細表
    Private Sub buildTickTable()
        If dtTicks.Columns.Count = 0 Then
            dtTicks.Columns.Add("Time", GetType(Date))
            dtTicks.Columns.Add("Ptr", GetType(Integer))
            dtTicks.Columns.Add("Bid", GetType(Single))
            dtTicks.Columns.Add("Ask", GetType(Single))
            dtTicks.Columns.Add("Close", GetType(Single))
            dtTicks.Columns.Add("Qty", GetType(Integer))
        End If
        dtTicks.Rows.Clear()
    End Sub

    '設定輸出檔名
    Private Sub outputFilesSetting()
        findLastTradingDay()
        tbxTicks.Text = "C:\Capital\Ticks\" & Format(datLastTrading, "yyyyMMdd")

        If Not My.Computer.FileSystem.DirectoryExists(tbxTicks.Text) Then
            My.Computer.FileSystem.CreateDirectory(tbxTicks.Text)
        End If
    End Sub

    '尋找最近一個交易日
    Private Sub findLastTradingDay()
        Dim arrHoliday As New ArrayList
        datLastTrading = Today
        Using rea As New System.IO.StreamReader("C:\Capital\Holidays.txt")
            Do Until rea.EndOfStream
                arrHoliday.Add(CDate(rea.ReadLine))
            Loop
        End Using
        Do While arrHoliday.Contains(datLastTrading)
            datLastTrading = datLastTrading.AddDays(-1)
        Loop
    End Sub

    '連結報價
    Private Sub connectingQuote()
        Dim errorSum = 0
        errorSum = SKQuoteLib_Initialize(userId, userPwd)
        errorSum += SKQuoteLib_AttachConnectionCallBack(ConnectionCB)
        errorSum += SKQuoteLib_AttachTicksGetCallBack(OnTicksCB)
        errorSum += SKQuoteLib_EnterMonitor

        If errorSum <> 0 Then
            MsgBox("登入錯誤")
            Close()
        End If
    End Sub

    '報價連結回傳，在此進行呼叫個股報價
    Private Sub onConnection(ByVal intKind%, ByVal intCode%)
        If intKind = 100 AndAlso intCode = 0 Then
            isConnected = True
            systemWait(500)
            Me.Text = "連線成功"
        Else
            isConnected = False
            Me.Text = "未連線"
        End If
    End Sub

    Private Sub onTick(ByVal MarketNo As Short, ByVal Stockidx As Short, ByVal Ptr%, ByVal tTime%, ByVal Bid%, ByVal Ask%, ByVal Close%, ByVal Qty%)
        Dim tTick As New TTick With {.Ask = Ask, .Bid = Bid, .Close = Close, .Ptr = Ptr, .Qty = Qty, .tTime = tTime}
        If isHistoryMode = True Then
            getHistory(MarketNo, Stockidx, Ptr)
        Else
            addTicks(tTick)
        End If
    End Sub

    '增加成交明細
    Private Sub addTicks(ByVal tTick As TTick)
        Dim tickTime As Date = Today.AddHours(Int(tTick.tTime / 10000)).AddMinutes(Int(tTick.tTime / 100) Mod 100).AddSeconds(tTick.tTime Mod 100)
        dtTicks.Rows.Add(tickTime, tTick.Ptr, tTick.Bid / 100, tTick.Ask / 100, tTick.Close / 100, tTick.Qty)
    End Sub

    '回補本日歷史成交資料
    Private Sub getHistory(ByVal MarketNo As Short, ByVal stockidx As Short, ByVal ptr As Integer)
        Dim tTick As New TTick
        For i% = 0 To ptr
            If SKQuoteLib_GetTick(MarketNo, stockidx, i, tTick) = 0 Then
                Dim dateT As Date = Today.AddHours(Int(tTick.tTime / 10000)).AddMinutes(Int(tTick.tTime / 100) Mod 100).AddSeconds(tTick.tTime Mod 100)
                addTicks(tTick)
            End If
            My.Application.DoEvents()
        Next
        MsgBox("回補完成")
        btnTicks.Enabled = True
    End Sub

    '等待intMS毫秒
    Private Sub systemWait(ByVal intMS%)
        Dim datStart As Date = Now
        Do Until Now.Subtract(datStart).TotalMilliseconds > intMS
            My.Application.DoEvents()
        Loop
        My.Computer.Audio.Stop()
    End Sub

    '顯示表格
    Private Sub showTable(ByVal dt As DataTable)
        Dim fom As New Form
        Dim dgv As New DataGridView
        dgv.DataSource = dt
        dgv.Dock = DockStyle.Fill
        dgv.EditMode = DataGridViewEditMode.EditProgrammatically
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        If dgv.RowCount > 0 Then
            dgv.FirstDisplayedScrollingRowIndex = dgv.RowCount - 1
        End If

        fom.Controls.Add(dgv)
        fom.Show()
        For i% = 0 To dt.Columns.Count - 1
            If dt.Columns(i).DataType Is GetType(Date) Then
                dgv.Columns(i).DefaultCellStyle.Format = "HH:mm:ss"
            End If
        Next
        dgv.AutoResizeColumns()
        fom.Width = dgv.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + 76
    End Sub

    '載入表單，一切開始的地方
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        buildTickTable() '設定成交明細表格
        outputFilesSetting() '設定輸出檔名，先找最後一個交易日
        connectingQuote() '連結報價
        tbxSymbol_TextChanged()
    End Sub

    '顯示明細按鈕
    Private Sub btnShowTicks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowTicks.Click
        showTable(dtTicks)
    End Sub

    '更改檔名
    Private Sub tbxSymbol_TextChanged() Handles tbxSymbol.TextChanged
        tbxTicks.Text = "C:\Capital\Ticks\" & Format(datLastTrading, "yyyyMMdd") & If(tbxSymbol.Text = "", "", "\" & tbxSymbol.Text & ".txt")
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Static datLast As Date = TimeOfDay
        If Not isConnected AndAlso Second(Now) Mod 5 = 0 AndAlso userId <> "" AndAlso userPwd <> "" Then
            Form1_Load(sender, e)
        End If
        If TimeOfDay >= #8:35:00 AM# AndAlso datLast < #8:35:00 AM# Then
            Dim rt% = SKQuoteLib_LeaveMonitor
        End If
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        isHistoryMode = True
        btnTicks.Enabled = False
        dtTicks.Rows.Clear()
        SKQuoteLib_RequestTicks(-1, tbxSymbol.Text)
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        writeTicks()
    End Sub

    Private Sub btnTicks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTicks.Click
        isHistoryMode = False
        dtTicks.Rows.Clear()
        SKQuoteLib_RequestTicks(-1, tbxSymbol.Text)
    End Sub

    '寫出成交明細
    Private Sub writeTicks()
        Using wri As New System.IO.StreamWriter(tbxTicks.Text, False)
            For Each drT As DataRow In dtTicks.Rows
                Dim strW$ = Format(drT.Item(0), "HH:mm:ss")
                For i% = 1 To 4
                    strW &= "," & Format(drT.Item(i), "0.##")
                Next

                wri.WriteLine(strW)
            Next
        End Using
    End Sub
End Class