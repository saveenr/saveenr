namespace IsotopeUITestApp
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
            this.colorSelectorLarge1 = new Isotope.UI.CommonControls.ColorSelectorLarge();
            this.SuspendLayout();
            // 
            // colorSelectorLarge1
            // 
            this.colorSelectorLarge1.BackColor = System.Drawing.Color.White;
            this.colorSelectorLarge1.Color = System.Drawing.Color.White;
            this.colorSelectorLarge1.Location = new System.Drawing.Point(77, 70);
            this.colorSelectorLarge1.Name = "colorSelectorLarge1";
            this.colorSelectorLarge1.Size = new System.Drawing.Size(282, 259);
            this.colorSelectorLarge1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 369);
            this.Controls.Add(this.colorSelectorLarge1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Isotope.UI.CommonControls.ColorSelectorLarge colorSelectorLarge1;
    }
}

