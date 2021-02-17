using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ShopFloorLib
{
    public class LabelPrinter
    {
        public LabelPrinterSettings settings { private set; get; }

        private Dictionary<string, FileInfo> fileBuffer;

        public class FileInfo
        {
            public List<string> symbols;
            public string contents;

            public FileInfo()
            {
                symbols = new List<string>();
            }
        }

        public LabelPrinter()
        {
            settings = (LabelPrinterSettings)XMLConfigHandler.ReadConfig(typeof(LabelPrinterSettings));
            fileBuffer = new Dictionary<string, FileInfo>();
        }

        public bool Print(string fileName, Dictionary<string,string> fldValues)
        {
            FileInfo fileInfo;
            try
            {
                fileInfo = fileBuffer[fileName];
            }
            catch(KeyNotFoundException)
            {
                fileInfo = BuildFileInfo(fileName);
                if (fileInfo == null)
                    return false;
                else
                    fileBuffer.Add(fileName, fileInfo);
            }

            string s = string.Copy(fileInfo.contents);

            foreach(string sym in fileInfo.symbols)
            {
                try {
                    var val = fldValues[sym].ToString();
                    s = s.Replace(sym, val);
                }
                catch (KeyNotFoundException)
                {
                    MessageLogger.Add("Unknown symbol in label file: " + sym, MessageLogger.MsgLevel.warning);
                    s = s.Replace(sym, "??");
                }
            }
            return RawPrinterHelper.SendStringToPrinter(settings.printerName, s);
        }

        private FileInfo BuildFileInfo(string fileName)
        {
                FileInfo fileInfo = new FileInfo();

                string filename = settings.labelFolder + @"\" + fileName + ".lbl";
                try
                {
                    fileInfo.contents = File.ReadAllText(filename);
                }
                catch(System.IO.IOException exp)
                {
                    MessageLogger.Add(string.Format("Label file {0} couldn't be opened. Msg {1}", filename, exp.ToString()), MessageLogger.MsgLevel.error);
                    return null;
                }
                StringReader sr = new StringReader(fileInfo.contents);
                string l;
                while ((l = sr.ReadLine()) != null)
                {
                    int start = 0;
                    while ((start = l.IndexOf('&', start)) != -1 && start < l.Length)
                    {
                        int end = l.IndexOf('&', start + 1);
                        if (end == -1) break;
                        fileInfo.symbols.Add(l.Substring(start, end - start + 1));
                        start = end + 1;
                    }
                }
                
                return fileInfo;
        }
    }
}
