using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace ShopFloorLib
{
    public partial class LabelPrinterSettingsCntrl : UserControl
    {
        public LabelPrinterSettingsCntrl()
        {
            InitializeComponent();
            GetPrinterNames();

            var settings = (LabelPrinterSettings)XMLConfigHandler.ReadConfig(typeof(LabelPrinterSettings));
            if (settings != null)
                UpdateComponents(settings);
        }
        private void GetPrinterNames()
        {
            foreach(var prt in PrinterSettings.InstalledPrinters)
            {
                cmboBxPrinter.Items.Add(prt.ToString());
            }
        }
        private void UpdateComponents(LabelPrinterSettings s)
        {
            cmboBxPrinter.Text = s.printerName;
            txtFolder.Text = s.labelFolder;
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Choose Label Folder";
            DialogResult res = folderBrowserDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                txtFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        public bool SaveSettings()
        {
            var settings = new LabelPrinterSettings();

            settings.printerName = cmboBxPrinter.Text;
            settings.labelFolder = txtFolder.Text;

            return XMLConfigHandler.SaveConfig(settings);
        }
    }
}
