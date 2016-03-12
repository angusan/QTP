using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using System.Windows.Forms.DataVisualization.Charting;
using System.Timers;
using System.Runtime.InteropServices;
using System.Data.SQLite;
using System.Data.Linq;
using QTP.entity;
using QTP.service;

namespace qtp
{
    public partial class Main : Form
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------
        private int         m_nCode;
        private string      m_strLoginID;
        private bool        m_bConnect  = false;
        private bool        m_bTick     = false;

        private DataTable   m_dtStocks; 
        private DataTable   m_dtTick; 
        private DataTable   m_dtBest5Ask; 
        private DataTable   m_dtBest5Bid;
        private TICK        m_Tick;

        DataContext sqliteContext;
        Hashtable symbolNames = new Hashtable();

        private delegate void InvokeSendMessage(string state);
        //private delegate void EnterMonitor();

        public delegate void ReconnectTimerEvent(object source, ElapsedEventArgs args);

        private System.Timers.Timer reconnectTimer = new System.Timers.Timer();

        FOnNotifyConnection     fOnNotifyConnection;
        FOnNotifyQuote          fOnNotifyQuote;
        FOnNotifyTicks          fOnNotifyTicks;
        FOnNotifyBest5          fOnNotifyBest5;
        FOnNotifyServerTime     fOnNotifyServerTime;
        FOnNotifyMarketTot      fOnNotifyMarketTot;
        FOnNotifyMarketBuySell  fOnNotifyMarketBuySell;
        FOnNotifyTicksGet       fOnNotifyTicksGet;
        FOnProductsReady        fOnProductsReady;
        ReconnectTimerEvent     fReconnectTimerEvent;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint RegisterWindowMessage(string lpString);

        IntPtr HWND_BROADCAST = new IntPtr(0xffff);
        uint MSG_SHOW = RegisterWindowMessage("Message");

        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------
        public Main()
        {
            InitializeComponent();
        }

        #endregion

        #region Component Event
        //----------------------------------------------------------------------
        // Component Event
        //----------------------------------------------------------------------
        private void Main_Load(object sender, EventArgs e)
        {
            fOnNotifyConnection = new FOnNotifyConnection(OnNotifyConnection);
            GC.KeepAlive(fOnNotifyConnection);
            GC.SuppressFinalize(fOnNotifyConnection);

            fOnNotifyQuote = new FOnNotifyQuote(OnNotifyQuote);
            GC.KeepAlive(fOnNotifyQuote);
            GC.SuppressFinalize(fOnNotifyQuote);

            fOnNotifyTicks = new FOnNotifyTicks(OnNotifyTicks);
            GC.KeepAlive(fOnNotifyTicks);
            GC.SuppressFinalize(fOnNotifyTicks);

            fOnNotifyBest5 = new FOnNotifyBest5(OnNotifyBest5);
            GC.KeepAlive(fOnNotifyBest5);
            GC.SuppressFinalize(fOnNotifyBest5);

            fOnNotifyServerTime = new FOnNotifyServerTime(OnNotifyServerTime);
            GC.KeepAlive(fOnNotifyServerTime);
            GC.SuppressFinalize(fOnNotifyServerTime);

            //fOnNotifyMarketTot = new FOnNotifyMarketTot(OnNotifyMarketTot);
            //GC.KeepAlive(fOnNotifyMarketTot);
            //GC.SuppressFinalize(fOnNotifyMarketTot);

            //fOnNotifyMarketBuySell = new FOnNotifyMarketBuySell(OnNotifyMarketBuySell);
            //GC.KeepAlive(fOnNotifyMarketBuySell);
            //GC.SuppressFinalize(fOnNotifyMarketBuySell);

            fOnNotifyTicksGet = new FOnNotifyTicksGet(OnNotifyTicksGet);
            GC.KeepAlive(fOnNotifyTicksGet);
            GC.SuppressFinalize(fOnNotifyTicksGet);

            fOnProductsReady = new FOnProductsReady(OnProductsReady);
            GC.KeepAlive(fOnProductsReady);
            GC.SuppressFinalize(fOnProductsReady);

            fReconnectTimerEvent = new ReconnectTimerEvent(reconnectTimerEvent);
            GC.KeepAlive(fReconnectTimerEvent);

            m_dtStocks = CreateStocksDataTable();
            m_dtTick = CreateTickDataTable();
            m_dtBest5Ask = CreateBest5AskTable();
            m_dtBest5Bid = CreateBest5AskTable();

            SetDoubleBuffered(gridStocks);

            sqliteContext = new DataContext(new SQLiteConnection("Data source=localDB.db"));
            initializeTabBooking();

            this.reconnectTimer.Elapsed += new System.Timers.ElapsedEventHandler(reconnectTimerEvent);
            this.reconnectTimer.Interval = 60 * 1000;
            this.reconnectTimer.Enabled = false;
        }

        private void initializeTabBooking()
        {
            List<Code_Mapping> mappings = this.findCodeMapping();
            List<Code_Mapping> regions = (mappings.Where(m => m.name.Equals("FOREIGN"))).ToList();
            ddRegion.DataSource = regions;
            ddRegion.DisplayMember = "description";
            ddRegion.ValueMember = "value";

            List<Code_Mapping> intervals = (mappings.Where(m => m.name.Equals("ANALYZE_INTERVAL"))).ToList();
            ddInterval.DataSource = intervals;
            ddInterval.DisplayMember = "description";
            ddInterval.ValueMember = "value";

            gridBooking.DataSource = this.findBookings();
        }

        private void quoteInitialize()
        {
            string strPassword;

            m_strLoginID = txtAccount.Text.Trim();
            strPassword = txtPassWord.Text.Trim();

            //Initialize SKOrderLib
            m_nCode = Functions.SKQuoteLib_Initialize(m_strLoginID, strPassword);

            if (m_nCode == 0)
            {
                MessageBox.Show("Initialize Success");
                lblMessage.Text = "元件初始化完成";
            }
            else if (m_nCode == 2003)
            {
                lblMessage.Text = "元件已初始過，無須重複執行";
                return;
            }
            else
            { 
                lblMessage.Text = "元件初始化失敗 code " + GetApiCodeDefine(m_nCode);
                return;
            }

            Functions.SKQuoteLib_AttachConnectionCallBack(fOnNotifyConnection);

            Functions.SKQuoteLib_AttachQuoteCallBack(fOnNotifyQuote);

            Functions.SKQuoteLib_AttachTicksCallBack(fOnNotifyTicks);

            Functions.SKQuoteLib_AttachBest5CallBack(fOnNotifyBest5);

            Functions.SKQuoteLib_AttchServerTimeCallBack(fOnNotifyServerTime);

            Functions.SKQuoteLib_AttachMarketTotCallBack(fOnNotifyMarketTot);

            Functions.SKQuoteLib_AttachMarketBuySellCallBack(fOnNotifyMarketBuySell);

            Functions.SKQuoteLib_AttachHistoryTicksGetCallBack(fOnNotifyTicksGet);

            Functions.SKQuoteLib_AttachProductsReadyCallBack(fOnProductsReady);

            this.reconnectTimer.Enabled = true;
        }

        private void btnEnterMonitor_Click(object sender, EventArgs e)
        {
            quoteInitialize();
            m_nCode = Functions.SKQuoteLib_EnterMonitor();

            if (m_nCode == 0)
            {
                lblMessage.Text = "SKQuoteLib_EnterMonitor 呼叫成功";
                m_bConnect = true;
            }
            else
            {
                lblMessage.Text = "呼叫失敗 [CODE] " + GetApiCodeDefine(m_nCode);
            }
        }

        private void btnLeaveMonitor_Click(object sender, EventArgs e)
        {
            m_nCode = Functions.SKQuoteLib_LeaveMonitor();

            if (m_nCode == 0)
            {
                lblConnect.ForeColor = Color.Red;
                lblMessage.Text = "SKQuoteLib_LeaveMonitor 呼叫成功";
                m_bConnect = false;
            }
            else
            {
                lblMessage.Text = "呼叫失敗 [CODE] " + GetApiCodeDefine(m_nCode);
            }
        }

        private void btnQueryStocks_Click(object sender, EventArgs e)
        {
            string  strStocks;
            int     nPage =  1;

            m_dtStocks.Clear();
            gridStocks.ClearSelection();

            gridStocks.DataSource = m_dtStocks;

            gridStocks.Columns["m_sStockidx"].Visible       = false;
            gridStocks.Columns["m_sDecimal"].Visible        = false;
            gridStocks.Columns["m_sTypeNo"].Visible         = false;
            gridStocks.Columns["m_cMarketNo"].Visible       = false;
            gridStocks.Columns["m_caStockNo"].HeaderText    = "代碼";
            gridStocks.Columns["m_caName"].HeaderText       = "名稱";
            gridStocks.Columns["m_nOpen"].HeaderText        = "開盤價";
            gridStocks.Columns["m_nHigh"].Visible           = false;
            gridStocks.Columns["m_nLow"].Visible            = false;
            gridStocks.Columns["m_nClose"].HeaderText       = "成交價";
            gridStocks.Columns["m_nTickQty"].HeaderText     = "單量";
            gridStocks.Columns["m_nRef"].HeaderText         = "昨收價";
            gridStocks.Columns["m_nBid"].HeaderText         = "買進";
            gridStocks.Columns["m_nBc"].Visible             = false;
            gridStocks.Columns["m_nAsk"].HeaderText         = "賣出";
            gridStocks.Columns["m_nAc"].Visible             = false;
            gridStocks.Columns["m_nTBc"].Visible            = false;
            gridStocks.Columns["m_nTAc"].Visible            = false;
            gridStocks.Columns["m_nFutureOI"].Visible       = false;
            gridStocks.Columns["m_nTQty"].Visible           = false;
            gridStocks.Columns["m_nYQty"].Visible           = false;
            gridStocks.Columns["m_nUp"].Visible             = false;
            gridStocks.Columns["m_nDown"].Visible           = false;

            strStocks = txtStocks.Text.Trim();

            string[] Stocks = strStocks.Split(new Char[]{','});

            foreach (string s in Stocks)
            {
                STOCK pSTOCK;

                int nCode = Functions.SKQuoteLib_GetStockByNo(s.Trim(), out pSTOCK);

                if (nCode == 0)
                {
                    OnUpDateDataRow(pSTOCK);
                }
            }

            m_nCode = Functions.SKQuoteLib_RequestStocks(out nPage, strStocks);

            if (m_nCode == 0)
            {
                lblMessage.Text = "SKQuoteLib_RequestStocks 呼叫成功"; 
            }
            else
            {
                lblMessage.Text = "呼叫失敗 [CODE] " + GetApiCodeDefine(m_nCode);
            }
        }

        private void timerServerTime_Tick(object sender, EventArgs e)
        {
            if (m_bConnect == true)
            {
                lblSendRequestServerTime.Text = DateTime.Now.ToString("HH:mm:ss fff");

                m_nCode = Functions.SKQuoteLib_RequestServerTime();
            }
            else
                btnEnterMonitor_Click(null,null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FORMAT05 fServerTime;
            m_nCode = Functions.SKQuoteLib_GetServerTime(out fServerTime);

            if (m_nCode == 0)
            {
                string strTime = string.Format("{0}:{1}:{2}", fServerTime.m_sHour.ToString(), fServerTime.m_sMinute.ToString(), fServerTime.m_sSecond.ToString());

                MessageBox.Show(strTime);
            }

            timerServerTime.Enabled = true;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnLeaveMonitor_Click(null,null);
        }

        public string m_strStock = "";
        private void btnTick_Click(object sender, EventArgs e)
        {
            m_dtTick.Clear();
            m_dtBest5Ask.Clear();
            m_dtBest5Bid.Clear();
            
            m_bTick    = false;

            GridTick.DataSource         = m_dtTick;
            GridBest5Ask.DataSource     = m_dtBest5Ask;
            GridBest5Bid.DataSource     = m_dtBest5Bid;

            GridTick.Columns["m_nPtr"].Visible          = true;
            GridTick.Columns["m_nTime"].HeaderText      = "時間";
            GridTick.Columns["m_nTime"].Width           = 80;
            GridTick.Columns["m_nBid"].HeaderText       = "買價";
            GridTick.Columns["m_nBid"].Width            = 80;
            GridTick.Columns["m_nAsk"].HeaderText       = "賣價";
            GridTick.Columns["m_nAsk"].Width            = 80;
            GridTick.Columns["m_nClose"].HeaderText     = "成交價";
            GridTick.Columns["m_nClose"].Width          = 80;
            GridTick.Columns["m_nQty"].HeaderText       = "量";
            GridTick.Columns["m_nQty"].Width            = 40;


            GridBest5Ask.Columns["m_nAskQty"].HeaderText    = "張數";
            GridBest5Ask.Columns["m_nAskQty"].Width         = 60;
            GridBest5Ask.Columns["m_nAsk"].HeaderText       = "賣價";
            GridBest5Ask.Columns["m_nAsk"].Width            = 60;

            GridBest5Bid.Columns["m_nAskQty"].HeaderText = "張數";
            GridBest5Bid.Columns["m_nAskQty"].Width = 60;
            GridBest5Bid.Columns["m_nAsk"].HeaderText = "買價";
            GridBest5Bid.Columns["m_nAsk"].Width = 60;

            int nPageNo;

            if (m_strStock != "")
            {
                nPageNo = 50;
                m_nCode = Functions.SKQuoteLib_RequestTicks(out nPageNo, m_strStock);
            }

            nPageNo = 1;
            m_nCode = Functions.SKQuoteLib_RequestTicks(out nPageNo, txtTick.Text.Trim());
            m_strStock = txtTick.Text.Trim();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listStockInfo.Items.Clear();

            for (short i = 0; i < 5; i++)
            {
                short nStockidx = 0;

                while (true)
                {
                    STOCK pSTOCK;

                    int nCode = Functions.SKQuoteLib_GetStockByIndex(i, nStockidx, out pSTOCK);

                    if (nCode == 0)
                    {
                        nStockidx++;

                        int nMarketNo = pSTOCK.m_cMarketNo;

                        string strMsg = "市場別：" + nMarketNo.ToString() + " 商品代碼：" + pSTOCK.m_caStockNo + " 商品名稱：" + pSTOCK.m_caName;

                        //string strMsg = " 商品代碼：" + pSTOCK.m_caStockNo + " 商品名稱：" + pSTOCK.m_caName;

                        listStockInfo.Items.Add(strMsg);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        #endregion

        #region CallBack Function
        //----------------------------------------------------------------------
        // CallBack Function
        //----------------------------------------------------------------------

        public void OnNotifyConnection(int nKind, int nCode)
        {
            if (nCode == 0)
            {
                if (nKind == 100)
                {
                    lblConnect.ForeColor = Color.Green;
                    m_bConnect = true;

                    /*
                    int nPage = 1;
                    m_nCode = Functions.SKQuoteLib_RequestStocks(out nPage, "1101,6005,TX00");
                     */

                }
                else if (nKind == 101)
                {
                    lblConnect.ForeColor = Color.Red;
                    m_bConnect = false;
                }
            }
        }

        public void OnNotifyQuote(short sMarketNo, short sStockidx)
        {
            STOCK pSTOCK; 

            Functions.SKQuoteLib_GetStockByIndex(sMarketNo, sStockidx, out pSTOCK);

            OnUpDateDataRow(pSTOCK);
        }

        public void OnNotifyTicks(short sMarketNo, short sStockidx, int nPtr)
        {
            TICK tick;

            Functions.SKQuoteLib_GetTick(sMarketNo, sStockidx, nPtr, out tick);

            InsertTick(tick, sMarketNo, sStockidx, nPtr);
        }

        public  void OnNotifyBest5(short sMarketNo, short sStockidx)
        {
            BEST5 best5;

            Functions.SKQuoteLib_GetBest5(sMarketNo, sStockidx, out best5);

            InsertBest5(best5);   
        }

        public void OnNotifyServerTime(short sHour, short sMinute, short sSecond, int nTotal)
        {
            lblGetServerTime.Text = DateTime.Now.ToString("HH:mm:ss fff");

            string strTime = string.Format("{0}:{1}:{2}", sHour.ToString(),sMinute.ToString(),sSecond.ToString());

            OnPrintServerTime(strTime);
        }

        private void gridStocks_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                e.Graphics.FillRectangle(Brushes.Black, e.CellBounds);
             
                if (e.Value != null)
                {
                    string strHeaderText =  ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText.ToString();
                      
                    if (strHeaderText == "名稱")
                    {
                        e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.SkyBlue, e.CellBounds.X, e.CellBounds.Y);
                    }
                    else if (strHeaderText == "買進" || strHeaderText == "賣出" || strHeaderText == "成交價")
                    {
                        double dPrc = double.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells["m_nRef"].Value.ToString());
 
                        double dValue = double.Parse(e.Value.ToString());

                        if (dValue > dPrc)
                        { 
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Red, e.CellBounds.X, e.CellBounds.Y);
                        }
                        else if (dValue < dPrc)
                        {
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Lime, e.CellBounds.X, e.CellBounds.Y);
                        }
                        else
                        {
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.White, e.CellBounds.X, e.CellBounds.Y);
                        }
                    }
                    else if (strHeaderText == "單量")
                    {
                        e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Yellow, e.CellBounds.X, e.CellBounds.Y);
                    }
                    else
                    {
                        e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.White, e.CellBounds.X, e.CellBounds.Y);
                    } 
                }
                e.Handled = true; 
            }
        }

        public void OnNotifyTicksGet(short sMarketNo, short sStockidx, int nPtr, int nTime, int nBid, int nAsk, int nClose, int nQty)
        {
            m_Tick.m_nTime  = nTime;
            m_Tick.m_nBid   = nBid;
            m_Tick.m_nAsk   = nAsk;
            m_Tick.m_nClose = nClose;
            m_Tick.m_nPtr   = nPtr;
            m_Tick.m_nQty   = nQty;

            InsertTick(m_Tick, sMarketNo, sStockidx, nPtr);

        }

        public void OnProductsReady()
        {
            lblMessage.Text = "商品載入完成，可進行";
        }

        #endregion

        #region Custom Method
        //----------------------------------------------------------------------
        // Custom Method
        //----------------------------------------------------------------------
        public string GetApiCodeDefine(int nCode)
        {
            string strNCode = Enum.GetName(typeof(ApiMessage), nCode);

            if (strNCode == "" || strNCode == null)
            {
                return nCode.ToString();
            }
            else
            {
                return strNCode;
            }
        }

        public void OnPrintServerTime(string strMsg)
        {
            if (lblServerTime.InvokeRequired == true)
            {
                this.Invoke(new InvokeSendMessage(this.OnPrintServerTime), new object[] { strMsg });
            }
            else
            {
                lblServerTime.Text = strMsg;
            }
        }

        private DataTable CreateStocksDataTable()
        {
            DataTable myDataTable = new DataTable();

            DataColumn myDataColumn;
            
            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int16");
            myDataColumn.ColumnName = "m_sStockidx";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int16");
            myDataColumn.ColumnName = "m_sDecimal";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int16");
            myDataColumn.ColumnName = "m_sTypeNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_cMarketNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caStockNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caName";
            myDataTable.Columns.Add(myDataColumn);
             
            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nOpen";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nHigh";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nLow";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nClose";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTickQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nRef";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nBid";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nBc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nAsk";
            myDataTable.Columns.Add(myDataColumn);
             
            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nAc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTBc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTAc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nFutureOI";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nYQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nUp";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nDown";
            myDataTable.Columns.Add(myDataColumn);


            myDataTable.PrimaryKey = new DataColumn[] { myDataTable.Columns["m_caStockNo"] };  


            return myDataTable;
        }

        private DataTable CreateTickDataTable()
        {
            DataTable myDataTable = new DataTable();

            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nPtr";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int64");
            myDataColumn.ColumnName = "m_nTime";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nBid";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nAsk";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nClose";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nQty";
            myDataTable.Columns.Add(myDataColumn);

            return myDataTable;
        }

        private DataTable CreateBest5AskTable()
        {
            DataTable myDataTable = new DataTable();

            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nAskQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nAsk";
            myDataTable.Columns.Add(myDataColumn);

            return myDataTable;

        }

        private void OnUpDateDataRow( STOCK pStock )
        {
            string strStockNo = pStock.m_caStockNo;

            DataRow drFind = m_dtStocks.Rows.Find(strStockNo); 
            if (drFind == null) 
            {
                try
                {
                    DataRow myDataRow = m_dtStocks.NewRow();

                    myDataRow["m_sStockidx"]    = pStock.m_sStockidx;
                    myDataRow["m_sDecimal"]     = pStock.m_sDecimal;
                    myDataRow["m_sTypeNo"]      = pStock.m_sTypeNo;
                    myDataRow["m_cMarketNo"]    = pStock.m_cMarketNo;
                    myDataRow["m_caStockNo"]    = pStock.m_caStockNo;
                    myDataRow["m_caName"]       = pStock.m_caName;
                    myDataRow["m_nOpen"]        = pStock.m_nOpen / ( Math.Pow( 10, pStock.m_sDecimal));
                    myDataRow["m_nHigh"]        = pStock.m_nHigh / ( Math.Pow( 10, pStock.m_sDecimal));
                    myDataRow["m_nLow"]         = pStock.m_nLow / ( Math.Pow( 10, pStock.m_sDecimal));
                    myDataRow["m_nClose"]       = pStock.m_nClose / ( Math.Pow( 10, pStock.m_sDecimal));
                    myDataRow["m_nTickQty"]     = pStock.m_nTickQty;
                    myDataRow["m_nRef"]         = pStock.m_nRef / ( Math.Pow( 10, pStock.m_sDecimal));
                    myDataRow["m_nBid"]         = pStock.m_nBid / ( Math.Pow( 10, pStock.m_sDecimal));
                    myDataRow["m_nBc"]          = pStock.m_nBc;
                    myDataRow["m_nAsk"]         = pStock.m_nAsk / ( Math.Pow( 10, pStock.m_sDecimal));
                    myDataRow["m_nAc"]          = pStock.m_nAc;
                    myDataRow["m_nTBc"]         = pStock.m_nTBc;
                    myDataRow["m_nTAc"]         = pStock.m_nTAc;
                    myDataRow["m_nFutureOI"]    = pStock.m_nFutureOI;
                    myDataRow["m_nTQty"]        = pStock.m_nTQty;
                    myDataRow["m_nYQty"]        = pStock.m_nYQty;
                    myDataRow["m_nUp"]          = pStock.m_nUp / ( Math.Pow( 10, pStock.m_sDecimal));
                    myDataRow["m_nDown"]        = pStock.m_nDown / ( Math.Pow( 10, pStock.m_sDecimal));

                    m_dtStocks.Rows.Add(myDataRow);
                     
                }
                catch( Exception ex )
                {
                    string msg = ex.Message;
                }
            }
            else
            {
                drFind["m_sStockidx"]   = pStock.m_sStockidx;
                drFind["m_sDecimal"]    = pStock.m_sDecimal;
                drFind["m_sTypeNo"]     = pStock.m_sTypeNo;
                drFind["m_cMarketNo"]   = pStock.m_cMarketNo;
                drFind["m_caStockNo"]   = pStock.m_caStockNo;
                drFind["m_caName"]      = pStock.m_caName;
                drFind["m_nOpen"]       =  pStock.m_nOpen / ( Math.Pow( 10, pStock.m_sDecimal));
                drFind["m_nHigh"]       = pStock.m_nHigh / ( Math.Pow( 10, pStock.m_sDecimal));
                drFind["m_nLow"]        = pStock.m_nLow / ( Math.Pow( 10, pStock.m_sDecimal));
                drFind["m_nClose"]      = pStock.m_nClose / ( Math.Pow( 10, pStock.m_sDecimal));
                drFind["m_nTickQty"]    = pStock.m_nTickQty;
                drFind["m_nRef"]        = pStock.m_nRef / ( Math.Pow( 10, pStock.m_sDecimal));
                drFind["m_nBid"]        = pStock.m_nBid / ( Math.Pow( 10, pStock.m_sDecimal));
                drFind["m_nBc"]         = pStock.m_nBc;
                drFind["m_nAsk"]        = pStock.m_nAsk / ( Math.Pow( 10, pStock.m_sDecimal));
                drFind["m_nAc"]         = pStock.m_nAc;
                drFind["m_nTBc"]        = pStock.m_nTBc;
                drFind["m_nTAc"]        = pStock.m_nTAc;
                drFind["m_nFutureOI"]   = pStock.m_nFutureOI;
                drFind["m_nTQty"]       = pStock.m_nTQty;
                drFind["m_nYQty"]       = pStock.m_nYQty;
                drFind["m_nUp"]         = pStock.m_nUp / ( Math.Pow( 10, pStock.m_sDecimal));
                drFind["m_nDown"]       = pStock.m_nDown / ( Math.Pow( 10, pStock.m_sDecimal));
            }
        }

        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession) return;

            System.Reflection.PropertyInfo aProp =
                        typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }

        public void InsertTick(TICK pTick, short sMarketNo, short sStockidx, int nPtr)
        {
            DataRow myDataRow = m_dtTick.NewRow();

            myDataRow["m_nPtr"] = pTick.m_nPtr;
            myDataRow["m_nTime"] = pTick.m_nTime;

            if (pTick.m_nBid == -999999)
                myDataRow["m_nBid"] = 0;
            else
                myDataRow["m_nBid"] = pTick.m_nBid / 100.00;

            if (pTick.m_nAsk == -999999)
                myDataRow["m_nAsk"] = 0;
            else
                myDataRow["m_nAsk"] = pTick.m_nAsk / 100.00;

            if (pTick.m_nAsk == -999999)
                myDataRow["m_nClose"] = 0;
            else
                myDataRow["m_nClose"] = pTick.m_nClose / 100.00;

            myDataRow["m_nQty"] = pTick.m_nQty;

            m_dtTick.Rows.Add(myDataRow);

            insertTickToDB(pTick, sMarketNo, sStockidx, nPtr);
        }
        

        private void insertTickToDB(TICK pTick, short sMarketNo, short sStockidx, int nPtr)
        {
            String symbol = "";
            String key = sMarketNo.ToString() + sStockidx.ToString();

            if (symbolNames.Contains(key))
            {
                symbol = System.Convert.ToString(symbolNames[key]);
            }
            else {
                STOCK pSTOCK;
                int nCode = Functions.SKQuoteLib_GetStockByIndex(sMarketNo, sStockidx, out pSTOCK);
                if (nCode == 0)
                {
                    int nMarketNo = pSTOCK.m_cMarketNo;
                    //商品代碼
                    symbolNames.Add(key, pSTOCK.m_caStockNo);
                    symbol = pSTOCK.m_caStockNo;
                }
            }

            //insert db
            Tick newTick = new Tick();
            newTick.marketNo = sMarketNo;
            newTick.stockIdx = symbol;
            newTick.ptr = nPtr;
            newTick.date = DateTime.Today.ToString("yyyyMMdd");
            if (pTick.m_nBid == -999999)
                newTick.bid = 0;
            else
                newTick.bid = Convert.ToInt32(pTick.m_nBid / 100.00);

            if (pTick.m_nAsk == -999999)
                newTick.ask = 0;
            else
                newTick.ask = Convert.ToInt32(pTick.m_nAsk / 100.00);
            
            if (pTick.m_nAsk == -999999)
                newTick.close = 0;
            else
                newTick.close = Convert.ToInt32(pTick.m_nClose / 100.00);

            //FIXME : SQLite高速寫檔 有效能問題
            //sqliteContext.GetTable<Tick>().InsertOnSubmit(newTick);
            //sqliteContext.SubmitChanges();
        }

        private void InsertBest5(BEST5 Best5)
        {
            if (m_dtBest5Ask.Rows.Count == 0 &&  m_dtBest5Bid.Rows.Count == 0)
            {
                DataRow myDataRow;

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"]  = Best5.m_nAskQty1;
                myDataRow["m_nAsk"]     = Best5.m_nAsk1/ 100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"]  = Best5.m_nAskQty2;
                myDataRow["m_nAsk"]     = Best5.m_nAsk2/ 100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"]  = Best5.m_nAskQty3;
                myDataRow["m_nAsk"] =    Best5.m_nAsk3/ 100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"]  = Best5.m_nAskQty4;
                myDataRow["m_nAsk"]     = Best5.m_nAsk4/ 100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"]  = Best5.m_nAskQty5;
                myDataRow["m_nAsk"]     = Best5.m_nAsk5/ 100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);



                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = Best5.m_nBidQty1;
                myDataRow["m_nAsk"] = Best5.m_nBid1 / 100.00;
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = Best5.m_nBidQty2;
                myDataRow["m_nAsk"] = Best5.m_nBid2 / 100.00;
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = Best5.m_nBidQty3;
                myDataRow["m_nAsk"] = Best5.m_nBid3 / 100.00;
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = Best5.m_nBidQty4;
                myDataRow["m_nAsk"] = Best5.m_nBid4 / 100.00;
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = Best5.m_nBidQty5;
                myDataRow["m_nAsk"] = Best5.m_nBid5 / 100.00;
                m_dtBest5Bid.Rows.Add(myDataRow);

            }
            else
            {
                m_dtBest5Ask.Rows[0]["m_nAskQty"]   = Best5.m_nAskQty1;
                m_dtBest5Ask.Rows[0]["m_nAsk"]      = Best5.m_nAsk1/ 100.00;

                m_dtBest5Ask.Rows[1]["m_nAskQty"]   = Best5.m_nAskQty2;
                m_dtBest5Ask.Rows[1]["m_nAsk"]      = Best5.m_nAsk2/ 100.00;
                
                m_dtBest5Ask.Rows[2]["m_nAskQty"]   = Best5.m_nAskQty3;
                m_dtBest5Ask.Rows[2]["m_nAsk"]      = Best5.m_nAsk3/ 100.00;
                
                m_dtBest5Ask.Rows[3]["m_nAskQty"]   = Best5.m_nAskQty4;
                m_dtBest5Ask.Rows[3]["m_nAsk"]      = Best5.m_nAsk4/ 100.00;
                
                m_dtBest5Ask.Rows[4]["m_nAskQty"]   = Best5.m_nAskQty5;
                m_dtBest5Ask.Rows[4]["m_nAsk"]      = Best5.m_nAsk5/ 100.00;


                m_dtBest5Bid.Rows[0]["m_nAskQty"]   = Best5.m_nBidQty1;
                m_dtBest5Bid.Rows[0]["m_nAsk"]      = Best5.m_nBid1 / 100.00;

                m_dtBest5Bid.Rows[1]["m_nAskQty"]   = Best5.m_nBidQty2;
                m_dtBest5Bid.Rows[1]["m_nAsk"]      = Best5.m_nBid2 / 100.00;

                m_dtBest5Bid.Rows[2]["m_nAskQty"]   = Best5.m_nBidQty3;
                m_dtBest5Bid.Rows[2]["m_nAsk"]      = Best5.m_nBid3 / 100.00;

                m_dtBest5Bid.Rows[3]["m_nAskQty"]   = Best5.m_nBidQty4;
                m_dtBest5Bid.Rows[3]["m_nAsk"]      = Best5.m_nBid4 / 100.00;

                m_dtBest5Bid.Rows[4]["m_nAskQty"]   = Best5.m_nBidQty5;
                m_dtBest5Bid.Rows[4]["m_nAsk"]      = Best5.m_nBid5 / 100.00;
            }
        }

        #endregion

        public void reconnectTimerEvent(object source, ElapsedEventArgs args)
        {
            if (!m_bConnect)
            {
                try
                {
                    UIntPtr uPtr = new UIntPtr(100);
                    SendMessage(HWND_BROADCAST, MSG_SHOW, uPtr, IntPtr.Zero);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }            
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == MSG_SHOW)
            {
                btnEnterMonitor_Click(null, null);
            }
            base.WndProc(ref m);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strStocks;
            int nPage = 2;

            m_dtStocks.Clear();
            gridStocks.ClearSelection();

            gridStocks.DataSource = m_dtStocks;

            gridStocks.Columns["m_sStockidx"].Visible = false;
            gridStocks.Columns["m_sDecimal"].Visible = false;
            gridStocks.Columns["m_sTypeNo"].Visible = false;
            gridStocks.Columns["m_cMarketNo"].Visible = false;
            gridStocks.Columns["m_caStockNo"].HeaderText = "代碼";
            gridStocks.Columns["m_caName"].HeaderText = "名稱";
            gridStocks.Columns["m_nOpen"].HeaderText = "開盤價";
            gridStocks.Columns["m_nHigh"].Visible = false;
            gridStocks.Columns["m_nLow"].Visible = false;
            gridStocks.Columns["m_nClose"].HeaderText = "成交價";
            gridStocks.Columns["m_nTickQty"].HeaderText = "單量";
            gridStocks.Columns["m_nRef"].HeaderText = "昨收價";
            gridStocks.Columns["m_nBid"].HeaderText = "買進";
            gridStocks.Columns["m_nBc"].Visible = false;
            gridStocks.Columns["m_nAsk"].HeaderText = "賣出";
            gridStocks.Columns["m_nAc"].Visible = false;
            gridStocks.Columns["m_nTBc"].Visible = false;
            gridStocks.Columns["m_nTAc"].Visible = false;
            gridStocks.Columns["m_nFutureOI"].Visible = false;
            gridStocks.Columns["m_nTQty"].Visible = false;
            gridStocks.Columns["m_nYQty"].Visible = false;
            gridStocks.Columns["m_nUp"].Visible = false;
            gridStocks.Columns["m_nDown"].Visible = false;

            strStocks = txtStocks.Text.Trim();

            string[] Stocks = strStocks.Split(new Char[] { ',' });

            foreach (string s in Stocks)
            {
                STOCK pSTOCK;

                int nCode = Functions.SKQuoteLib_GetStockByNo(s.Trim(), out pSTOCK);

                if (nCode == 0)
                {
                    OnUpDateDataRow(pSTOCK);
                }
            }

            strStocks = "MTX00,MTX02";

            m_nCode = Functions.SKQuoteLib_RequestStocks(out nPage, strStocks);

            if (m_nCode == 0)
            {
                lblMessage.Text = "SKQuoteLib_RequestStocks 呼叫成功";
            }
            else
            {
                lblMessage.Text = "呼叫失敗 [CODE] " + GetApiCodeDefine(m_nCode);
            }
        }

        private List<Code_Mapping> findCodeMapping() {
            List<Code_Mapping> mappings = (sqliteContext.ExecuteQuery<Code_Mapping>("SELECT * FROM CODE_MAPPING order by name, value")).ToList();
            foreach (var mapping in mappings)
            {
                Console.WriteLine("{0},{1},{2}", mapping.name, mapping.value, mapping.description);
            };
            return mappings;
        }

        private DataTable findBookings()
        {
            List<Booking> booking = (sqliteContext.ExecuteQuery<Booking>("SELECT * FROM Booking")).ToList();
            return DataTableConverter.from(booking);
        }

        private void btnInsertBook_Click(object sender, EventArgs e)
        {
            //sqliteContext = new DataContext(new SQLiteConnection("Data source=localDB.db"));
            //sqliteContext.ExecuteCommand("insert into booking(foreign, symbol, market_no, analyze_interval_level, analyze_interval, strategy_required_bar_no, write_file, write_path, push_mq) values ({0},{1},{2},{3},{4},{5},{6},{7},{8})",
            Booking newBooking = new Booking();
            newBooking.foreign = Int32.Parse(ddRegion.SelectedValue.ToString());
            newBooking.symbol = txtBoxSymbol.Text;
            newBooking.marketNo = txtBoxMarket.Text;
            newBooking.analyzeIntervalLevel = Int32.Parse(ddInterval.SelectedValue.ToString());
            newBooking.analyzeInterval = Int32.Parse(txtBoxInterval.Text);
            newBooking.strategyRequireBarNo = Int32.Parse(txtBoxMaxBars.Text);
            newBooking.writeFile = (checkIfWriteFile.Checked == false ? 0 : 1);
            newBooking.writePath = txtBoxWritePath.Text;
            newBooking.pushMQ = (checkIfMQ.Checked == false ? 0 : 1);
            sqliteContext.GetTable<Booking>().InsertOnSubmit(newBooking);
            sqliteContext.SubmitChanges();

            gridBooking.DataSource = this.findBookings();
        }

        private void btnSaveTick_Click(object sender, EventArgs e)
        {
            /*
            List<Tick> ticks = DataTableConverter.to<Tick>(m_dtTick);
            foreach (var tick in ticks) {
                try
                {
                    sqliteContext.GetTable<Tick>().InsertOnSubmit(tick);
                    sqliteContext.SubmitChanges();
                } catch (Exception ex) {
                    Console.Write(ex.ToString());
                }
            }
            */
        }
    }
}
