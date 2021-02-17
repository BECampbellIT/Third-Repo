using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StandAlonePackingLib
{
    [XmlRootAttribute("LocalSettings")]
    public class LocalSettings
    {
        public bool PullOrdersAtStartup { get; set; }
        public bool PullMaterialsAtStartup { get; set; }

        public int CartonSendInterval { get; set; }
        public int OrderReadInterval { get; set; }
        public int MaterialReadInterval { get; set; }

        public string OtherPackingStationAddr { get; set; }
        public int OtherPackingStationPort { get; set; }
        public int ThisPackingStationPort { get; set; }
    }
}
