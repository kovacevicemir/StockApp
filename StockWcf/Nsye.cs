using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StockWcf
{
    [DataContract]
    public class Nsye
    {
        [DataMember]
        public string exchange { get; set; }
        [DataMember]
        public string stock_symbol { get; set; }
        [DataMember]
        public string date_from { get; set; } = "10/10/1999";
        [DataMember]
        public string date_to { get; set; } = "10/12/2025";
        [DataMember]
        public float stock_price_open_from { get; set; } = 1;
        [DataMember]
        public float stock_price_open_to { get; set; } = 1000;
        [DataMember]
        public float stock_price_high_from { get; set; } = 1;
        [DataMember]
        public float stock_price_high_to { get; set; } = 1000;
        [DataMember]
        public float stock_price_low_from { get; set; } = 1;
        [DataMember]
        public float stock_price_low_to { get; set; } = 1000;
        [DataMember]

        public float stock_price_close_from { get; set; } = 1;
        [DataMember]
        public float stock_price_close_to { get; set; } = 1000;
        [DataMember]
        public int stock_volume_from { get; set; } = 1;
        [DataMember]
        public int stock_volume_to { get; set; } = 999999999;
        [DataMember]
        public float stock_price_adj_close_from { get; set; } = 1;
        [DataMember]
        public float stock_price_adj_close_to { get; set; } = 1000;
    }
}
