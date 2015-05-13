namespace FileNameTimeStampApp
{
    partial class FormFileRenamerApp
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
            this.listViewInputFiles = new System.Windows.Forms.ListView();
            this.columnHeaderFolder = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderOldName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderNewName = new System.Windows.Forms.ColumnHeader();
            this.labelInstructions = new System.Windows.Forms.Label();
            this.columnHeaderCreateTime = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderModified = new System.Windows.Forms.ColumnHeader();
            this.buttonScriptToClipboard = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewInputFiles
            // 
            this.listViewInputFiles.AllowDrop = true;
            this.listViewInputFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewInputFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewInputFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFolder,
            this.columnHeaderCreateTime,
            this.columnHeaderModified,
            this.columnHeaderOldName,
            this.columnHeaderNewName});
            this.listViewInputFiles.Location = new System.Drawing.Point(12, 60);
            this.listViewInputFiles.Name = "listViewInputFiles";
            this.listViewInputFiles.Size = new System.Drawing.Size(921, 469);
            this.listViewInputFiles.TabIndex = 0;
            this.listViewInputFiles.UseCompatibleStateImageBehavior = false;
            this.listViewInputFiles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderFolder
            // 
            this.columnHeaderFolder.Text = "Folder";
            this.columnHeaderFolder.Width = 386;
            // 
            // columnHeaderOldName
            // 
            this.columnHeaderOldName.Text = "Old Name";
            this.columnHeaderOldName.Width = 200;
            // 
            // columnHeaderNewName
            // 
            this.columnHeaderNewName.Text = "New Name";
            this.columnHeaderNewName.Width = 200;
            // 
            // labelInstructions
            // 
            this.labelInstructions.AutoSize = true;
            this.labelInstructions.Location = new System.Drawing.Point(12, 44);
            this.labelInstructions.Name = "labelInstructions";
            this.labelInstructions.Size = new System.Drawing.Size(144, 13);
            this.labelInstructions.TabIndex = 1;
            this.labelInstructions.Text = "Drop files into the area below";
            // 
            // columnHeaderCreateTime
            // 
            this.columnHeaderCreateTime.Text = "Created";
            // 
            // columnHeaderModified
            // 
            this.columnHeaderModified.Text = "Modified";
            // 
            // buttonScriptToClipboard
            // 
            this.buttonScriptToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonScriptToClipboard.Location = new System.Drawing.Point(15, 536);
            this.buttonScriptToClipboard.Name = "buttonScriptToClipboard";
            this.buttonScriptToClipboard.Size = new System.Drawing.Size(129, 23);
            this.buttonScriptToClipboard.TabIndex = 2;
            this.buttonScriptToClipboard.Text = "Copy Script to Clipboard";
            this.buttonScriptToClipboard.UseVisualStyleBackColor = true;
            this.buttonScriptToClipboard.Click += new System.EventHandler(this.buttonScriptToClipboard_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClear.Location = new System.Drawing.Point(165, 535);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // FormFileRenamerApp
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 577);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonScriptToClipboard);
            this.Controls.Add(this.labelInstructions);
            this.Controls.Add(this.listViewInputFiles);
            this.Name = "FormFileRenamerApp";
            this.Text = "Isotope File Renamer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewInputFiles;
        private System.Windows.Forms.Label labelInstructions;
        private System.Windows.Forms.ColumnHeader columnHeaderFolder;
        private System.Windows.Forms.ColumnHeader columnHeaderOldName;
        private System.Windows.Forms.ColumnHeader columnHeaderNewName;
        private System.Windows.Forms.ColumnHeader columnHeaderCreateTime;
        private System.Windows.Forms.ColumnHeader columnHeaderModified;
        private System.Windows.Forms.Button buttonScriptToClipboard;
        private System.Windows.Forms.Button buttonClear;
    }
}

