namespace ScaleSimulator
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.numWeight = new System.Windows.Forms.NumericUpDown();
            this.lblKg = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.radBtnStable = new System.Windows.Forms.RadioButton();
            this.radBtnUnstable = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gross Weight:";
            // 
            // numWeight
            // 
            this.numWeight.DecimalPlaces = 2;
            this.numWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numWeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numWeight.Location = new System.Drawing.Point(161, 30);
            this.numWeight.Maximum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.numWeight.Name = "numWeight";
            this.numWeight.Size = new System.Drawing.Size(120, 26);
            this.numWeight.TabIndex = 1;
            this.numWeight.ThousandsSeparator = true;
            // 
            // lblKg
            // 
            this.lblKg.AutoSize = true;
            this.lblKg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKg.Location = new System.Drawing.Point(287, 36);
            this.lblKg.Name = "lblKg";
            this.lblKg.Size = new System.Drawing.Size(30, 20);
            this.lblKg.TabIndex = 2;
            this.lblKg.Text = "Kg";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Crimson;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(273, 99);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 31);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.button1_Click);
            // 
            // radBtnStable
            // 
            this.radBtnStable.AutoSize = true;
            this.radBtnStable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radBtnStable.Location = new System.Drawing.Point(27, 75);
            this.radBtnStable.Name = "radBtnStable";
            this.radBtnStable.Size = new System.Drawing.Size(123, 20);
            this.radBtnStable.TabIndex = 4;
            this.radBtnStable.TabStop = true;
            this.radBtnStable.Text = "Stable Weight";
            this.radBtnStable.UseVisualStyleBackColor = true;
            // 
            // radBtnUnstable
            // 
            this.radBtnUnstable.AutoSize = true;
            this.radBtnUnstable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radBtnUnstable.Location = new System.Drawing.Point(27, 99);
            this.radBtnUnstable.Name = "radBtnUnstable";
            this.radBtnUnstable.Size = new System.Drawing.Size(140, 20);
            this.radBtnUnstable.TabIndex = 5;
            this.radBtnUnstable.TabStop = true;
            this.radBtnUnstable.Text = "Unstable Weight";
            this.radBtnUnstable.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 160);
            this.Controls.Add(this.radBtnUnstable);
            this.Controls.Add(this.radBtnStable);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblKg);
            this.Controls.Add(this.numWeight);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Scale Simulator";
            ((System.ComponentModel.ISupportInitialize)(this.numWeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numWeight;
        private System.Windows.Forms.Label lblKg;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RadioButton radBtnStable;
        private System.Windows.Forms.RadioButton radBtnUnstable;
    }
}

