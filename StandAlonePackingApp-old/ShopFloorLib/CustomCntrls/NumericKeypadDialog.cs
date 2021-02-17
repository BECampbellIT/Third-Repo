using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorLib
{
    public partial class NumericKeypadDialog : Form
    {
        private TextBox txt = null;
        public string result { get; private set; }

        public NumericKeypadDialog(string title, TextBox entryField = null)
        {
            InitializeComponent();

            if (entryField != null)
            {
                txt = entryField;
                result = entryField.Text;
            }
            else
            {
                txt = null;
                result = "";
            }

            this.Text = title;
            numericKeypad1.SetDecimalEntry(false);
            numericKeypad1.setValue(result);
            numericKeypad1.ValueChanged += NumericKeypad1_ValueChanged;
            numericKeypad1.EnterPressed += NumericKeypad1_EnterPressed;
        }

        private void NumericKeypad1_EnterPressed(object sender, ValueChangedEventArgs e)
        {
            result = numericKeypad1.valueEntered;
            this.Close();
        }

        private void NumericKeypad1_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            result = numericKeypad1.valueEntered;

            if ( txt != null)
            {
                txt.Text = e.val;
            }
        }
    }
}
