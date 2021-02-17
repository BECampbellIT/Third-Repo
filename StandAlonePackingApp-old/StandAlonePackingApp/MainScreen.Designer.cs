namespace StandAlonePackingApp
{
    partial class MainScreen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            this.btnExit = new System.Windows.Forms.Button();
            this.lblFilter = new System.Windows.Forms.Label();
            this.btnRecord = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblCustLabel = new System.Windows.Forms.Label();
            this.lblRange = new System.Windows.Forms.Label();
            this.lblWeightLabel = new System.Windows.Forms.Label();
            this.lblQtyLabel = new System.Windows.Forms.Label();
            this.lblMatLabel = new System.Windows.Forms.Label();
            this.lblMat = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.grpBoxSelectedMaterial = new System.Windows.Forms.GroupBox();
            this.btnSwitchMode = new System.Windows.Forms.Button();
            this.btnActions = new System.Windows.Forms.Button();
            this.lblCurrMode = new System.Windows.Forms.Label();
            this.txtCurrMode = new System.Windows.Forms.TextBox();
            this.btnChooseSlDate = new System.Windows.Forms.Button();
            this.numericKeypad1 = new ShopFloorLib.NumericKeypad();
            this.buttonMatrix1 = new ShopFloorLib.ButtonMatrix();
            this.messageLog1 = new ShopFloorLib.MessageLogCntrl();
            this.scaleIndicator1 = new ShopFloorLib.ScaleIndicator();
            this.barcodeScanner1 = new ShopFloorLib.BarcodeScanner(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpBoxSelectedMaterial.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Crimson;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(859, 640);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 40);
            this.btnExit.TabIndex = 26;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(574, 229);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(45, 16);
            this.lblFilter.TabIndex = 27;
            this.lblFilter.Text = "Filter:";
            // 
            // btnRecord
            // 
            this.btnRecord.BackColor = System.Drawing.Color.Yellow;
            this.btnRecord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecord.Location = new System.Drawing.Point(577, 587);
            this.btnRecord.Margin = new System.Windows.Forms.Padding(4);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(372, 45);
            this.btnRecord.TabIndex = 29;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = false;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.PaleTurquoise;
            this.label2.Location = new System.Drawing.Point(625, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 16);
            this.label2.TabIndex = 35;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.39056F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.60944F));
            this.tableLayoutPanel1.Controls.Add(this.lblCustomer, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblCustLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblRange, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblWeightLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblQtyLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblMatLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblMat, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblQty, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(466, 108);
            this.tableLayoutPanel1.TabIndex = 36;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.Location = new System.Drawing.Point(112, 81);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(176, 16);
            this.lblCustomer.TabIndex = 42;
            this.lblCustomer.Text = "VBIN - Vinnie\'s Bargin Meats";
            // 
            // lblCustLabel
            // 
            this.lblCustLabel.AutoSize = true;
            this.lblCustLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustLabel.Location = new System.Drawing.Point(3, 81);
            this.lblCustLabel.Name = "lblCustLabel";
            this.lblCustLabel.Size = new System.Drawing.Size(72, 16);
            this.lblCustLabel.TabIndex = 41;
            this.lblCustLabel.Text = "Customer:";
            // 
            // lblRange
            // 
            this.lblRange.AutoSize = true;
            this.lblRange.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRange.Location = new System.Drawing.Point(112, 54);
            this.lblRange.Name = "lblRange";
            this.lblRange.Size = new System.Drawing.Size(105, 16);
            this.lblRange.TabIndex = 40;
            this.lblRange.Text = "15.5kg to 17.5kg";
            // 
            // lblWeightLabel
            // 
            this.lblWeightLabel.AutoSize = true;
            this.lblWeightLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeightLabel.Location = new System.Drawing.Point(3, 54);
            this.lblWeightLabel.Name = "lblWeightLabel";
            this.lblWeightLabel.Size = new System.Drawing.Size(102, 16);
            this.lblWeightLabel.TabIndex = 39;
            this.lblWeightLabel.Text = "Weight Range:";
            // 
            // lblQtyLabel
            // 
            this.lblQtyLabel.AutoSize = true;
            this.lblQtyLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtyLabel.Location = new System.Drawing.Point(3, 27);
            this.lblQtyLabel.Name = "lblQtyLabel";
            this.lblQtyLabel.Size = new System.Drawing.Size(84, 16);
            this.lblQtyLabel.TabIndex = 38;
            this.lblQtyLabel.Text = "Qty Packed:";
            // 
            // lblMatLabel
            // 
            this.lblMatLabel.AutoSize = true;
            this.lblMatLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatLabel.Location = new System.Drawing.Point(3, 0);
            this.lblMatLabel.Name = "lblMatLabel";
            this.lblMatLabel.Size = new System.Drawing.Size(64, 16);
            this.lblMatLabel.TabIndex = 0;
            this.lblMatLabel.Text = "Material:";
            // 
            // lblMat
            // 
            this.lblMat.AutoSize = true;
            this.lblMat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMat.Location = new System.Drawing.Point(112, 0);
            this.lblMat.Name = "lblMat";
            this.lblMat.Size = new System.Drawing.Size(177, 16);
            this.lblMat.TabIndex = 1;
            this.lblMat.Text = "3xxxxx - Material Description";
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.Location = new System.Drawing.Point(112, 27);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(182, 16);
            this.lblQty.TabIndex = 3;
            this.lblQty.Text = "45 / 50 - Maximum allowed 55";
            // 
            // grpBoxSelectedMaterial
            // 
            this.grpBoxSelectedMaterial.Controls.Add(this.tableLayoutPanel1);
            this.grpBoxSelectedMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpBoxSelectedMaterial.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxSelectedMaterial.Location = new System.Drawing.Point(509, 99);
            this.grpBoxSelectedMaterial.Name = "grpBoxSelectedMaterial";
            this.grpBoxSelectedMaterial.Size = new System.Drawing.Size(472, 127);
            this.grpBoxSelectedMaterial.TabIndex = 37;
            this.grpBoxSelectedMaterial.TabStop = false;
            this.grpBoxSelectedMaterial.Text = "Selected Material";
            // 
            // btnSwitchMode
            // 
            this.btnSwitchMode.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnSwitchMode.Location = new System.Drawing.Point(227, 641);
            this.btnSwitchMode.Name = "btnSwitchMode";
            this.btnSwitchMode.Size = new System.Drawing.Size(130, 40);
            this.btnSwitchMode.TabIndex = 39;
            this.btnSwitchMode.Text = "Switch Mode";
            this.btnSwitchMode.UseVisualStyleBackColor = false;
            this.btnSwitchMode.Click += new System.EventHandler(this.btnSwitchMode_Click);
            // 
            // btnActions
            // 
            this.btnActions.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnActions.Location = new System.Drawing.Point(372, 641);
            this.btnActions.Name = "btnActions";
            this.btnActions.Size = new System.Drawing.Size(130, 40);
            this.btnActions.TabIndex = 40;
            this.btnActions.Text = "Actions";
            this.btnActions.UseVisualStyleBackColor = false;
            this.btnActions.Click += new System.EventHandler(this.btnActions_Click);
            // 
            // lblCurrMode
            // 
            this.lblCurrMode.AutoSize = true;
            this.lblCurrMode.Location = new System.Drawing.Point(4, 650);
            this.lblCurrMode.Name = "lblCurrMode";
            this.lblCurrMode.Size = new System.Drawing.Size(47, 16);
            this.lblCurrMode.TabIndex = 41;
            this.lblCurrMode.Text = "Mode:";
            // 
            // txtCurrMode
            // 
            this.txtCurrMode.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrMode.Location = new System.Drawing.Point(67, 645);
            this.txtCurrMode.Name = "txtCurrMode";
            this.txtCurrMode.ReadOnly = true;
            this.txtCurrMode.Size = new System.Drawing.Size(136, 26);
            this.txtCurrMode.TabIndex = 42;
            this.txtCurrMode.TabStop = false;
            this.txtCurrMode.Text = "Normal Packing";
            this.txtCurrMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnChooseSlDate
            // 
            this.btnChooseSlDate.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnChooseSlDate.Location = new System.Drawing.Point(577, 641);
            this.btnChooseSlDate.Name = "btnChooseSlDate";
            this.btnChooseSlDate.Size = new System.Drawing.Size(224, 40);
            this.btnChooseSlDate.TabIndex = 43;
            this.btnChooseSlDate.Text = "Pack Date: DD.MM.YYYY\r\nSl.Dates: 01-03/03/2017";
            this.btnChooseSlDate.UseVisualStyleBackColor = false;
            this.btnChooseSlDate.Click += new System.EventHandler(this.btnChooseSlDate_Click);
            // 
            // numericKeypad1
            // 
            this.numericKeypad1.Location = new System.Drawing.Point(577, 249);
            this.numericKeypad1.Margin = new System.Windows.Forms.Padding(4);
            this.numericKeypad1.Name = "numericKeypad1";
            this.numericKeypad1.Size = new System.Drawing.Size(372, 331);
            this.numericKeypad1.TabIndex = 34;
            // 
            // buttonMatrix1
            // 
            this.buttonMatrix1.Location = new System.Drawing.Point(13, 182);
            this.buttonMatrix1.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMatrix1.Name = "buttonMatrix1";
            this.buttonMatrix1.Size = new System.Drawing.Size(489, 450);
            this.buttonMatrix1.TabIndex = 33;
            // 
            // messageLog1
            // 
            this.messageLog1.Location = new System.Drawing.Point(13, 14);
            this.messageLog1.Margin = new System.Windows.Forms.Padding(4);
            this.messageLog1.MaximumSize = new System.Drawing.Size(800, 738);
            this.messageLog1.MinimumSize = new System.Drawing.Size(267, 62);
            this.messageLog1.Name = "messageLog1";
            this.messageLog1.Size = new System.Drawing.Size(479, 150);
            this.messageLog1.TabIndex = 32;
            // 
            // scaleIndicator1
            // 
            this.scaleIndicator1.BackColor = System.Drawing.Color.Green;
            this.scaleIndicator1.Location = new System.Drawing.Point(715, 14);
            this.scaleIndicator1.Margin = new System.Windows.Forms.Padding(4);
            this.scaleIndicator1.Name = "scaleIndicator1";
            this.scaleIndicator1.Size = new System.Drawing.Size(251, 78);
            this.scaleIndicator1.TabIndex = 31;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // txtUserName
            // 
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(512, 14);
            this.txtUserName.Multiline = true;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(187, 48);
            this.txtUserName.TabIndex = 44;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.btnChooseSlDate);
            this.Controls.Add(this.txtCurrMode);
            this.Controls.Add(this.lblCurrMode);
            this.Controls.Add(this.btnActions);
            this.Controls.Add(this.btnSwitchMode);
            this.Controls.Add(this.grpBoxSelectedMaterial);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericKeypad1);
            this.Controls.Add(this.buttonMatrix1);
            this.Controls.Add(this.messageLog1);
            this.Controls.Add(this.scaleIndicator1);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.btnExit);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainScreen";
            this.Text = "Standalone Packing Application";
            this.Load += new System.EventHandler(this.MainScreen_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.grpBoxSelectedMaterial.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Button btnRecord;
        private ShopFloorLib.ScaleIndicator scaleIndicator1;
        private ShopFloorLib.MessageLogCntrl messageLog1;
        private ShopFloorLib.ButtonMatrix buttonMatrix1;
        private ShopFloorLib.NumericKeypad numericKeypad1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblRange;
        private System.Windows.Forms.Label lblWeightLabel;
        private System.Windows.Forms.Label lblQtyLabel;
        private System.Windows.Forms.Label lblMatLabel;
        private System.Windows.Forms.Label lblMat;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.GroupBox grpBoxSelectedMaterial;
        private System.Windows.Forms.Button btnSwitchMode;
        private System.Windows.Forms.Button btnActions;
        private System.Windows.Forms.Label lblCurrMode;
        private System.Windows.Forms.TextBox txtCurrMode;
        private System.Windows.Forms.Button btnChooseSlDate;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblCustLabel;
        private ShopFloorLib.BarcodeScanner barcodeScanner1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox txtUserName;
    }
}