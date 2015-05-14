namespace UxBitmapsToAVI
{
    partial class FormBitmapsToAVI
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonCreateAVI = new System.Windows.Forms.Button();
            this.textBoxBitmapsFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAVIFile = new System.Windows.Forms.TextBox();
            this.numericUpDownFrameRate = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxUseCompression = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrameRate)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(605, 398);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonCreateAVI
            // 
            this.buttonCreateAVI.Location = new System.Drawing.Point(524, 398);
            this.buttonCreateAVI.Name = "buttonCreateAVI";
            this.buttonCreateAVI.Size = new System.Drawing.Size(75, 23);
            this.buttonCreateAVI.TabIndex = 1;
            this.buttonCreateAVI.Text = "Create AVI";
            this.buttonCreateAVI.UseVisualStyleBackColor = true;
            this.buttonCreateAVI.Click += new System.EventHandler(this.buttonCreateAVI_Click);
            // 
            // textBoxBitmapsFolder
            // 
            this.textBoxBitmapsFolder.Location = new System.Drawing.Point(9, 25);
            this.textBoxBitmapsFolder.Name = "textBoxBitmapsFolder";
            this.textBoxBitmapsFolder.Size = new System.Drawing.Size(671, 20);
            this.textBoxBitmapsFolder.TabIndex = 2;
            this.textBoxBitmapsFolder.Text = "D:\\saveenr\\Workspace\\beta2-mcp-icons\\jamaica-rebranded-beta-2-2006-04-26\\anim-fil" +
                "es";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Folder with bitmaps";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Name of AVI file to save";
            // 
            // textBoxAVIFile
            // 
            this.textBoxAVIFile.Location = new System.Drawing.Point(12, 78);
            this.textBoxAVIFile.Name = "textBoxAVIFile";
            this.textBoxAVIFile.Size = new System.Drawing.Size(668, 20);
            this.textBoxAVIFile.TabIndex = 5;
            this.textBoxAVIFile.Text = "d:\\anim.avi";
            // 
            // numericUpDownFrameRate
            // 
            this.numericUpDownFrameRate.Location = new System.Drawing.Point(12, 144);
            this.numericUpDownFrameRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFrameRate.Name = "numericUpDownFrameRate";
            this.numericUpDownFrameRate.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownFrameRate.TabIndex = 6;
            this.numericUpDownFrameRate.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Frames Per Second";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 207);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(668, 171);
            this.textBox1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Message Log";
            // 
            // checkBoxUseCompression
            // 
            this.checkBoxUseCompression.AutoSize = true;
            this.checkBoxUseCompression.Location = new System.Drawing.Point(154, 127);
            this.checkBoxUseCompression.Name = "checkBoxUseCompression";
            this.checkBoxUseCompression.Size = new System.Drawing.Size(108, 17);
            this.checkBoxUseCompression.TabIndex = 10;
            this.checkBoxUseCompression.Text = "Use Compression";
            this.checkBoxUseCompression.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(180, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Will ask you to select compression";
            // 
            // FormBitmapsToAVI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 433);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBoxUseCompression);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownFrameRate);
            this.Controls.Add(this.textBoxAVIFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxBitmapsFolder);
            this.Controls.Add(this.buttonCreateAVI);
            this.Controls.Add(this.buttonClose);
            this.Name = "FormBitmapsToAVI";
            this.Text = "Bitmaps To AVI";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrameRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonCreateAVI;
        private System.Windows.Forms.TextBox textBoxBitmapsFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAVIFile;
        private System.Windows.Forms.NumericUpDown numericUpDownFrameRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxUseCompression;
        private System.Windows.Forms.Label label5;
    }
}

