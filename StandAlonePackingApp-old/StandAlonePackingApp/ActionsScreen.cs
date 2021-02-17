using System;
using System.Windows.Forms;
using ShopFloorLib;

namespace StandAlonePackingApp
{
    public partial class ActionsScreen : Form
    {
        public ActionsScreen()
        {
            InitializeComponent();
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            ThisApp.ReprintLast();
            Close();
        }
        private void btnCancelLast_Click(object sender, EventArgs e)
        {
            ThisApp.CancelLast();
            Close();
        }
        private void btnRereadOrdersSAP_Click(object sender, EventArgs e)
        {
            new BusyDialog(ThisApp.ReadOrdersFromSAP).ShowDialog();
            Close();
        }

        private void btnRereadMaterialsSAP_Click(object sender, EventArgs e)
        {
            new BusyDialog(ThisApp.ReadMaterialsFromSAP).ShowDialog();
            Close();
        }

        private void btnReadOrdersDB_Click(object sender, EventArgs e)
        {
            new BusyDialog(ThisApp.ReadOrdersFromDB).ShowDialog();
            Close();
        }

        private void btnRereadMaterialsDB_Click(object sender, EventArgs e)
        {
            new BusyDialog(ThisApp.ReadMaterialsFromDB).ShowDialog();
            Close();
        }

        private void btnToggleTolerance_Click(object sender, EventArgs e)
        {
            if(ThisApp.weightToleranceDisabled)
            {
                MessageLogger.Add("Weight Tolerance Checking Re-enabled.", MessageLogger.MsgLevel.info);
                ThisApp.weightToleranceDisabled = false;
            }
            else
            {
                MessageLogger.Add("Weight Tolerance Checking Disabled.", MessageLogger.MsgLevel.warning);
                ThisApp.weightToleranceDisabled = true;
            }
            this.Close();
        }

        private void btnTestLabel_Click(object sender, EventArgs e)
        {
            if (ThisApp.PrintTestLabel())
                MessageLogger.Add("Test label printed successfully", MessageLogger.MsgLevel.info);
            else
                MessageLogger.Add("Error printing Test Label", MessageLogger.MsgLevel.error);

            Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUserAdmin_Click(object sender, EventArgs e)
        {
            if (!ThisApp.user.accessUserAdmin)
            {
                MessageBox.Show("You are not authorised to maintain users", "No Authorisation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            new UserAdminScreen().ShowDialog();
        }

        private void ActionsScreen_Load(object sender, EventArgs e)
        {
            // Only allow packing related buttons if the user is authorised to pack cartons
            btnCancelLast.Visible = ThisApp.user.accessPackCarton;
            btnReprint.Visible = ThisApp.user.accessPackCarton;
            btnRereadMaterialsDB.Visible = ThisApp.user.accessPackCarton;
            btnRereadMaterialsSAP.Visible = ThisApp.user.accessPackCarton;
            btnReadOrdersDB.Visible = ThisApp.user.accessPackCarton;
            btnRereadOrdersSAP.Visible = ThisApp.user.accessPackCarton;
            btnToggleTolerance.Visible = ThisApp.user.accessPackCarton;

            btnImageAdmin.Visible = ThisApp.user.accessSendImage || ThisApp.user.accessMaintainImage || ThisApp.user.accessBlockImage;
            btnUserAdmin.Visible = ThisApp.user.accessUserAdmin;
            btnToggleTolerance.Text = ThisApp.weightToleranceDisabled ? "Enable Weight Tolerance" : "Disable Weight Tolerance";
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            new UserAdmin.ChangePasswordForm(ThisApp.user).ShowDialog();
        }

        private void btnImageAdmin_Click(object sender, EventArgs e)
        {
            new PrinterImagesDialog().ShowDialog();
        }
    }
}
