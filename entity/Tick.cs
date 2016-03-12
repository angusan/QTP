using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace QTP.entity
{
    [Table(Name = "Tick")]
    class Tick
    {
        [Column(Name = "MARKET_NO", IsPrimaryKey = true)]
        public int marketNo { set; get; }
        [Column(Name = "STOCK_IDX", IsPrimaryKey = true)]
        public string stockIdx { set; get; }
        [Column(Name = "PTR", IsPrimaryKey = true)]
        public int ptr { set; get; } // KEY
        [Column(Name = "TICK_DATE")]
        public string date { set; get; } //日期
        [Column(Name = "TICK_TIME")]
        public int time { set; get; } //時間
        [Column(Name = "BID")]
        public int bid { set; get; } // 買價
        [Column(Name = "ASK")]
        public int ask { set; get; } // 賣價
        [Column(Name = "CLOSE")]
        public int close { set; get; } //成交價
        [Column(Name = "QTY")]
        public int qty { set; get; } // 成交量
    }
}
