using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShopFloorLib
{
    [XmlRootAttribute("LabelPrinterSettings")]
    public class LabelPrinterSettings
    {
        public string printerName { set; get; }
        public string labelFolder { set; get; }
    }
}
