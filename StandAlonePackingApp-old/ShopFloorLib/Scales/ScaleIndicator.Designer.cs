namespace ShopFloorLib
{
    partial class ScaleIndicator
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
            this.lblNet = new System.Windows.Forms.Label();
            this.lblTare = new System.Windows.Forms.Label();
            this.lblGross = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // lblNet
            // 
            this.lblNet.AutoSize = true;
            this.lblNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNet.Location = new System.Drawing.Point(28, 11);
            this.lblNet.Name = "lblNet";
            this.lblNet.Size = new System.Drawing.Size(161, 25);
            this.lblNet.TabIndex = 0;
            this.lblNet.Text = "Net: xxx.xx kg";
            this.lblNet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTare
            // 
            this.lblTare.AutoSize = true;
            this.lblTare.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTare.Location = new System.Drawing.Point(3, 46);
            this.lblTare.Name = "lblTare";
            this.lblTare.Size = new System.Drawing.Size(81, 16);
            this.lblTare.TabIndex = 1;
            this.lblTare.Text = "Tare: xx.xx";
            this.lblTare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGross
            // 
            this.lblGross.AutoSize = true;
            this.lblGross.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGross.Location = new System.Drawing.Point(90, 46);
            this.lblGross.Name = "lblGross";
            this.lblGross.Size = new System.Drawing.Size(96, 16);
            this.lblGross.TabIndex = 2;
            this.lblGross.Text = "Gross: xxx.xx";
            this.lblGross.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // ScaleIndicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.Controls.Add(this.lblGross);
            this.Controls.Add(this.lblTare);
            this.Controls.Add(this.lblNet);
            this.Name = "ScaleIndicator";
            this.Size = new System.Drawing.Size(217, 76);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNet;
        private System.Windows.Forms.Label lblTare;
        private System.Windows.Forms.Label lblGross;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
