<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請勿使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnShowTicks = New System.Windows.Forms.Button()
        Me.btnTicks = New System.Windows.Forms.Button()
        Me.tbxSymbol = New System.Windows.Forms.TextBox()
        Me.tbxTicks = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnHistory = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'btnShowTicks
        '
        Me.btnShowTicks.Location = New System.Drawing.Point(416, 2)
        Me.btnShowTicks.Name = "btnShowTicks"
        Me.btnShowTicks.Size = New System.Drawing.Size(107, 24)
        Me.btnShowTicks.TabIndex = 29
        Me.btnShowTicks.Text = "顯示Ticks"
        Me.btnShowTicks.UseVisualStyleBackColor = True
        '
        'btnTicks
        '
        Me.btnTicks.Location = New System.Drawing.Point(166, 3)
        Me.btnTicks.Name = "btnTicks"
        Me.btnTicks.Size = New System.Drawing.Size(119, 23)
        Me.btnTicks.TabIndex = 28
        Me.btnTicks.Text = "即時接收Ticks"
        Me.btnTicks.UseVisualStyleBackColor = True
        '
        'tbxSymbol
        '
        Me.tbxSymbol.Location = New System.Drawing.Point(73, 5)
        Me.tbxSymbol.Name = "tbxSymbol"
        Me.tbxSymbol.Size = New System.Drawing.Size(87, 22)
        Me.tbxSymbol.TabIndex = 27
        Me.tbxSymbol.Text = "TX00"
        '
        'tbxTicks
        '
        Me.tbxTicks.Location = New System.Drawing.Point(73, 35)
        Me.tbxTicks.Name = "tbxTicks"
        Me.tbxTicks.Size = New System.Drawing.Size(337, 22)
        Me.tbxTicks.TabIndex = 26
        Me.tbxTicks.Text = "C:\Capital\Ticks.txt"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "存檔路徑"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "商品代碼"
        '
        'btnHistory
        '
        Me.btnHistory.Location = New System.Drawing.Point(291, 3)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(119, 23)
        Me.btnHistory.TabIndex = 31
        Me.btnHistory.Text = "盤後回補Ticks"
        Me.btnHistory.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(416, 34)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(107, 23)
        Me.btnExport.TabIndex = 32
        Me.btnExport.Text = "匯出檔案"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(539, 73)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnHistory)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnShowTicks)
        Me.Controls.Add(Me.btnTicks)
        Me.Controls.Add(Me.tbxSymbol)
        Me.Controls.Add(Me.tbxTicks)
        Me.Controls.Add(Me.Label5)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As Timer
    Friend WithEvents btnShowTicks As Button
    Friend WithEvents btnTicks As Button
    Friend WithEvents tbxSymbol As TextBox
    Friend WithEvents tbxTicks As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnHistory As Button
    Friend WithEvents btnExport As Button
End Class
