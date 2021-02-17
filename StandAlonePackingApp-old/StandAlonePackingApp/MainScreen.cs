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
using System.IO;
using System.Threading;

namespace StandAlonePackingApp
{
    public partial class MainScreen : Form
    {
        private Order selectedOrder;
        private Order.IncOrder selectedIncOrder;
        private static Order.PackedOn reworkDateSel = null;
        private static ThisApp.Mode currMode = ThisApp.Mode.NormalPacking;

        public MainScreen()
        {
            InitializeComponent();
        }

        #region MainScreen_Load
        private void MainScreen_Load(object sender, EventArgs e)
        {
            MessageLogger.SetLogControl(this.messageLog1);

            this.Text += " - " + CommonData.sapSettings.device;
            btnRecord.Visible = false;
            grpBoxSelectedMaterial.Visible = false;

            barcodeScanner1.startRunning(this);

            if (CommonData.localSettings.OtherPackingStationPort != 0)
                backgroundWorker1.RunWorkerAsync();

            txtUserName.Text = string.Format("User: {0}", ThisApp.user);

            // Do main application initialisation in a separate thread, showing the startup dialog while we're waiting
            new BusyDialog(ThisApp.Initialise).ShowDialog();
            
            if (CommonData.slDates.Count > 0)
                reworkDateSel = CommonData.slDates[0];

            scaleIndicator1.SetReader(ScaleReader.GetScaleReader());
            ThisApp.scale = scaleIndicator1;

            buttonMatrix1.Setup(4, 2, CommonData.normalOrders.Cast<ButtonMatrix.MatrixObject>().ToList());

            numericKeypad1.ValueChanged += HandleFilterValueChanged;
            buttonMatrix1.ButtonSelected += HandleButtonSelected;
            barcodeScanner1.BarcodeRead += HandleBarcodeScanned;

            UpdateModeText();
        }
        #endregion

        #region HandleFilterValueChanged
        private void HandleFilterValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblFilter.Text = "Filter: " + e.val;
            CommonData.filter = e.val;

            buttonMatrix1.Redraw();
        }
        #endregion

        #region HandleBarcodeScanned
        private void HandleBarcodeScanned(object sender, BCEventArgs e)
        {
            if (currMode == ThisApp.Mode.Deletion)
            {
                ThisApp.CancelCartonReceipt(e.val);
            }
        }
        #endregion

        #region UpdateModeText
        private void UpdateModeText()
        {
            switch (currMode)
            {
                case ThisApp.Mode.NormalPacking :
                    txtCurrMode.Text = "Normal Packing";
                    txtCurrMode.BackColor = this.BackColor;
                    btnChooseSlDate.Visible = false;
                    break;
                case ThisApp.Mode.Deletion:
                    txtCurrMode.Text = "Deletion";
                    txtCurrMode.BackColor = Color.Crimson;
                    btnChooseSlDate.Visible = false;
                    break;
                case ThisApp.Mode.PartCarton:
                    txtCurrMode.Text = "Part Carton";
                    txtCurrMode.BackColor = Color.Yellow;
                    btnChooseSlDate.Visible = false;
                    break;
                case ThisApp.Mode.Rework:
                    txtCurrMode.Text = "Re-work";
                    txtCurrMode.BackColor = Color.PowderBlue;
                    btnChooseSlDate.Visible = true;
                    break;
            }
            btnChooseSlDate.Text = reworkDateSel == null ? "No re-work date selected" : reworkDateSel.ToString();
        }
        #endregion

        #region HandleButtonSelected
        private void HandleButtonSelected(object sender, ButtonMatrix.ButtonSelectedEventArgs e)
        {
            selectedOrder = (Order)e.obj;

            if (selectedOrder == null)
            {
                DeselectOrder();
                return;
            }

            ShowSelectedMaterialDetails();

            if (selectedOrder.material.madeToOrd)
            {
                ProcessBinSelection();
            }
            else
            {
                if (currMode != ThisApp.Mode.Rework || reworkDateSel != null)
                    // Activate Record button (for rework - must have selected a production date as well)
                    btnRecord.Visible = true;

                // Packing a normal carton; set tare based on order components 
                ThisApp.scale.SetTare(selectedOrder.incOrders[0].tareWeight);

                MessageLogger.Add(String.Format("Material {0} tare set to {1}", selectedOrder.materialNum, selectedOrder.incOrders[0].tareWeight),
                                MessageLogger.MsgLevel.info);
            }
        }
        #endregion

        #region ShowSelectedMaterialDetails
        private void ShowSelectedMaterialDetails()
        {
            // Show details of the selected material
            lblMat.Text = string.Format("{0} - {1}", selectedOrder.material.matNumber, selectedOrder.material.description);
            lblQty.Text = string.Format("{0} / {1} {2}", selectedOrder.packedQty.ToString("#0"), selectedOrder.targetQty.ToString("#"), selectedOrder.Uom);


            lblRange.BackColor = SystemColors.Control;
            lblRange.ForeColor = SystemColors.ControlText;

            // Show Maximum and Minimum pack weights for material (if maintained).
            if (ThisApp.weightToleranceDisabled)
            {
                lblRange.Text = "Weight Tolerance Disabled";
                lblRange.BackColor = Color.DarkMagenta;
                lblRange.ForeColor = Color.White;
            }
            else if (selectedOrder.material.minWeight != 0M && selectedOrder.material.maxWeight != 0M)
                lblRange.Text = string.Format("{0} to {1} kg", selectedOrder.material.minWeight, selectedOrder.material.maxWeight);
            else if (selectedOrder.material.minWeight != 0M)
                lblRange.Text = string.Format("Minimum of {0} kg", selectedOrder.material.minWeight);
            else if (selectedOrder.material.maxWeight != 0M)
                lblRange.Text = string.Format("Maximum of {0} kg", selectedOrder.material.maxWeight);
            else
                lblRange.Text = "No weight restriction";

            // Show the maximum qty allowed to be packed according to overdelivery tolerance limit
            if (selectedOrder.maxQty != 0)
                lblQty.Text = string.Format("{0} - up to a limit of {1} {2}", lblQty.Text, selectedOrder.maxQty, selectedOrder.Uom);

            lblCustLabel.Visible = lblCustomer.Visible = false;
            grpBoxSelectedMaterial.Visible = true;
        }
        #endregion

        #region ProcessBinSelection
        private void ProcessBinSelection()
        {
            if (selectedOrder.incOrders.Count == 1)
            {
                // Only one order for this material - select it straight away without getting the user to choose it
                selectedIncOrder = selectedOrder.incOrders[0];
            }
            else
            {
                // Multiple orders for this material - show dialog for user to chooise which one to use
                var co = new ChooseOrderForBinDialog();
                co.ord = selectedOrder;
                if (co.ShowDialog() != DialogResult.OK)
                {
                    DeselectOrder();
                    return;
                }
                selectedIncOrder = co.incOrdSel;
            }
            // Show customer details of selected order
            lblCustLabel.Visible = lblCustomer.Visible = true;
            if (selectedIncOrder.customer.Length == 0)
                lblCustomer.Text = "Arndell Park";
            else
                lblCustomer.Text = string.Format("{0} - {1} / {2}",
                                        selectedIncOrder.customer,
                                        selectedIncOrder.custName,
                                        selectedIncOrder.delivDate.ToString("dd.MM.yy"));

            var tw = new TareWeightForBinDialog();
            
            if (tw.ShowDialog() != DialogResult.OK)
            {
                DeselectOrder();
                return;
            }
            // Activate Record button 
            btnRecord.Visible = true;

            // Packing a normal carton; set tare based on order components 
            ThisApp.scale.SetTare(tw.tw);

            MessageLogger.Add(String.Format("Material {0} tare set to {1}", selectedOrder.materialNum, tw.tw),
                            MessageLogger.MsgLevel.info);
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region  btnRecord_Click
        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (ThisApp.PostReceipt(selectedOrder, currMode, reworkDateSel) == true)
            {
                // Receipt was successful, clear selection and filter. Re-draw the buttons.
                DeselectOrder();
                buttonMatrix1.Redraw();
            }
        }
        #endregion

        #region DeselectOrder
        private void DeselectOrder()
        {
            CommonData.filter = "";
            lblFilter.Text = "Filter:";
            numericKeypad1.resetValue();
            selectedOrder = null;
            btnRecord.Visible = false;
            grpBoxSelectedMaterial.Visible = false;
        }
        #endregion

        #region btnSwitchMode_Click
        private void btnSwitchMode_Click(object sender, EventArgs e)
        {
            // Call dialog box for choosing processing mode.
            var cm = new ChooseModeDialog();
            cm.modeSelected = currMode;
            if (cm.ShowDialog() == DialogResult.OK)
            {
                // User selected a mode in the dialog box - switch processing modes
                currMode = cm.modeSelected;
                UpdateModeText();

                // De-select any selected order.
                DeselectOrder();
                SwitchMode();
            }
        }
        #endregion

        #region SwitchMode
        private void SwitchMode()
        {
                switch (currMode)
                {
                    case ThisApp.Mode.NormalPacking:
                        buttonMatrix1.SetList(CommonData.normalOrders.Cast<ButtonMatrix.MatrixObject>().ToList());
                        MessageLogger.Add("Normal Packing mode selected", MessageLogger.MsgLevel.info);
                        break;
                    case ThisApp.Mode.Rework:
                        buttonMatrix1.SetList(CommonData.reworkOrders.Cast<ButtonMatrix.MatrixObject>().ToList());
                        MessageLogger.Add("Re-work mode selected", MessageLogger.MsgLevel.info);
                        break;
                    case ThisApp.Mode.Deletion:
                        buttonMatrix1.SetList(null);
                        MessageLogger.Add("Deletion mode selected", MessageLogger.MsgLevel.info);
                        break;
                    case ThisApp.Mode.PartCarton:
                        MessageBox.Show("Part Carton functionality is not implemented yet :(");
                        buttonMatrix1.SetList(null);
                        MessageLogger.Add("Part Carton mode selected", MessageLogger.MsgLevel.info);
                        break;
                } 
        }
        #endregion

        #region btnChooseSlDate_Click
        private void btnChooseSlDate_Click(object sender, EventArgs e)
        {
            if (CommonData.slDates.Count == 0)
            {
                MessageLogger.Add("No packing dates maintained", MessageLogger.MsgLevel.error);
                return;
            }

            var cd = new ChooseReworkDateDialog();
            cd.dateSel = reworkDateSel;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                reworkDateSel = cd.dateSel;
                UpdateModeText();
            }
        }
        #endregion

        #region  btnActions_Click
        private void btnActions_Click(object sender, EventArgs e)
        {
            var act = new ActionsScreen();
            act.ShowDialog();
            DeselectOrder();
            SwitchMode();
        }
        #endregion

        #region backgroundWorker1_DoWork
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            InterStationComms.ListenToOtherStation(buttonMatrix1);
        }
        #endregion
    }
}
