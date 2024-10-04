using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookLookupDomain.Resources
{
    public class LogDetail
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public string Product { get; set; }
        public string Layer { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public long? ElapsedMilliseconds { get; set; }
    }
}
