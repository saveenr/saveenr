namespace DemoMergeImagesSideBySide
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
            this.TextRun = new System.Windows.Forms.Button();
            this.textBoxSrcFolders = new System.Windows.Forms.TextBox();
            this.textBoxOutputFolder = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TextRun
            // 
            this.TextRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TextRun.Location = new System.Drawing.Point(668, 244);
            this.TextRun.Name = "TextRun";
            this.TextRun.Size = new System.Drawing.Size(75, 23);
            this.TextRun.TabIndex = 0;
            this.TextRun.Text = "Run";
            this.TextRun.UseVisualStyleBackColor = true;
            this.TextRun.Click += new System.EventHandler(this.TextRun_Click);
            // 
            // textBoxSrcFolders
            // 
            this.textBoxSrcFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSrcFolders.Location = new System.Drawing.Point(13, 45);
            this.textBoxSrcFolders.Multiline = true;
            this.textBoxSrcFolders.Name = "textBoxSrcFolders";
            this.textBoxSrcFolders.Size = new System.Drawing.Size(730, 156);
            this.textBoxSrcFolders.TabIndex = 1;
            // 
            // textBoxOutputFolder
            // 
            this.textBoxOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputFolder.Location = new System.Drawing.Point(13, 218);
            this.textBoxOutputFolder.Name = "textBoxOutputFolder";
            this.textBoxOutputFolder.Size = new System.Drawing.Size(730, 20);
            this.textBoxOutputFolder.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 279);
            this.Controls.Add(this.textBoxOutputFolder);
            this.Controls.Add(this.textBoxSrcFolders);
            this.Controls.Add(this.TextRun);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button TextRun;
        private System.Windows.Forms.TextBox textBoxSrcFolders;
        private System.Windows.Forms.TextBox textBoxOutputFolder;
    }
}

