namespace ShopFloorLib
{
    partial class NumericKeypadDialog
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
            this.numericKeypad1 = new ShopFloorLib.NumericKeypad();
            this.SuspendLayout();
            // 
            // numericKeypad1
            // 
            this.numericKeypad1.Location = new System.Drawing.Point(1, 12);
            this.numericKeypad1.Name = "numericKeypad1";
            this.numericKeypad1.Size = new System.Drawing.Size(409, 343);
            this.numericKeypad1.TabIndex = 0;
            // 
            // NumericKeypadDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 375);
            this.Controls.Add(this.numericKeypad1);
            this.Name = "NumericKeypadDialog";
            this.Text = "NumericKeypadDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private NumericKeypad numericKeypad1;
    }
}