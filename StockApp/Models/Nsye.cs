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

        public string date_from { get; set; }
        public string date_to { get; set; }

        public float stock_price_open_from { get; set; } = 1;
        public float stock_price_open_to { get; set; } = 1000;

        public float stock_price_high_from { get; set; } = 1;
        public float stock_price_high_to { get; set; } = 1000;

        public float stock_price_low_from { get; set; } = 1;
        public float stock_price_low_to { get; set; } = 1000;

        public float stock_price_close_from { get; set; } = 1;
        public float stock_price_close_to { get; set; } = 1000;

        public int stock_volume_from { get; set; } = 1;
        public int stock_volume_to { get; set; } = 999999999;

        public float stock_price_adj_close_from { get; set; } = 1;
        public float stock_price_adj_close_to { get; set; } = 1000;

    }
}
