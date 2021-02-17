namespace StandAlonePackingApp
{
    partial class TareWeightForBinDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TareWeightForBinDialog));
            this.numericKeypad1 = new ShopFloorLib.NumericKeypad();
            this.lblTare = new System.Windows.Forms.Label();
            this.txtBxTare = new System.Windows.Forms.TextBox();
            this.lblKg = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // numericKeypad1
            // 
            this.numericKeypad1.Location = new System.Drawing.Point(25, 36);
            this.numericKeypad1.Name = "numericKeypad1";
            this.numericKeypad1.Size = new System.Drawing.Size(303, 323);
            this.numericKeypad1.TabIndex = 0;
            // 
            // lblTare
            // 
            this.lblTare.AutoSize = true;
            this.lblTare.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTare.Location = new System.Drawing.Point(366, 36);
            this.lblTare.Name = "lblTare";
            this.lblTare.Size = new System.Drawing.Size(45, 16);
            this.lblTare.TabIndex = 1;
            this.lblTare.Text = "Tare:";
            // 
            // txtBxTare
            // 
            this.txtBxTare.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxTare.Location = new System.Drawing.Point(417, 35);
            this.txtBxTare.Name = "txtBxTare";
            this.txtBxTare.ReadOnly = true;
            this.txtBxTare.Size = new System.Drawing.Size(100, 22);
            this.txtBxTare.TabIndex = 2;
            // 
            // lblKg
            // 
            this.lblKg.AutoSize = true;
            this.lblKg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKg.Location = new System.Drawing.Point(517, 35);
            this.lblKg.Name = "lblKg";
            this.lblKg.Size = new System.Drawing.Size(26, 16);
            this.lblKg.TabIndex = 3;
            this.lblKg.Text = "Kg";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Crimson;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(427, 310);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 40);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // TareWeightForBinDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 387);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblKg);
            this.Controls.Add(this.txtBxTare);
            this.Controls.Add(this.lblTare);
            this.Controls.Add(this.numericKeypad1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TareWeightForBinDialog";
            this.Text = "Enter Bin Tare Weight";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ShopFloorLib.NumericKeypad numericKeypad1;
        private System.Windows.Forms.Label lblTare;
        private System.Windows.Forms.TextBox txtBxTare;
        private System.Windows.Forms.Label lblKg;
        private System.Windows.Forms.Button btnCancel;
    }
}