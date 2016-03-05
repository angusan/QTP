using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace qtp
{

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void FOnNotifyConnection(int nKind, int nCode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void FOnNotifyQuote(short sMarketNo, short sStockidx);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void FOnNotifyTicks(short sMarketNo, short sStockidx, int nPtr);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void FOnNotifyBest5(short sMarketNo, short sStockidx);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void FOnNotifyKLineData([MarshalAs(UnmanagedType.BStr)]string caStockNo, [MarshalAs(UnmanagedType.BStr)]string caData);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void FOnNotifyServerTime(short sHour, short sMinute, short sSecond, int nTotal);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void FOnNotifyMarketTot(short sMarketNo, short sPrt, int lTime, int lTotv, int lTots, int lTotc);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void FOnNotifyMarketBuySell(short cMarketNo, short sPrt, int lTime, int lBc, int lSc, int lBs, int lSs);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void FOnNotifyTicksGet( short sMarketNo, short sStockidx,
											  int nPtr, int nTime,int nBid, int nAsk, int nClose, int nQty);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void FOnNotifyFutureTradeInfo( [MarshalAs(UnmanagedType.BStr)]string caStockNo, short sMarket, short sStockidx, int nBuyTotalCount, int nSellTotalCount, int nBuyTotalQty, int nSellTotalQty, int nBuyDealTotalQty, int nSellDealTotalQty);


    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void FOnProductsReady();

    public struct FORMAT05
    {
        public short m_sHour;			// 時
        public short m_sMinute;			// 分
        public short m_sSecond;			// 秒
        public long m_lTotal;				// 總秒數
    }

    public struct TICK
    {
        public int m_nPtr; // KEY
        public int m_nTime; //時間
        public int m_nBid; // 買價
        public int m_nAsk; // 賣價
        public int m_nClose; //成交價
        public int m_nQty; // 成交量
    }

    public struct BEST5
    {
        public int m_nBid1;
        public int m_nBidQty1;
        public int m_nBid2;
        public int m_nBidQty2;
        public int m_nBid3;
        public int m_nBidQty3;
        public int m_nBid4;
        public int m_nBidQty4;
        public int m_nBid5;
        public int m_nBidQty5;
        public int m_nExtendBid;
        public int m_nExtendBidQty;
        public int m_nAsk1;
        public int m_nAskQty1;
        public int m_nAsk2;
        public int m_nAskQty2;
        public int m_nAsk3;
        public int m_nAskQty3;
        public int m_nAsk4;
        public int m_nAskQty4;
        public int m_nAsk5;
        public int m_nAskQty5; 
        public int m_nExtendAsk; 
        public int m_nExtendAskQty; 
    }

    public struct STOCK
    {
        public short m_sStockidx;				// 系統自行定義的股票代碼
        public short m_sDecimal;				// 報價小數位數
        public short m_sTypeNo;				// 類股分類
        //public char m_cMarketNo;		        // 市場代號0x00上市;0x01上櫃;0x02期貨;0x03選擇權; 0x04興櫃

        public char m_cMarketNo;      // 市場代號0x00上市;0x01上櫃;0x02期貨;0x03選擇權; 0x04興櫃

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string m_caStockNo;      // 股票代號
        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string m_caName;         // 股票名稱

        public int m_nOpen;					// 開盤價
        public int m_nHigh;					// 最高價
        public int m_nLow;					    // 最低價
        public int m_nClose;					// 成交價
        public int m_nTickQty;				    // 單量
        public int m_nRef;					    // 昨收、參考價
        public int m_nBid;					    // 買價
        public int m_nBc;					    // 買量
        public int m_nAsk;					    // 賣價
        public int m_nAc;					    // 賣量
        public int m_nTBc;					    // 買盤量
        public int m_nTAc;					    // 賣盤量
        public int m_nFutureOI;				// 期貨未平倉 OI
        public int m_nTQty;					// 總量
        public int m_nYQty;					// 昨量
        public int m_nUp;					    // 漲停價
        public int m_nDown;					// 跌停價

    }

    enum ApiMessage 
    {
        SK_SUCCESS                                      = 0,
        SK_FAIL                                         = -1,
        SK_ERROR_SERVER_NOT_CONNECTED					= -2,
        SK_ERROR_INITIALIZE_FAIL						= -4,
        SK_ERROR_ACCOUNT_NOT_EXIST						= 1,
        SK_ERROR_ACCOUNT_MARKET_NOT_MATCH				= 2,
        SK_ERROR_PERIOD_OUT_OF_RANGE					= 3,
        SK_ERROR_FLAG_OUT_OF_RANGE						= 4,
        SK_ERROR_BUYSELL_OUT_OF_RANGE					= 5,
        SK_ERROR_ORDER_SERVER_INVALID					= 6,
        SK_ERROR_PERMISSION_DENIED						= 7,
        SK_ERROR_TRADE_TYPR_OUT_OF_RANGE				= 8,
        SK_ERROR_PERMISSION_TIMEOUT                     = 9,
        SK_ERROR_SERVER_RESET_DATA			            = 10,
        SK_SUBJECT_CONNECTION_CONNECTED			        = 100,
        SK_SUBJECT_CONNECTION_DISCONNECT			    = 101,
        SK_SUBJECT_CONNECTION_CLEAR                     = 102,
        SK_SUBJECT_CONNECTION_RECONNECT                 = 103,
        SK_SUBJECT_QUOTE_PAGE_EXCEED				    = 200,
        SK_SUBJECT_QUOTE_PAGE_INCORRECT			        = 201,
        SK_SUBJECT_TICK_PAGE_EXCEED					    = 210,
        SK_SUBJECT_TICK_PAGE_INCORRECT				    = 211,
        SK_SUBJECT_TICK_STOCK_NOT_FOUND			        = 212,
        SK_SUBJECT_BEST5_DATA_NOT_FOUND			        = 213,
        SK_SUBJECT_QUOTEREQUEST_NOT_FOUND		        = 214,
        SK_SUBJECT_SERVER_TIME_NOT_FOUND			    = 215

    }

    class Functions
    {
        [DllImport("SKQuoteLib.dll", EntryPoint = "SKQuoteLib_Initialize", CharSet = CharSet.Ansi)]
        public static extern int SKQuoteLib_Initialize(string pcUserName, string pcPassword);

        [DllImport("SKQuoteLib.dll", EntryPoint = "SKQuoteLib_EnterMonitor", CharSet = CharSet.Ansi)]
        public static extern int SKQuoteLib_EnterMonitor();

        [DllImport("SKQuoteLib.dll", EntryPoint = "SKQuoteLib_LeaveMonitor", CharSet = CharSet.Ansi)]
        public static extern int SKQuoteLib_LeaveMonitor();

        [DllImport("SKQuoteLib.dll", EntryPoint = "SKQuoteLib_RequestServerTime", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int SKQuoteLib_RequestServerTime();

        [DllImport("SKQuoteLib.dll", EntryPoint = "SKQuoteLib_GetServerTime", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int SKQuoteLib_GetServerTime( out FORMAT05 Time);

        [DllImport("SKQuoteLib.dll", EntryPoint = "SKQuoteLib_RequestStocks", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int SKQuoteLib_RequestStocks(out int psPageNo, string pStockNos);

        [DllImport("SKQuoteLib.dll", EntryPoint = "SKQuoteLib_GetStockByIndex", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int SKQuoteLib_GetStockByIndex(short sMarketNo, short sIndex,out STOCK TStock );

        [DllImport("SKQuoteLib.dll", EntryPoint = "SKQuoteLib_RequestTicks", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int SKQuoteLib_RequestTicks(out int psPageNo, string pStockNos);

        [DllImport("SKQuoteLib.dll", EntryPoint = "SKQuoteLib_GetTick", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int SKQuoteLib_GetTick(int sMarketNo, int sStockidx,int nPtr, out TICK Tick);

        [DllImport("SKQuoteLib.dll", EntryPoint = "SKQuoteLib_GetBest5", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int SKQuoteLib_GetBest5(int sMarketNo, int sStockidx, out BEST5 Best5);

        [DllImport("SKQuoteLib.dll", EntryPoint = "SKQuoteLib_GetKLine", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int SKQuoteLib_GetKLine(string pStockNos, int nKLineType);

        [DllImport("SKQuoteLib.dll", EntryPoint = "SKQuoteLib_GetStockByNo", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int SKQuoteLib_GetStockByNo(string pStockNo, out STOCK TStock);
        



        [DllImport("SKQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SKQuoteLib_AttachConnectionCallBack(FOnNotifyConnection Connection);

        [DllImport("SKQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SKQuoteLib_AttachQuoteCallBack(FOnNotifyQuote Quote);

        [DllImport("SKQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SKQuoteLib_AttachTicksCallBack(FOnNotifyTicks Ticks);

        [DllImport("SKQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SKQuoteLib_AttachBest5CallBack(FOnNotifyBest5 Quote);
        
        [DllImport("SKQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SKQuoteLib_AttchServerTimeCallBack(FOnNotifyServerTime Quote);

        [DllImport("SKQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SKQuoteLib_AttachKLineDataCallBack(FOnNotifyKLineData KLine);

        [DllImport("SKQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SKQuoteLib_AttachMarketTotCallBack(FOnNotifyMarketTot MarketTot);

        [DllImport("SKQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SKQuoteLib_AttachMarketBuySellCallBack(FOnNotifyMarketBuySell MarketBuySell);

        [DllImport("SKQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SKQuoteLib_AttachHistoryTicksGetCallBack(FOnNotifyTicksGet TicksGet);

        [DllImport("SKQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SKQuoteLib_AttachProductsReadyCallBack(FOnProductsReady ProductsReady);

        [DllImport("SKQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SKQuoteLib_AttachFutureTradeInfoCallBack(FOnNotifyFutureTradeInfo ProductsReady);
        
    }
}
