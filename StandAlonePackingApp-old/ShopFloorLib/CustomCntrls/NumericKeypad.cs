using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorLib
{
    public partial class NumericKeypad : UserControl
    {
        public string valueEntered { get; private set; }

        private bool decimalEntry;

        public NumericKeypad()
        {
            InitializeComponent();
            valueEntered = "";
        }
        public void SetDecimalEntry(bool _decEntry = true)
        {
            decimalEntry = _decEntry;
        }

        public event EventHandler<ValueChangedEventArgs> ValueChanged;
        public event EventHandler<ValueChangedEventArgs> EnterPressed;

        protected virtual void OnValueChanged(ValueChangedEventArgs e)
        {
            var handler = ValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnEnterPressed(ValueChangedEventArgs e)
        {
            var handler = EnterPressed;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public void resetValue()
        {
            valueEntered = "";
        }
        public void setValue(string _val)
        {
            valueEntered = _val;
        }
        private void updateFilter(char c)
        {
            switch (c)
            {
                case '.':
                    if (decimalEntry && !valueEntered.Contains(".") && valueEntered.Length<8)
                        valueEntered += c;
                    break;
                case 'B':
                    if (valueEntered.Length > 0)
                        valueEntered = valueEntered.Substring(0, valueEntered.Length - 1);
                    break;
                case 'C':
                    valueEntered = "";
                    break;
                default:
                    if (valueEntered.Length < 8)
                        valueEntered += c;
                    break;
            }
            var args = new ValueChangedEventArgs();
            args.val = valueEntered;
            OnValueChanged(args);
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            updateFilter('1');
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            updateFilter('2');
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            updateFilter('3');
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            updateFilter('4');
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            updateFilter('5');
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            updateFilter('6');
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            updateFilter('7');
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            updateFilter('8');
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            updateFilter('9');
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            updateFilter('0');
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            updateFilter('.');
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (decimalEntry)
            {
                decimal d;
                // Check that a valid decimal number has been entered
                if (!decimal.TryParse(valueEntered, out d))
                {
                    return;
                }
            }
            var args = new ValueChangedEventArgs();
            args.val = valueEntered;
            OnEnterPressed(args);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            updateFilter('C');
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            updateFilter('B');
        }
    }
    public class ValueChangedEventArgs : EventArgs
    {
        public string val { get; set; }
    }

}
