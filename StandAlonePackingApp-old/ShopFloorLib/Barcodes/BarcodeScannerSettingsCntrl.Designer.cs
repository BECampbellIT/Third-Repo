namespace ShopFloorLib
{
    partial class BarcodeScannerSettingsCntrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmboBxInput = new System.Windows.Forms.ComboBox();
            this.lblInput = new System.Windows.Forms.Label();
            this.cmboBxSuffix = new System.Windows.Forms.ComboBox();
            this.lblSuffix = new System.Windows.Forms.Label();
            this.cmboBxPrefix = new System.Windows.Forms.ComboBox();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.grpBxCOMPort = new System.Windows.Forms.GroupBox();
            this.cmboBxParity = new System.Windows.Forms.ComboBox();
            this.lblParity = new System.Windows.Forms.Label();
            this.cmboBxStopBits = new System.Windows.Forms.ComboBox();
            this.lblStopBits = new System.Windows.Forms.Label();
            this.cmboBxDataBits = new System.Windows.Forms.ComboBox();
            this.lblDataBits = new System.Windows.Forms.Label();
            this.cmboBxBaudRate = new System.Windows.Forms.ComboBox();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.grpBxCOMPort.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Snow;
            this.groupBox1.Controls.Add(this.grpBxCOMPort);
            this.groupBox1.Controls.Add(this.cmboBxInput);
            this.groupBox1.Controls.Add(this.lblInput);
            this.groupBox1.Controls.Add(this.cmboBxSuffix);
            this.groupBox1.Controls.Add(this.lblSuffix);
            this.groupBox1.Controls.Add(this.cmboBxPrefix);
            this.groupBox1.Controls.Add(this.lblPrefix);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 205);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Barcode Scanner Settings";
            // 
            // cmboBxInput
            // 
            this.cmboBxInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboBxInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboBxInput.FormattingEnabled = true;
            this.cmboBxInput.Items.AddRange(new object[] {
            "Keyboard",
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.cmboBxInput.Location = new System.Drawing.Point(118, 58);
            this.cmboBxInput.Name = "cmboBxInput";
            this.cmboBxInput.Size = new System.Drawing.Size(109, 24);
            this.cmboBxInput.TabIndex = 5;
            this.cmboBxInput.SelectedIndexChanged += new System.EventHandler(this.cmboBxInput_SelectedIndexChanged);
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInput.Location = new System.Drawing.Point(7, 61);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(110, 16);
            this.lblInput.TabIndex = 4;
            this.lblInput.Text = "Read Input From:";
            // 
            // cmboBxSuffix
            // 
            this.cmboBxSuffix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboBxSuffix.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboBxSuffix.FormattingEnabled = true;
            this.cmboBxSuffix.Items.AddRange(new object[] {
            ")",
            "]",
            "}",
            "\\",
            "Enter"});
            this.cmboBxSuffix.Location = new System.Drawing.Point(344, 22);
            this.cmboBxSuffix.Name = "cmboBxSuffix";
            this.cmboBxSuffix.Size = new System.Drawing.Size(76, 24);
            this.cmboBxSuffix.TabIndex = 3;
            // 
            // lblSuffix
            // 
            this.lblSuffix.AutoSize = true;
            this.lblSuffix.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuffix.Location = new System.Drawing.Point(236, 25);
            this.lblSuffix.Name = "lblSuffix";
            this.lblSuffix.Size = new System.Drawing.Size(103, 16);
            this.lblSuffix.TabIndex = 2;
            this.lblSuffix.Text = "Suffix Character:";
            // 
            // cmboBxPrefix
            // 
            this.cmboBxPrefix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboBxPrefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboBxPrefix.FormattingEnabled = true;
            this.cmboBxPrefix.Items.AddRange(new object[] {
            "(",
            "[",
            "{",
            "<",
            "/",
            "AIM Code"});
            this.cmboBxPrefix.Location = new System.Drawing.Point(118, 22);
            this.cmboBxPrefix.Name = "cmboBxPrefix";
            this.cmboBxPrefix.Size = new System.Drawing.Size(109, 24);
            this.cmboBxPrefix.TabIndex = 1;
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrefix.Location = new System.Drawing.Point(7, 25);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(105, 16);
            this.lblPrefix.TabIndex = 0;
            this.lblPrefix.Text = "Prefix Character:";
            // 
            // grpBxCOMPort
            // 
            this.grpBxCOMPort.Controls.Add(this.cmboBxParity);
            this.grpBxCOMPort.Controls.Add(this.lblParity);
            this.grpBxCOMPort.Controls.Add(this.cmboBxStopBits);
            this.grpBxCOMPort.Controls.Add(this.lblStopBits);
            this.grpBxCOMPort.Controls.Add(this.cmboBxDataBits);
            this.grpBxCOMPort.Controls.Add(this.lblDataBits);
            this.grpBxCOMPort.Controls.Add(this.cmboBxBaudRate);
            this.grpBxCOMPort.Controls.Add(this.lblBaudRate);
            this.grpBxCOMPort.Location = new System.Drawing.Point(7, 101);
            this.grpBxCOMPort.Name = "grpBxCOMPort";
            this.grpBxCOMPort.Size = new System.Drawing.Size(462, 97);
            this.grpBxCOMPort.TabIndex = 6;
            this.grpBxCOMPort.TabStop = false;
            this.grpBxCOMPort.Text = "Serial Settings";
            // 
            // cmboBxParity
            // 
            this.cmboBxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboBxParity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboBxParity.FormattingEnabled = true;
            this.cmboBxParity.Location = new System.Drawing.Point(324, 58);
            this.cmboBxParity.Name = "cmboBxParity";
            this.cmboBxParity.Size = new System.Drawing.Size(128, 24);
            this.cmboBxParity.TabIndex = 12;
            // 
            // lblParity
            // 
            this.lblParity.AutoSize = true;
            this.lblParity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParity.Location = new System.Drawing.Point(254, 61);
            this.lblParity.Name = "lblParity";
            this.lblParity.Size = new System.Drawing.Size(45, 16);
            this.lblParity.TabIndex = 11;
            this.lblParity.Text = "Parity:";
            // 
            // cmboBxStopBits
            // 
            this.cmboBxStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboBxStopBits.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboBxStopBits.FormattingEnabled = true;
            this.cmboBxStopBits.Location = new System.Drawing.Point(322, 25);
            this.cmboBxStopBits.Name = "cmboBxStopBits";
            this.cmboBxStopBits.Size = new System.Drawing.Size(130, 24);
            this.cmboBxStopBits.TabIndex = 10;
            // 
            // lblStopBits
            // 
            this.lblStopBits.AutoSize = true;
            this.lblStopBits.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStopBits.Location = new System.Drawing.Point(251, 28);
            this.lblStopBits.Name = "lblStopBits";
            this.lblStopBits.Size = new System.Drawing.Size(64, 16);
            this.lblStopBits.TabIndex = 9;
            this.lblStopBits.Text = "Stop Bits:";
            // 
            // cmboBxDataBits
            // 
            this.cmboBxDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboBxDataBits.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboBxDataBits.FormattingEnabled = true;
            this.cmboBxDataBits.Items.AddRange(new object[] {
            "7",
            "8"});
            this.cmboBxDataBits.Location = new System.Drawing.Point(102, 54);
            this.cmboBxDataBits.Name = "cmboBxDataBits";
            this.cmboBxDataBits.Size = new System.Drawing.Size(113, 24);
            this.cmboBxDataBits.TabIndex = 8;
            // 
            // lblDataBits
            // 
            this.lblDataBits.AutoSize = true;
            this.lblDataBits.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataBits.Location = new System.Drawing.Point(19, 57);
            this.lblDataBits.Name = "lblDataBits";
            this.lblDataBits.Size = new System.Drawing.Size(65, 16);
            this.lblDataBits.TabIndex = 7;
            this.lblDataBits.Text = "Data Bits:";
            // 
            // cmboBxBaudRate
            // 
            this.cmboBxBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboBxBaudRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboBxBaudRate.FormattingEnabled = true;
            this.cmboBxBaudRate.Items.AddRange(new object[] {
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600"});
            this.cmboBxBaudRate.Location = new System.Drawing.Point(101, 22);
            this.cmboBxBaudRate.Name = "cmboBxBaudRate";
            this.cmboBxBaudRate.Size = new System.Drawing.Size(114, 24);
            this.cmboBxBaudRate.TabIndex = 6;
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaudRate.Location = new System.Drawing.Point(17, 28);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(75, 16);
            this.lblBaudRate.TabIndex = 4;
            this.lblBaudRate.Text = "Baud Rate:";
            // 
            // BarcodeScannerSettingsCntrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "BarcodeScannerSettingsCntrl";
            this.Size = new System.Drawing.Size(480, 229);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpBxCOMPort.ResumeLayout(false);
            this.grpBxCOMPort.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmboBxPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.ComboBox cmboBxInput;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.ComboBox cmboBxSuffix;
        private System.Windows.Forms.Label lblSuffix;
        private System.Windows.Forms.GroupBox grpBxCOMPort;
        private System.Windows.Forms.ComboBox cmboBxParity;
        private System.Windows.Forms.Label lblParity;
        private System.Windows.Forms.ComboBox cmboBxStopBits;
        private System.Windows.Forms.Label lblStopBits;
        private System.Windows.Forms.ComboBox cmboBxDataBits;
        private System.Windows.Forms.Label lblDataBits;
        private System.Windows.Forms.ComboBox cmboBxBaudRate;
        private System.Windows.Forms.Label lblBaudRate;
    }
}
