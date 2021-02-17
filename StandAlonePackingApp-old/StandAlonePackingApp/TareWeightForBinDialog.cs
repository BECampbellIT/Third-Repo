using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShopFloorLib;

namespace StandAlonePackingApp
{
    public partial class TareWeightForBinDialog : Form
    {
        public decimal tw;

        public TareWeightForBinDialog()
        {
            InitializeComponent();
            numericKeypad1.SetDecimalEntry();

            numericKeypad1.ValueChanged += HandleValueChanged;
            numericKeypad1.EnterPressed += HandleEnterPressed;
        }
        private void HandleValueChanged(object sender, ValueChangedEventArgs e)
        {
            txtBxTare.Text = e.val.ToString();
        }
        private void HandleEnterPressed(object sender, ValueChangedEventArgs e)
        {
            tw = decimal.Parse(e.val.ToString());
            DialogResult = DialogResult.OK;
            Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
