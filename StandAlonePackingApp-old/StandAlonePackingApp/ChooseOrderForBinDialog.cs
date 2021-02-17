using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StandAlonePackingLib;
using ShopFloorLib;

namespace StandAlonePackingApp
{
    public partial class ChooseOrderForBinDialog : Form
    {
        public Order ord = null;
        public Order.IncOrder incOrdSel = null;

        public ChooseOrderForBinDialog()
        {
            InitializeComponent();

            buttonMatrix1.ButtonSelected += HandleButtonSelected;
        }
        
        private void BinDetailsDialog_Load(object sender, EventArgs e)
        {
            buttonMatrix1.Setup(4, 1, ord.incOrders.Cast<ButtonMatrix.MatrixObject>().ToList());
        }

        private void HandleButtonSelected(object sender, ButtonMatrix.ButtonSelectedEventArgs e)
        {
            incOrdSel = (Order.IncOrder)e.obj;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
