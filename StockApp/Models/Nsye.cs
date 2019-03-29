using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Models
{
    public class Nsye
    {
        public string exchange { get; set; }
        public string stock_symbol { get; set; }
        public DateTime date { get; set; }
        public float stock_price_open { get; set; }
        public float stock_price_high { get; set; }
        public float stock_price_low { get; set; }
        public float stock_price_close { get; set; }
        public int stock_volume { get; set; }
        public float stock_price_adj_close { get; set; }
    }
}
