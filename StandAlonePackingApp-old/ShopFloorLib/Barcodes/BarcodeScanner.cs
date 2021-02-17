using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorLib
{
    public partial class BarcodeScanner : Component
    {
        private BarcodeScannerSettings settings;

        private char firstChar;
        private char lastChar;
        private bool inBc = false;
        private string bc = "";

        public event EventHandler<BCEventArgs> BarcodeRead;

        public BarcodeScanner()
        {
            InitializeComponent();
        }

        public BarcodeScanner(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected virtual void OnBarcodeRead(BCEventArgs e)
        {
            var handler = BarcodeRead;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public void startRunning(Form parent = null)
        {
            settings = (BarcodeScannerSettings)XMLConfigHandler.ReadConfig(typeof(BarcodeScannerSettings));
            
            if (settings.prefix == 'A')
                firstChar = ']';   //AIM code
            else
                firstChar = settings.prefix;

            if (settings.suffix == 'E')
                lastChar = '\r';
            else
                lastChar = settings.suffix;

            if (parent != null && settings.readFrom.Equals("Keyboard"))
            {
                // Read input from the keyboard
                parent.KeyPreview = true;
                parent.KeyPress += HandleKeyPress;
            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void HandleKeyPress(object sender, KeyPressEventArgs e)
        {
            BarcodeChar(e.KeyChar);
            e.Handled = true;  //Don't forward key press onto window
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SerialPort port = null;

            try
            {
                port = new SerialPort(settings.readFrom, settings.baudRate, settings.parity, settings.dataBits, settings.stopBits);

                port.Open();

                while (port.IsOpen)
                {
                    BarcodeChar((char)port.ReadChar());
                }
            }
            catch(Exception exp)
            {
                MessageLogger.Add(string.Format("Can't connect to barcode scanner {0}", settings), MessageLogger.MsgLevel.warning);
                MessageLogger.Add(string.Format("Error opening barcode port {0}", exp.ToString()), MessageLogger.MsgLevel.additional);
            }
            finally
            {
                if (port != null && port.IsOpen)
                    port.Close();
            }
        }

        private void BarcodeChar(char c)
        {
            if (c == firstChar)
            {
                bc = "";
                inBc = true;
            }
            else if (c == lastChar)
            {   
                if(inBc)
                {
                    // Got one
                    var args = new BCEventArgs();
                    if( settings.prefix == 'A' && bc.Length > 2)  //AIM Code is 1st 3 chars 
                        args.val = bc.Substring(2);
                    else
                        args.val = bc;

                    OnBarcodeRead(args);

                    inBc = false;
                    bc = "";
                }
            }
            else if (inBc)
            {
                bc += c;
            }
        }
    }
    public class BCEventArgs : EventArgs
    {
        public string val { get; set; }
    }
}
