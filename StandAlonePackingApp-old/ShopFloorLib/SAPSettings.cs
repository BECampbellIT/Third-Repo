using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShopFloorLib
{
    [XmlRootAttribute("SAPSettings")]
    public class SAPSettings
    {
        public string hostname { get; set; }
        public string client { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string device { get; set; }
    }
}
