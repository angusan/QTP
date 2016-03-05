using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace QTP.entity
{
    [Table(Name ="Booking")]
    class Booking
    {
        [Column(Name ="FOREIGN", IsPrimaryKey = true)]
        public int foreign { set; get; }
        [Column(Name = "SYMBOL", IsPrimaryKey = true)]
        public string symbol { set; get; }
        [Column(Name = "MARKET_NO", IsPrimaryKey = true)]
        public string marketNo { set; get; }
        [Column(Name = "ANALYZE_INTERVAL_LEVEL", IsPrimaryKey = true)]
        public int analyzeIntervalLevel { set; get; }
        [Column(Name = "ANALYZE_INTERVAL", IsPrimaryKey = true)]
        public int analyzeInterval { set; get; }
        [Column(Name = "STRATEGY_REQUIRED_BAR_NO", IsPrimaryKey = true)]
        public int strategyRequireBarNo { set; get; }
        [Column(Name = "WRITE_FILE")]
        public int writeFile { set; get; }
        [Column(Name = "WRITE_PATH")]
        public string writePath { set; get; }
        [Column(Name = "PUSH_MQ")]
        public int pushMQ { set; get; }
    }
}
