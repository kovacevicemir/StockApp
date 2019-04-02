using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string lastSearchAll { get; set; }
        public string lastSearchHistory { get; set; }
        public string lastSearchLive { get; set; }
    }
}
