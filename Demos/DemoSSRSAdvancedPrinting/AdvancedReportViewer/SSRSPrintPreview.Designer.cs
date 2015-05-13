namespace AdvancedReportViewer
{
    partial class SSRSPrintPreview
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonPrevPage = new System.Windows.Forms.Button();
            this.buttonNextPage = new System.Windows.Forms.Button();
            this.panelRenderBox = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPageActualSize = new System.Windows.Forms.Label();
            this.labelDesignActualSize = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSwitchOrientation = new System.Windows.Forms.Button();
            this.comboBoxPaperSizes = new System.Windows.Forms.ComboBox();
            this.checkBoxGrid = new System.Windows.Forms.CheckBox();
            this.labelPrinterName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxUsePrintableArea = new System.Windows.Forms.CheckBox();
            this.checkBoxUseFullPageHeight = new System.Windows.Forms.CheckBox();
            this.labelPageInfo = new System.Windows.Forms.Label();
            this.checkBoxGridHalf = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(637, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Print";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(637, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Page Setup";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.PrinterSettings_Click);
            // 
            // buttonPrevPage
            // 
            this.buttonPrevPage.Location = new System.Drawing.Point(13, 12);
            this.buttonPrevPage.Name = "buttonPrevPage";
            this.buttonPrevPage.Size = new System.Drawing.Size(75, 23);
            this.buttonPrevPage.TabIndex = 2;
            this.buttonPrevPage.Text = "Back";
            this.buttonPrevPage.UseVisualStyleBackColor = true;
            this.buttonPrevPage.Click += new System.EventHandler(this.buttonPrevPage_Click);
            // 
            // buttonNextPage
            // 
            this.buttonNextPage.Location = new System.Drawing.Point(95, 11);
            this.buttonNextPage.Name = "buttonNextPage";
            this.buttonNextPage.Size = new System.Drawing.Size(75, 23);
            this.buttonNextPage.TabIndex = 3;
            this.buttonNextPage.Text = "Forward";
            this.buttonNextPage.UseVisualStyleBackColor = true;
            this.buttonNextPage.Click += new System.EventHandler(this.button4_Click);
            // 
            // panelRenderBox
            // 
            this.panelRenderBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRenderBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelRenderBox.Location = new System.Drawing.Point(13, 155);
            this.panelRenderBox.Name = "panelRenderBox";
            this.panelRenderBox.Size = new System.Drawing.Size(699, 341);
            this.panelRenderBox.TabIndex = 4;
            this.panelRenderBox.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(369, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Paper:";
            // 
            // labelPageActualSize
            // 
            this.labelPageActualSize.Location = new System.Drawing.Point(433, 31);
            this.labelPageActualSize.Name = "labelPageActualSize";
            this.labelPageActualSize.Size = new System.Drawing.Size(198, 14);
            this.labelPageActualSize.TabIndex = 6;
            this.labelPageActualSize.Text = "{WxH}";
            // 
            // labelDesignActualSize
            // 
            this.labelDesignActualSize.Location = new System.Drawing.Point(433, 51);
            this.labelDesignActualSize.Name = "labelDesignActualSize";
            this.labelDesignActualSize.Size = new System.Drawing.Size(198, 14);
            this.labelDesignActualSize.TabIndex = 8;
            this.labelDesignActualSize.Text = "{WxH}";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(369, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Design:";
            // 
            // buttonSwitchOrientation
            // 
            this.buttonSwitchOrientation.Location = new System.Drawing.Point(13, 40);
            this.buttonSwitchOrientation.Name = "buttonSwitchOrientation";
            this.buttonSwitchOrientation.Size = new System.Drawing.Size(122, 23);
            this.buttonSwitchOrientation.TabIndex = 9;
            this.buttonSwitchOrientation.Text = "Switch Orientation";
            this.buttonSwitchOrientation.UseVisualStyleBackColor = true;
            this.buttonSwitchOrientation.Click += new System.EventHandler(this.buttonSwitchOrientation_Click);
            // 
            // comboBoxPaperSizes
            // 
            this.comboBoxPaperSizes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPaperSizes.FormattingEnabled = true;
            this.comboBoxPaperSizes.Location = new System.Drawing.Point(180, 107);
            this.comboBoxPaperSizes.Name = "comboBoxPaperSizes";
            this.comboBoxPaperSizes.Size = new System.Drawing.Size(184, 21);
            this.comboBoxPaperSizes.TabIndex = 10;
            this.comboBoxPaperSizes.SelectedIndexChanged += new System.EventHandler(this.comboBoxPaperSizes_SelectedIndexChanged);
            // 
            // checkBoxGrid
            // 
            this.checkBoxGrid.AutoSize = true;
            this.checkBoxGrid.Location = new System.Drawing.Point(196, 42);
            this.checkBoxGrid.Name = "checkBoxGrid";
            this.checkBoxGrid.Size = new System.Drawing.Size(86, 17);
            this.checkBoxGrid.TabIndex = 11;
            this.checkBoxGrid.Text = "1.0 inch Grid";
            this.checkBoxGrid.UseVisualStyleBackColor = true;
            this.checkBoxGrid.CheckedChanged += new System.EventHandler(this.checkBoxGrid_CheckedChanged);
            // 
            // labelPrinterName
            // 
            this.labelPrinterName.Location = new System.Drawing.Point(433, 12);
            this.labelPrinterName.Name = "labelPrinterName";
            this.labelPrinterName.Size = new System.Drawing.Size(198, 14);
            this.labelPrinterName.TabIndex = 13;
            this.labelPrinterName.Text = "{name}";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(369, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Printer";
            // 
            // checkBoxUsePrintableArea
            // 
            this.checkBoxUsePrintableArea.AutoSize = true;
            this.checkBoxUsePrintableArea.Location = new System.Drawing.Point(250, 18);
            this.checkBoxUsePrintableArea.Name = "checkBoxUsePrintableArea";
            this.checkBoxUsePrintableArea.Size = new System.Drawing.Size(114, 17);
            this.checkBoxUsePrintableArea.TabIndex = 14;
            this.checkBoxUsePrintableArea.Text = "Use Printable Area";
            this.checkBoxUsePrintableArea.UseVisualStyleBackColor = true;
            this.checkBoxUsePrintableArea.CheckedChanged += new System.EventHandler(this.checkBoxUsePrintableArea_CheckedChanged);
            // 
            // checkBoxUseFullPageHeight
            // 
            this.checkBoxUseFullPageHeight.AutoSize = true;
            this.checkBoxUseFullPageHeight.Location = new System.Drawing.Point(13, 69);
            this.checkBoxUseFullPageHeight.Name = "checkBoxUseFullPageHeight";
            this.checkBoxUseFullPageHeight.Size = new System.Drawing.Size(120, 17);
            this.checkBoxUseFullPageHeight.TabIndex = 15;
            this.checkBoxUseFullPageHeight.Text = "Use full page height";
            this.checkBoxUseFullPageHeight.UseVisualStyleBackColor = true;
            // 
            // labelPageInfo
            // 
            this.labelPageInfo.AutoSize = true;
            this.labelPageInfo.Location = new System.Drawing.Point(372, 70);
            this.labelPageInfo.Name = "labelPageInfo";
            this.labelPageInfo.Size = new System.Drawing.Size(64, 13);
            this.labelPageInfo.TabIndex = 16;
            this.labelPageInfo.Text = "Page X of Y";
            // 
            // checkBoxGridHalf
            // 
            this.checkBoxGridHalf.AutoSize = true;
            this.checkBoxGridHalf.Location = new System.Drawing.Point(196, 65);
            this.checkBoxGridHalf.Name = "checkBoxGridHalf";
            this.checkBoxGridHalf.Size = new System.Drawing.Size(86, 17);
            this.checkBoxGridHalf.TabIndex = 17;
            this.checkBoxGridHalf.Text = "0.5 inch Grid";
            this.checkBoxGridHalf.UseVisualStyleBackColor = true;
            this.checkBoxGridHalf.CheckedChanged += new System.EventHandler(this.checkBoxGridHalf_CheckedChanged);
            // 
            // SSRSPrintPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 508);
            this.Controls.Add(this.checkBoxGridHalf);
            this.Controls.Add(this.labelPageInfo);
            this.Controls.Add(this.checkBoxUseFullPageHeight);
            this.Controls.Add(this.checkBoxUsePrintableArea);
            this.Controls.Add(this.labelPrinterName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxGrid);
            this.Controls.Add(this.comboBoxPaperSizes);
            this.Controls.Add(this.buttonSwitchOrientation);
            this.Controls.Add(this.labelDesignActualSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelPageActualSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelRenderBox);
            this.Controls.Add(this.buttonNextPage);
            this.Controls.Add(this.buttonPrevPage);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "SSRSPrintPreview";
            this.Text = "SSRSPrintPreview";
            this.Resize += new System.EventHandler(this.SSRSPrintPreview_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonPrevPage;
        private System.Windows.Forms.Button buttonNextPage;
        private System.Windows.Forms.Panel panelRenderBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPageActualSize;
        private System.Windows.Forms.Label labelDesignActualSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSwitchOrientation;
        private System.Windows.Forms.ComboBox comboBoxPaperSizes;
        private System.Windows.Forms.CheckBox checkBoxGrid;
        private System.Windows.Forms.Label labelPrinterName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxUsePrintableArea;
        private System.Windows.Forms.CheckBox checkBoxUseFullPageHeight;
        private System.Windows.Forms.Label labelPageInfo;
        private System.Windows.Forms.CheckBox checkBoxGridHalf;
    }
}