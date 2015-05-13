namespace GetColorsFromBitmap
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
            this.textBoxInputBitmap = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxOutputFile = new System.Windows.Forms.TextBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonBrowseInput = new System.Windows.Forms.Button();
            this.buttonBrowseOutput = new System.Windows.Forms.Button();
            this.checkBoxUseOnlyOpauqePixels = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBoxInputBitmap
            // 
            this.textBoxInputBitmap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInputBitmap.Location = new System.Drawing.Point(12, 50);
            this.textBoxInputBitmap.Name = "textBoxInputBitmap";
            this.textBoxInputBitmap.Size = new System.Drawing.Size(520, 20);
            this.textBoxInputBitmap.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input Bitmap";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output filename";
            // 
            // textBoxOutputFile
            // 
            this.textBoxOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputFile.Location = new System.Drawing.Point(12, 110);
            this.textBoxOutputFile.Name = "textBoxOutputFile";
            this.textBoxOutputFile.Size = new System.Drawing.Size(520, 20);
            this.textBoxOutputFile.TabIndex = 2;
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(534, 153);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(75, 23);
            this.buttonRun.TabIndex = 4;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonBrowseInput
            // 
            this.buttonBrowseInput.Location = new System.Drawing.Point(539, 48);
            this.buttonBrowseInput.Name = "buttonBrowseInput";
            this.buttonBrowseInput.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseInput.TabIndex = 5;
            this.buttonBrowseInput.Text = "Browse";
            this.buttonBrowseInput.UseVisualStyleBackColor = true;
            this.buttonBrowseInput.Click += new System.EventHandler(this.buttonBrowseInput_Click);
            // 
            // buttonBrowseOutput
            // 
            this.buttonBrowseOutput.Location = new System.Drawing.Point(539, 108);
            this.buttonBrowseOutput.Name = "buttonBrowseOutput";
            this.buttonBrowseOutput.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseOutput.TabIndex = 6;
            this.buttonBrowseOutput.Text = "Browse";
            this.buttonBrowseOutput.UseVisualStyleBackColor = true;
            this.buttonBrowseOutput.Click += new System.EventHandler(this.buttonBrowseOutput_Click);
            // 
            // checkBoxUseOnlyOpauqePixels
            // 
            this.checkBoxUseOnlyOpauqePixels.AutoSize = true;
            this.checkBoxUseOnlyOpauqePixels.Location = new System.Drawing.Point(13, 153);
            this.checkBoxUseOnlyOpauqePixels.Name = "checkBoxUseOnlyOpauqePixels";
            this.checkBoxUseOnlyOpauqePixels.Size = new System.Drawing.Size(194, 17);
            this.checkBoxUseOnlyOpauqePixels.TabIndex = 7;
            this.checkBoxUseOnlyOpauqePixels.Text = "Only use opaque pixels (alpha=255)";
            this.checkBoxUseOnlyOpauqePixels.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 194);
            this.Controls.Add(this.checkBoxUseOnlyOpauqePixels);
            this.Controls.Add(this.buttonBrowseOutput);
            this.Controls.Add(this.buttonBrowseInput);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxOutputFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxInputBitmap);
            this.Name = "Form1";
            this.Text = "Colors from bitmap";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxInputBitmap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxOutputFile;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonBrowseInput;
        private System.Windows.Forms.Button buttonBrowseOutput;
        private System.Windows.Forms.CheckBox checkBoxUseOnlyOpauqePixels;
    }
}

