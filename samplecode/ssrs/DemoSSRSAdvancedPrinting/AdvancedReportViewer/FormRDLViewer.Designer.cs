namespace AdvancedReportViewer
{
    partial class FormRDLViewer
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelXmlNamespace = new System.Windows.Forms.Label();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.labelPageSettings = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox1.Location = new System.Drawing.Point(12, 71);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(573, 618);
            this.textBox1.TabIndex = 0;
            // 
            // labelXmlNamespace
            // 
            this.labelXmlNamespace.AutoSize = true;
            this.labelXmlNamespace.Location = new System.Drawing.Point(12, 9);
            this.labelXmlNamespace.Name = "labelXmlNamespace";
            this.labelXmlNamespace.Size = new System.Drawing.Size(97, 13);
            this.labelXmlNamespace.TabIndex = 1;
            this.labelXmlNamespace.Text = "{XML Namespace}";
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Location = new System.Drawing.Point(480, 42);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectAll.TabIndex = 2;
            this.buttonSelectAll.Text = "Select All";
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.buttonSelectAll_Click);
            // 
            // labelPageSettings
            // 
            this.labelPageSettings.AutoSize = true;
            this.labelPageSettings.Location = new System.Drawing.Point(12, 42);
            this.labelPageSettings.Name = "labelPageSettings";
            this.labelPageSettings.Size = new System.Drawing.Size(81, 13);
            this.labelPageSettings.TabIndex = 3;
            this.labelPageSettings.Text = "{Page Settings}";
            // 
            // FormRDLViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 701);
            this.Controls.Add(this.labelPageSettings);
            this.Controls.Add(this.buttonSelectAll);
            this.Controls.Add(this.labelXmlNamespace);
            this.Controls.Add(this.textBox1);
            this.Name = "FormRDLViewer";
            this.Text = "RDL Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelXmlNamespace;
        private System.Windows.Forms.Button buttonSelectAll;
        private System.Windows.Forms.Label labelPageSettings;
    }
}