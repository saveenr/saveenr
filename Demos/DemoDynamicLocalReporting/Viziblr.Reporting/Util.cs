using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRM=Viziblr.Reporting.Modeling;
using IR=Viziblr.Reporting.Modeling;

namespace Viziblr.Reporting
{
    public static class Util
    {
        public static string NormalizeColumnName(string text)
        {
            string new_text;
            new_text = text.Replace(" ", "_");
            new_text = text.Replace(":", "_");
            new_text = text.Replace("/", "_");
            new_text = text.Replace("\\", "_");
            new_text = text.Replace("(", "_");
            new_text = text.Replace(")", "_");
            new_text = text.Replace(",", "_");
            new_text = text.Replace("+", "_");
            new_text = text.Replace("-", "_");
            new_text = text.Replace("*", "_");
            new_text = text.Replace("__", "_");
            return new_text;
        }

        public static void ShowReport(   
            IR.Report report_model,
            Viziblr.Reporting.ViewingOptions viewingoptions, 
                                                    System.Action<Viziblr.Reporting.RDL2005.Report> mod_report)
        {

            int dpi = 96;
            var layout= report_model.PageLayout;
            int pixel_width = (int)( (layout.PageWidth+  layout.LeftMargin + layout.RightMargin )*dpi);
            int pixel_height = (int)((layout.PageHeight+ layout.TopMargin+   layout.BottomMargin) * dpi);
            var form_size = new System.Drawing.Size(pixel_width, pixel_height);

            var form = new ReportViewerForm();
            form.Size = form_size;


            var reportviewer = form.ReportViewer;

            //reportviewer.Dock = System.Windows.Forms.DockStyle.Fill;
            reportviewer.Name = report_model.DatasetName;
            reportviewer.Name = "ReportViewerDynamic_" + report_model.DatasetName;
            //form.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            form.Name = report_model.DatasetName;

            reportviewer.ShowParameterPrompts = true;
            reportviewer.ShowPrintButton = false;
            reportviewer.ShowDocumentMapButton= false;
            reportviewer.ShowBackButton = false;
            reportviewer.ShowExportButton = false;
            reportviewer.ShowRefreshButton = false;

            var xmldom =Viziblr.Reporting.RDL2005.RDLGenerator.ShowReport(reportviewer, report_model, viewingoptions, mod_report);

            
            string form_title = report_model.ReportTitle;
            form.Text = form_title;
            form.ShowDialog();


        }

        public static string GetExprFieldValue(string fieldname)
        {
            string s = "=" + GetExprFieldValueNoEquals(fieldname);
            return s;
        }

        public static string GetExprFieldValueNoEquals(string fieldname)
        {
            string s = "Fields!" + fieldname + ".Value";
            return s;
        }
    }
}
