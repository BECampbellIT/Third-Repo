namespace StandAlonePackingApp
{
    partial class SettingsScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsScreen));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.grpAppSettings = new System.Windows.Forms.GroupBox();
            this.chkBxReadMats = new System.Windows.Forms.CheckBox();
            this.chkBxReadOrders = new System.Windows.Forms.CheckBox();
            this.labelPrinterSettingsCntrl1 = new ShopFloorLib.LabelPrinterSettingsCntrl();
            this.sapSettingsCntrl1 = new ShopFloorLib.SAPSettingsCntrl();
            this.scaleSettingsCntrl1 = new ShopFloorLib.ScaleSettingsCntrl();
            this.barcodeScannerSettingsCntrl1 = new ShopFloorLib.BarcodeScannerSettingsCntrl();
            this.gtpServiceSettings = new System.Windows.Forms.GroupBox();
            this.lblMaterialReadInt2 = new System.Windows.Forms.Label();
            this.numMaterialReadInt = new System.Windows.Forms.NumericUpDown();
            this.lblOrderReadInt2 = new System.Windows.Forms.Label();
            this.numOrderReadInt = new System.Windows.Forms.NumericUpDown();
            this.lblMaterialReadInt1 = new System.Windows.Forms.Label();
            this.lblOrderReadInt1 = new System.Windows.Forms.Label();
            this.lblCartonSendInt2 = new System.Windows.Forms.Label();
            this.lblCartonSendInt1 = new System.Windows.Forms.Label();
            this.numCartonSendInterval = new System.Windows.Forms.NumericUpDown();
            this.txtOtherStationAddr = new System.Windows.Forms.TextBox();
            this.lblOtherPackingStationAddr = new System.Windows.Forms.Label();
            this.lblOtherStationsAddr = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOtherStationPort = new System.Windows.Forms.TextBox();
            this.txtThisStationPort = new System.Windows.Forms.TextBox();
            this.lblThisStationsPort = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dbSettingsCntrl1 = new ShopFloorLib.DBSettingsCntrl();
            this.grpAppSettings.SuspendLayout();
            this.gtpServiceSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaterialReadInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderReadInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCartonSendInterval)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.ForestGreen;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(828, 631);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(123, 32);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save Settings";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Crimson;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(690, 631);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(98, 32);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // grpAppSettings
            // 
            this.grpAppSettings.BackColor = System.Drawing.Color.Snow;
            this.grpAppSettings.Controls.Add(this.chkBxReadMats);
            this.grpAppSettings.Controls.Add(this.chkBxReadOrders);
            this.grpAppSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAppSettings.Location = new System.Drawing.Point(498, 3);
            this.grpAppSettings.Name = "grpAppSettings";
            this.grpAppSettings.Size = new System.Drawing.Size(480, 91);
            this.grpAppSettings.TabIndex = 17;
            this.grpAppSettings.TabStop = false;
            this.grpAppSettings.Text = "Application Settings";
            // 
            // chkBxReadMats
            // 
            this.chkBxReadMats.AutoSize = true;
            this.chkBxReadMats.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBxReadMats.Location = new System.Drawing.Point(17, 58);
            this.chkBxReadMats.Name = "chkBxReadMats";
            this.chkBxReadMats.Size = new System.Drawing.Size(237, 20);
            this.chkBxReadMats.TabIndex = 3;
            this.chkBxReadMats.Text = "Read Materials from SAP at Startup";
            this.chkBxReadMats.UseVisualStyleBackColor = true;
            // 
            // chkBxReadOrders
            // 
            this.chkBxReadOrders.AutoSize = true;
            this.chkBxReadOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBxReadOrders.Location = new System.Drawing.Point(17, 30);
            this.chkBxReadOrders.Name = "chkBxReadOrders";
            this.chkBxReadOrders.Size = new System.Drawing.Size(223, 20);
            this.chkBxReadOrders.TabIndex = 2;
            this.chkBxReadOrders.Text = "Read Orders from SAP at Startup";
            this.chkBxReadOrders.UseVisualStyleBackColor = true;
            // 
            // labelPrinterSettingsCntrl1
            // 
            this.labelPrinterSettingsCntrl1.Location = new System.Drawing.Point(12, 353);
            this.labelPrinterSettingsCntrl1.Name = "labelPrinterSettingsCntrl1";
            this.labelPrinterSettingsCntrl1.Size = new System.Drawing.Size(480, 93);
            this.labelPrinterSettingsCntrl1.TabIndex = 16;
            // 
            // sapSettingsCntrl1
            // 
            this.sapSettingsCntrl1.Location = new System.Drawing.Point(12, 157);
            this.sapSettingsCntrl1.Name = "sapSettingsCntrl1";
            this.sapSettingsCntrl1.Size = new System.Drawing.Size(480, 190);
            this.sapSettingsCntrl1.TabIndex = 15;
            // 
            // scaleSettingsCntrl1
            // 
            this.scaleSettingsCntrl1.Location = new System.Drawing.Point(499, 250);
            this.scaleSettingsCntrl1.Name = "scaleSettingsCntrl1";
            this.scaleSettingsCntrl1.Size = new System.Drawing.Size(480, 252);
            this.scaleSettingsCntrl1.TabIndex = 14;
            // 
            // barcodeScannerSettingsCntrl1
            // 
            this.barcodeScannerSettingsCntrl1.Location = new System.Drawing.Point(13, 453);
            this.barcodeScannerSettingsCntrl1.Name = "barcodeScannerSettingsCntrl1";
            this.barcodeScannerSettingsCntrl1.Size = new System.Drawing.Size(480, 210);
            this.barcodeScannerSettingsCntrl1.TabIndex = 18;
            // 
            // gtpServiceSettings
            // 
            this.gtpServiceSettings.BackColor = System.Drawing.Color.Snow;
            this.gtpServiceSettings.Controls.Add(this.lblMaterialReadInt2);
            this.gtpServiceSettings.Controls.Add(this.numMaterialReadInt);
            this.gtpServiceSettings.Controls.Add(this.lblOrderReadInt2);
            this.gtpServiceSettings.Controls.Add(this.numOrderReadInt);
            this.gtpServiceSettings.Controls.Add(this.lblMaterialReadInt1);
            this.gtpServiceSettings.Controls.Add(this.lblOrderReadInt1);
            this.gtpServiceSettings.Controls.Add(this.lblCartonSendInt2);
            this.gtpServiceSettings.Controls.Add(this.lblCartonSendInt1);
            this.gtpServiceSettings.Controls.Add(this.numCartonSendInterval);
            this.gtpServiceSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gtpServiceSettings.Location = new System.Drawing.Point(499, 498);
            this.gtpServiceSettings.Name = "gtpServiceSettings";
            this.gtpServiceSettings.Size = new System.Drawing.Size(480, 117);
            this.gtpServiceSettings.TabIndex = 19;
            this.gtpServiceSettings.TabStop = false;
            this.gtpServiceSettings.Text = "Background Service Settings";
            // 
            // lblMaterialReadInt2
            // 
            this.lblMaterialReadInt2.AutoSize = true;
            this.lblMaterialReadInt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaterialReadInt2.Location = new System.Drawing.Point(283, 87);
            this.lblMaterialReadInt2.Name = "lblMaterialReadInt2";
            this.lblMaterialReadInt2.Size = new System.Drawing.Size(109, 16);
            this.lblMaterialReadInt2.TabIndex = 13;
            this.lblMaterialReadInt2.Text = "hours (0 = never).";
            // 
            // numMaterialReadInt
            // 
            this.numMaterialReadInt.Location = new System.Drawing.Point(207, 85);
            this.numMaterialReadInt.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numMaterialReadInt.Name = "numMaterialReadInt";
            this.numMaterialReadInt.Size = new System.Drawing.Size(70, 22);
            this.numMaterialReadInt.TabIndex = 12;
            // 
            // lblOrderReadInt2
            // 
            this.lblOrderReadInt2.AutoSize = true;
            this.lblOrderReadInt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderReadInt2.Location = new System.Drawing.Point(283, 55);
            this.lblOrderReadInt2.Name = "lblOrderReadInt2";
            this.lblOrderReadInt2.Size = new System.Drawing.Size(122, 16);
            this.lblOrderReadInt2.TabIndex = 11;
            this.lblOrderReadInt2.Text = "minutes (0 = never).";
            // 
            // numOrderReadInt
            // 
            this.numOrderReadInt.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numOrderReadInt.Location = new System.Drawing.Point(207, 54);
            this.numOrderReadInt.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numOrderReadInt.Name = "numOrderReadInt";
            this.numOrderReadInt.Size = new System.Drawing.Size(70, 22);
            this.numOrderReadInt.TabIndex = 10;
            // 
            // lblMaterialReadInt1
            // 
            this.lblMaterialReadInt1.AutoSize = true;
            this.lblMaterialReadInt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaterialReadInt1.Location = new System.Drawing.Point(5, 87);
            this.lblMaterialReadInt1.Name = "lblMaterialReadInt1";
            this.lblMaterialReadInt1.Size = new System.Drawing.Size(196, 16);
            this.lblMaterialReadInt1.TabIndex = 9;
            this.lblMaterialReadInt1.Text = "Read Materials from SAP every";
            // 
            // lblOrderReadInt1
            // 
            this.lblOrderReadInt1.AutoSize = true;
            this.lblOrderReadInt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderReadInt1.Location = new System.Drawing.Point(5, 55);
            this.lblOrderReadInt1.Name = "lblOrderReadInt1";
            this.lblOrderReadInt1.Size = new System.Drawing.Size(182, 16);
            this.lblOrderReadInt1.TabIndex = 8;
            this.lblOrderReadInt1.Text = "Read Orders from SAP every";
            // 
            // lblCartonSendInt2
            // 
            this.lblCartonSendInt2.AutoSize = true;
            this.lblCartonSendInt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCartonSendInt2.Location = new System.Drawing.Point(283, 25);
            this.lblCartonSendInt2.Name = "lblCartonSendInt2";
            this.lblCartonSendInt2.Size = new System.Drawing.Size(63, 16);
            this.lblCartonSendInt2.TabIndex = 7;
            this.lblCartonSendInt2.Text = "seconds.";
            // 
            // lblCartonSendInt1
            // 
            this.lblCartonSendInt1.AutoSize = true;
            this.lblCartonSendInt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCartonSendInt1.Location = new System.Drawing.Point(6, 25);
            this.lblCartonSendInt1.Name = "lblCartonSendInt1";
            this.lblCartonSendInt1.Size = new System.Drawing.Size(195, 16);
            this.lblCartonSendInt1.TabIndex = 6;
            this.lblCartonSendInt1.Text = "Send new cartons to SAP every";
            // 
            // numCartonSendInterval
            // 
            this.numCartonSendInterval.Location = new System.Drawing.Point(207, 23);
            this.numCartonSendInterval.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numCartonSendInterval.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numCartonSendInterval.Name = "numCartonSendInterval";
            this.numCartonSendInterval.Size = new System.Drawing.Size(70, 22);
            this.numCartonSendInterval.TabIndex = 0;
            this.numCartonSendInterval.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // txtOtherStationAddr
            // 
            this.txtOtherStationAddr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherStationAddr.Location = new System.Drawing.Point(184, 82);
            this.txtOtherStationAddr.Name = "txtOtherStationAddr";
            this.txtOtherStationAddr.Size = new System.Drawing.Size(242, 22);
            this.txtOtherStationAddr.TabIndex = 5;
            // 
            // lblOtherPackingStationAddr
            // 
            this.lblOtherPackingStationAddr.AutoSize = true;
            this.lblOtherPackingStationAddr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtherPackingStationAddr.Location = new System.Drawing.Point(14, 57);
            this.lblOtherPackingStationAddr.Name = "lblOtherPackingStationAddr";
            this.lblOtherPackingStationAddr.Size = new System.Drawing.Size(157, 16);
            this.lblOtherPackingStationAddr.TabIndex = 4;
            this.lblOtherPackingStationAddr.Text = "Other Packing Station";
            // 
            // lblOtherStationsAddr
            // 
            this.lblOtherStationsAddr.AutoSize = true;
            this.lblOtherStationsAddr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtherStationsAddr.Location = new System.Drawing.Point(14, 85);
            this.lblOtherStationsAddr.Name = "lblOtherStationsAddr";
            this.lblOtherStationsAddr.Size = new System.Drawing.Size(124, 16);
            this.lblOtherStationsAddr.TabIndex = 9;
            this.lblOtherStationsAddr.Text = "Hostname / IP Addr";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Port";
            // 
            // txtOtherStationPort
            // 
            this.txtOtherStationPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherStationPort.Location = new System.Drawing.Point(184, 110);
            this.txtOtherStationPort.Name = "txtOtherStationPort";
            this.txtOtherStationPort.Size = new System.Drawing.Size(62, 22);
            this.txtOtherStationPort.TabIndex = 11;
            // 
            // txtThisStationPort
            // 
            this.txtThisStationPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThisStationPort.Location = new System.Drawing.Point(184, 25);
            this.txtThisStationPort.Name = "txtThisStationPort";
            this.txtThisStationPort.Size = new System.Drawing.Size(62, 22);
            this.txtThisStationPort.TabIndex = 13;
            // 
            // lblThisStationsPort
            // 
            this.lblThisStationsPort.AutoSize = true;
            this.lblThisStationsPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThisStationsPort.Location = new System.Drawing.Point(14, 28);
            this.lblThisStationsPort.Name = "lblThisStationsPort";
            this.lblThisStationsPort.Size = new System.Drawing.Size(164, 16);
            this.lblThisStationsPort.TabIndex = 12;
            this.lblThisStationsPort.Text = "This Station Listens to Port";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Snow;
            this.groupBox1.Controls.Add(this.lblThisStationsPort);
            this.groupBox1.Controls.Add(this.txtThisStationPort);
            this.groupBox1.Controls.Add(this.txtOtherStationPort);
            this.groupBox1.Controls.Add(this.lblOtherPackingStationAddr);
            this.groupBox1.Controls.Add(this.txtOtherStationAddr);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblOtherStationsAddr);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(498, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 144);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inter Packing Station Communications";
            // 
            // dbSettingsCntrl1
            // 
            this.dbSettingsCntrl1.Location = new System.Drawing.Point(10, 3);
            this.dbSettingsCntrl1.Name = "dbSettingsCntrl1";
            this.dbSettingsCntrl1.Size = new System.Drawing.Size(482, 152);
            this.dbSettingsCntrl1.TabIndex = 20;
            // 
            // SettingsScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 675);
            this.Controls.Add(this.dbSettingsCntrl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gtpServiceSettings);
            this.Controls.Add(this.barcodeScannerSettingsCntrl1);
            this.Controls.Add(this.grpAppSettings);
            this.Controls.Add(this.labelPrinterSettingsCntrl1);
            this.Controls.Add(this.sapSettingsCntrl1);
            this.Controls.Add(this.scaleSettingsCntrl1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsScreen";
            this.Text = "Maintain Customising Settings for StandAlone Packing Application";
            this.grpAppSettings.ResumeLayout(false);
            this.grpAppSettings.PerformLayout();
            this.gtpServiceSettings.ResumeLayout(false);
            this.gtpServiceSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaterialReadInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderReadInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCartonSendInterval)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private ShopFloorLib.ScaleSettingsCntrl scaleSettingsCntrl1;
        private ShopFloorLib.SAPSettingsCntrl sapSettingsCntrl1;
        private ShopFloorLib.LabelPrinterSettingsCntrl labelPrinterSettingsCntrl1;
        private System.Windows.Forms.GroupBox grpAppSettings;
        private System.Windows.Forms.CheckBox chkBxReadOrders;
        private System.Windows.Forms.CheckBox chkBxReadMats;
        private ShopFloorLib.BarcodeScannerSettingsCntrl barcodeScannerSettingsCntrl1;
        private System.Windows.Forms.GroupBox gtpServiceSettings;
        private System.Windows.Forms.Label lblMaterialReadInt2;
        private System.Windows.Forms.NumericUpDown numMaterialReadInt;
        private System.Windows.Forms.Label lblOrderReadInt2;
        private System.Windows.Forms.NumericUpDown numOrderReadInt;
        private System.Windows.Forms.Label lblMaterialReadInt1;
        private System.Windows.Forms.Label lblOrderReadInt1;
        private System.Windows.Forms.Label lblCartonSendInt2;
        private System.Windows.Forms.Label lblCartonSendInt1;
        private System.Windows.Forms.NumericUpDown numCartonSendInterval;
        private System.Windows.Forms.TextBox txtOtherStationAddr;
        private System.Windows.Forms.Label lblOtherPackingStationAddr;
        private System.Windows.Forms.Label lblOtherStationsAddr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOtherStationPort;
        private System.Windows.Forms.TextBox txtThisStationPort;
        private System.Windows.Forms.Label lblThisStationsPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private ShopFloorLib.DBSettingsCntrl dbSettingsCntrl1;
    }
}