namespace AdvancedReportViewer
{
    partial class FormReportViewer
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.textBoxReportServer = new System.Windows.Forms.TextBox();
            this.textBoxReportPath = new System.Windows.Forms.TextBox();
            this.buttonRenderReport = new System.Windows.Forms.Button();
            this.buttonPrintPreview = new System.Windows.Forms.Button();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonSelectReport = new System.Windows.Forms.Button();
            this.buttonGetRDL = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.Location = new System.Drawing.Point(12, 99);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.reportViewer1.ServerReport.ReportServerUrl = new System.Uri("", System.UriKind.Relative);
            this.reportViewer1.Size = new System.Drawing.Size(684, 495);
            this.reportViewer1.TabIndex = 0;
            // 
            // textBoxReportServer
            // 
            this.textBoxReportServer.Location = new System.Drawing.Point(37, 10);
            this.textBoxReportServer.Name = "textBoxReportServer";
            this.textBoxReportServer.Size = new System.Drawing.Size(323, 20);
            this.textBoxReportServer.TabIndex = 1;
            // 
            // textBoxReportPath
            // 
            this.textBoxReportPath.Location = new System.Drawing.Point(37, 63);
            this.textBoxReportPath.Name = "textBoxReportPath";
            this.textBoxReportPath.Size = new System.Drawing.Size(323, 20);
            this.textBoxReportPath.TabIndex = 2;
            this.textBoxReportPath.Text = "SamplesLibrary.Demo_PaperSizes.A4_Portrait";
            // 
            // buttonRenderReport
            // 
            this.buttonRenderReport.Location = new System.Drawing.Point(367, 12);
            this.buttonRenderReport.Name = "buttonRenderReport";
            this.buttonRenderReport.Size = new System.Drawing.Size(75, 23);
            this.buttonRenderReport.TabIndex = 3;
            this.buttonRenderReport.Text = "Render";
            this.buttonRenderReport.UseVisualStyleBackColor = true;
            this.buttonRenderReport.Click += new System.EventHandler(this.buttonRenderReport_Click);
            // 
            // buttonPrintPreview
            // 
            this.buttonPrintPreview.Location = new System.Drawing.Point(593, 12);
            this.buttonPrintPreview.Name = "buttonPrintPreview";
            this.buttonPrintPreview.Size = new System.Drawing.Size(103, 52);
            this.buttonPrintPreview.TabIndex = 5;
            this.buttonPrintPreview.Text = "Preview && Print";
            this.buttonPrintPreview.UseVisualStyleBackColor = true;
            this.buttonPrintPreview.Click += new System.EventHandler(this.buttonPrintPreview_Click);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(37, 36);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(323, 20);
            this.textBoxPath.TabIndex = 6;
            this.textBoxPath.Text = "dynamics/";
            // 
            // buttonSelectReport
            // 
            this.buttonSelectReport.Location = new System.Drawing.Point(367, 59);
            this.buttonSelectReport.Name = "buttonSelectReport";
            this.buttonSelectReport.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectReport.TabIndex = 7;
            this.buttonSelectReport.Text = "Select";
            this.buttonSelectReport.UseVisualStyleBackColor = true;
            this.buttonSelectReport.Click += new System.EventHandler(this.buttonSelectReport_Click);
            // 
            // buttonGetRDL
            // 
            this.buttonGetRDL.Location = new System.Drawing.Point(593, 70);
            this.buttonGetRDL.Name = "buttonGetRDL";
            this.buttonGetRDL.Size = new System.Drawing.Size(103, 23);
            this.buttonGetRDL.TabIndex = 11;
            this.buttonGetRDL.Text = "View RDL";
            this.buttonGetRDL.UseVisualStyleBackColor = true;
            this.buttonGetRDL.Click += new System.EventHandler(this.buttonGetRDL_Click);
            // 
            // FormReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 606);
            this.Controls.Add(this.buttonGetRDL);
            this.Controls.Add(this.buttonSelectReport);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.buttonPrintPreview);
            this.Controls.Add(this.buttonRenderReport);
            this.Controls.Add(this.textBoxReportPath);
            this.Controls.Add(this.textBoxReportServer);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormReportViewer";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.TextBox textBoxReportServer;
        private System.Windows.Forms.TextBox textBoxReportPath;
        private System.Windows.Forms.Button buttonRenderReport;
        private System.Windows.Forms.Button buttonPrintPreview;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonSelectReport;
        private System.Windows.Forms.Button buttonGetRDL;
    }
}

