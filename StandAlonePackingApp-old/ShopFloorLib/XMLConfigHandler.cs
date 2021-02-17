using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShopFloorLib
{
    public class XMLConfigHandler
    {
        static public object ReadConfig(Type type)
        {
            // Just some boiler plate deserialisation code...
            var settings = Activator.CreateInstance(type);
            System.IO.FileStream fs = null;

            try
            {
                // Create an instance of the XmlSerializer class; specify the type of object to be deserialized.
                XmlSerializer serializer = new XmlSerializer(type);

                /* If the XML document has been altered with unknown nodes or attributes, handle them with the 
                UnknownNode and UnknownAttribute events.*/
                serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
                serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

                // A FileStream is needed to read the XML document.
                fs = new System.IO.FileStream( type.Name + ".xml", System.IO.FileMode.Open);

                /* Use the Deserialize method to restore the object's state withdata from the XML document. */
                settings = serializer.Deserialize(fs);
            }
            catch (Exception exp)
            {
                if (fs != null) fs.Close();
                MessageLogger.Add(exp.ToString(), MessageLogger.MsgLevel.error);
                return null;
            }
            fs.Close();

            return settings;
        }

        static private void serializer_UnknownNode
        (object sender, System.Xml.Serialization.XmlNodeEventArgs e)
        {
            MessageLogger.Add(string.Format("Error in xml settings file - unknown Node: {0} \t {1}", e.Name, e.Text), MessageLogger.MsgLevel.error);
        }

        static private void serializer_UnknownAttribute
        (object sender, System.Xml.Serialization.XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            MessageLogger.Add("Error in xml settings file - unknown attribute " + attr.Name + "='" + attr.Value + "'", MessageLogger.MsgLevel.error);
        }

        static public bool SaveConfig(object settings)
        {
            // Just some boiler plate serialisation code...
            try
            {
                Type type = settings.GetType();

                // Create an instance of the XmlSerializer class; specify the type of object to be deserialized.
                XmlSerializer serializer = new XmlSerializer(type);

                /* If the XML document has been altered with unknown nodes or attributes, handle them with the 
                UnknownNode and UnknownAttribute events.*/
                serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
                serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

                // A FileStream is needed to read the XML document.
                System.IO.FileStream fs = new System.IO.FileStream(type.Name + ".xml", System.IO.FileMode.Create);

                /* Use the Deserialize method to restore the object's state withdata from the XML document. */
                serializer.Serialize(fs, settings);
            }
            catch (Exception exp)
            {
                MessageLogger.Add(exp.ToString(), MessageLogger.MsgLevel.error);
                return false;
            }
            return true;
        }
    }
}
