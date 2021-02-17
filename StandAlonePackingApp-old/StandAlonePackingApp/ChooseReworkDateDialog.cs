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
using StandAlonePackingLib;

namespace StandAlonePackingApp
{
    public partial class ChooseReworkDateDialog : Form
    {
        public Order.PackedOn dateSel;

        public ChooseReworkDateDialog()
        {
            InitializeComponent();

            buttonMatrix1.ButtonSelected += HandleButtonSelected;
            dateSel = null;
        }
        private void ChooseMode_Load(object sender, EventArgs e)
        {
            buttonMatrix1.Setup(4, 2, CommonData.slDates.Cast<ButtonMatrix.MatrixObject>().ToList(), dateSel);
        }
        private void HandleButtonSelected(object sender, ButtonMatrix.ButtonSelectedEventArgs e)
        {
            dateSel = (Order.PackedOn)e.obj;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
