using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StockWcf
{
    [DataContract]
    public class Member
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string firstname { get; set; }
        [DataMember]
        public string lastname { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string lastSearchAll { get; set; }
        [DataMember]
        public string lastSearchHistory { get; set; }
        [DataMember]
        public string lastSearchLive { get; set; }
    }
}
