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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ucDirectoryPickerOutputFolder = new UxBitmapsToAVI.UCDirectoryPicker();
            this.ucFilePickerInputAviFile = new UxBitmapsToAVI.UCFilePicker();
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
            this.buttonCreateAVI.Location = new System.Drawing.Point(470, 398);
            this.buttonCreateAVI.Name = "buttonCreateAVI";
            this.buttonCreateAVI.Size = new System.Drawing.Size(129, 23);
            this.buttonCreateAVI.TabIndex = 1;
            this.buttonCreateAVI.Text = "Extract Bitmaps";
            this.buttonCreateAVI.UseVisualStyleBackColor = true;
            this.buttonCreateAVI.Click += new System.EventHandler(this.buttonCreateAVI_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Output Folder for bitmaps";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Input AVI File";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 243);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(668, 138);
            this.textBox1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Message Log";
            // 
            // ucDirectoryPickerOutputFolder
            // 
            this.ucDirectoryPickerOutputFolder.Directory = "";
            this.ucDirectoryPickerOutputFolder.Location = new System.Drawing.Point(9, 80);
            this.ucDirectoryPickerOutputFolder.Name = "ucDirectoryPickerOutputFolder";
            this.ucDirectoryPickerOutputFolder.Size = new System.Drawing.Size(669, 37);
            this.ucDirectoryPickerOutputFolder.TabIndex = 15;
            // 
            // ucFilePickerInputAviFile
            // 
            this.ucFilePickerInputAviFile.AllowEditing = true;
            this.ucFilePickerInputAviFile.Filename = "";
            this.ucFilePickerInputAviFile.Location = new System.Drawing.Point(9, 25);
            this.ucFilePickerInputAviFile.Name = "ucFilePickerInputAviFile";
            this.ucFilePickerInputAviFile.Size = new System.Drawing.Size(671, 34);
            this.ucFilePickerInputAviFile.TabIndex = 14;
            // 
            // FormBitmapsToAVI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 437);
            this.Controls.Add(this.ucDirectoryPickerOutputFolder);
            this.Controls.Add(this.ucFilePickerInputAviFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCreateAVI);
            this.Controls.Add(this.buttonClose);
            this.Name = "FormBitmapsToAVI";
            this.Text = "Bitmaps To AVI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonCreateAVI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private UCFilePicker ucFilePickerInputAviFile;
        private UCDirectoryPicker ucDirectoryPickerOutputFolder;
    }
}

