using System.Xml.Serialization;

namespace ShopFloorLib
{
    [XmlRootAttribute("DBSettings")]
    public class DBSettings
    {
        public string DBServer { get; set; }
        public string DBUser { get; set; }
        public string DBPassword { get; set; }
    }
}
