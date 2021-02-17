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
    public partial class ChooseModeDialog : Form
    {
        List<ModeSel> modes;
        public ThisApp.Mode modeSelected;

        public ChooseModeDialog()
        {
            InitializeComponent();

            modes = new List<ModeSel>();
            modes.Add(new ModeSel(ThisApp.Mode.NormalPacking, "Normal Packing", ""));
            modes.Add(new ModeSel(ThisApp.Mode.Deletion, "Deletion Mode :", "Scan label to delete from system"));
            modes.Add(new ModeSel(ThisApp.Mode.PartCarton, "Part Carton Mode :", "Pack and Fill-up Part Cartons"));
            modes.Add(new ModeSel(ThisApp.Mode.Rework, "Re-work mode :", "Receipt re-worked cartons with a specific production date"));

            buttonMatrix1.ButtonSelected += HandleButtonSelected;
        }
        private void ChooseMode_Load(object sender, EventArgs e)
        {
            buttonMatrix1.Setup(4, 1, modes.Cast<ButtonMatrix.MatrixObject>().ToList(), modes.Find(m => m._mode == modeSelected));
        }
        private void HandleButtonSelected(object sender, ButtonMatrix.ButtonSelectedEventArgs e)
        {
            modeSelected = ((ModeSel)e.obj)._mode;

            DialogResult = DialogResult.OK;
            Close();
        }
        class ModeSel : ButtonMatrix.MatrixObject
        {
            public ThisApp.Mode _mode;
            private string _name;
            private string _desc;

            public ModeSel(ThisApp.Mode mode, string name, string desc)
            {
                _mode = mode;
                _name = name;
                _desc = desc;
            }
            /************************************
             * Implement methods from MatrixObject interface 
             ************************************/
            override public string ToString()
            {
                return string.Format("{0}\r\n{1}", _name, _desc);
            }
            public bool MatchesFilter()
            {
                return true;
            }
            public Color GetNormalColor()
            {
                return Color.MintCream;
            }
            public Color GetSelectedColor()
            {
                return Color.Cyan;
            }
            public string GetKey()
            {
                return _mode.ToString();
            }
        }
    }
}
