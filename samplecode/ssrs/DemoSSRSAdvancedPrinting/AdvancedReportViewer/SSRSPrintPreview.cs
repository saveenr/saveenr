using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSRSCommon.Extensions;
using SD=System.Drawing;
namespace AdvancedReportViewer
{
    public partial class SSRSPrintPreview : Form
    {
        // this report viewer is NOT shown on the form at all, we simply use
        // it to generate EMFs
        private Microsoft.Reporting.WinForms.ReportViewer rep_viewer_ctrl;

        public SSRSCommon.ReportExecutionService.ReportExecutionService rep_exec_svc;
        public SSRSCommon.ReportService2005.ReportingService2005 rep_svc;

        public string ReportServerURL;
        public string ReportPath;
        private List<System.Drawing.Imaging.Metafile> metafiles;

        private SSRSCommon.RDLMetaData rdlmd;

        private int page_index = 0;
        int total_pages= -1;

        public SSRSPrintPreview(SSRSCommon.ReportExecutionService.ReportExecutionService rep_exec_svc, SSRSCommon.ReportService2005.ReportingService2005 rep_svc, string surl, string rpath, SSRSCommon.RDLMetaData rdlmetadata)
        {
            InitializeComponent();

            this.rep_exec_svc = rep_exec_svc;
            this.rep_svc = rep_svc;
            this.ReportServerURL = surl;
            this.ReportPath = rpath;
            this.rdlmd = rdlmetadata;
            
            this.rep_viewer_ctrl = new Microsoft.Reporting.WinForms.ReportViewer();
            this.rep_viewer_ctrl.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;

            var report = this.rep_viewer_ctrl.ServerReport;
            report.ReportServerUrl = new System.Uri( this.ReportServerURL );
            report.ReportPath = this.ReportPath;

            this.rep_viewer_ctrl.RefreshReport();

            

            // get the metafiles
            // In this sample we are getting only the first metafile
            preparemetafiles();

            this.UpdateUI();

        }

        private void preparemetafiles()
        {
            var printersettings = this.GetCurentPrinterSettings();
            var upp = new SSRSCommon.EMFRenderPrefs();
            upp.PageOrientation = SSRSCommon.PageOrientation.Landscape;
            upp.PaperHeight = printersettings.DefaultPageSettings.PaperSize.Height / 100.0f;
            upp.PaperWidth = printersettings.DefaultPageSettings.PaperSize.Width / 100.0f;



            int pages_to_get = 1;
            this.total_pages = this.rep_viewer_ctrl.GetTotalPages();

            this.metafiles = SSRSCommon.RSUtil.RenderMetafilesForReport(

                this.rep_svc,
                this.rep_exec_svc,
                this.ReportPath,
                upp,
                pages_to_get 
                );

        }

        private void UpdateUI()
        {
            this.labelPageInfo.Text = string.Format("Page {0} of {1}", this.page_index+1, this.total_pages);
            var printersettings = this.GetCurentPrinterSettings();

            var po = printersettings.DefaultPageSettings.GetPageOrientation();
            var papersize = printersettings.DefaultPageSettings.PaperSize.ToSizeF().DivideBy(100.0f);

            var designsize = this.rep_viewer_ctrl.ServerReport.GetDefaultPageSettings().PaperSize.ToSizeF().DivideBy(100.0f);

            double ar1 = !printersettings.DefaultPageSettings.Landscape ? papersize.GetAspectRatio() : papersize.FlipSides().GetAspectRatio();
            double ar2 = designsize.GetAspectRatio();

            this.labelPageActualSize.Text = string.Format("{0:0.00} x {1:0.00} AR={3:0.0} {2}", papersize.Width, papersize.Height, po, ar1);
            this.labelDesignActualSize.Text = string.Format("{0:0.00} x {1:0.00} AR={2:0.00}", designsize.Width, designsize.Height, ar2);

            this.labelPrinterName.Text = printersettings.PrinterName;

            // Reset the paper sizes Combobox for the current printer
            this.comboBoxPaperSizes.Items.Clear();
            foreach (var x in printersettings.PaperSizes.AsEnumerable())
            {
                this.comboBoxPaperSizes.Items.Add(x.PaperName);
            }
            this.comboBoxPaperSizes.Text = printersettings.DefaultPageSettings.PaperSize.PaperName;
        }

        private void PrinterSettings_Click(object sender, EventArgs e)
        {
            var form = new PrintDialog();
            form.PrinterSettings = this.GetCurentPrinterSettings();
            var result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.m_printsettings = form.PrinterSettings;
                this.UpdateUI();
            }

            this.panelRenderBox.Invalidate();
        }

        private System.Drawing.Printing.PrinterSettings m_printsettings;

        private System.Drawing.Printing.PrinterSettings GetCurentPrinterSettings()
        {
            if (m_printsettings == null)
            {
                m_printsettings = new System.Drawing.Printing.PrinterSettings();
            }
            return m_printsettings;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (this.metafiles.Count<1)
            {
                // no metafiles to show - do nothing
                return;
            }

            // set the smoothing mode so that the rendering looks good
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            var brush_black = System.Drawing.Brushes.Black;
            var brush_gray = System.Drawing.Brushes.Gray;
            var brush_white = System.Drawing.Brushes.White;
            var pen_red = System.Drawing.Pens.Red;
            var pen_green = System.Drawing.Pens.Green;
            var renderbox_margin = new SD.Size(10, 10);
            bool use_print_area = this.checkBoxUsePrintableArea.Checked;
            
            // retrieve the default size to be used for printing
            var printer_settings = this.GetCurentPrinterSettings();
            var landscape = printer_settings.DefaultPageSettings.Landscape;

            // Get the sizes in common units
            SD.SizeF papersize_raw = printer_settings.DefaultPageSettings.PaperSize.ToSizeF();
            SD.SizeF rdlsize_raw = this.rdlmd.PageSize;
            SD.RectangleF printable_area_raw = printer_settings.DefaultPageSettings.PrintableArea;

            // Normalize the sizes into comparable units of 100th of an inch
            SD.SizeF papersize_normalized = landscape ? papersize_raw.FlipSides() : papersize_raw ; // adjust for landscape
            SD.RectangleF printable_rect_normalized = landscape ? printable_area_raw.FlipSides() : printable_area_raw; 
            SD.SizeF rdlsize_normalized = rdlsize_raw.MultiplyBy(100.0f); // multiply by 100 to match printer units

            // Fit the normalized RDL into the papersize
            SD.SizeF rdlsize_normalized_size = use_print_area
                                                   ? rdlsize_normalized.ResizeDownToFit(printable_rect_normalized.Size)
                                                   : rdlsize_normalized.ResizeDownToFit(papersize_normalized);
            
            // the renderbox is the full area dedicated to the preview
            // renderbox_margin keeps the preview from bumping into the edges of the renderbox
            var renderbox_rect = this.panelRenderBox.Size.ToRect();
            
            // display rect is the actual available region we use to render the preview
            var display_rect = renderbox_rect.GetInternalRectangle(renderbox_margin);


            
            // Coords for the white area, accounting for Landscape
            var paper_rect = papersize_normalized.ToRectF();
            
            var paper_scale = SSRSCommon.GraphicsUtil.GetFitToAreaScalingFactor(paper_rect.Size, display_rect.Size);

            int new_paper_x = (int)((display_rect.Width - ((int)paper_rect.Width * paper_scale)) / 2.0);
            int new_paper_y = (int)((display_rect.Height- ((int)paper_rect.Height* paper_scale)) / 2.0);


            // Coords for the design
            var mf = this.metafiles[this.page_index];


            SD.RectangleF rdl_rect = use_print_area
                                       ? rdlsize_normalized_size.ToRectF().Add(printable_rect_normalized.Location)
                                       : rdlsize_normalized_size.ToRectF().Add(0.0f,0.0f);
            
            
            // do the drawing -----------------------------------------

            // Erase the entire background
            e.Graphics.FillRectangle(brush_gray, renderbox_rect);
            
            // Translate to account for the renderbox margin

            e.Graphics.TranslateTransform(renderbox_margin.Width + new_paper_x, renderbox_margin.Height + new_paper_y);
            e.Graphics.ScaleTransform((float)paper_scale, (float)paper_scale);

            // Draw a white rectangle to simulate the physical page used by the printer
            e.Graphics.FillRectangle(brush_white, paper_rect);



            // Draw annotations
            bool debug_rendering = true;
            bool debug_show_printable_area = true;
            bool debug_show_design_edges = true;
            bool debug_show_inch_grid = this.checkBoxGrid.Checked;
            bool debug_show_halfinch_grid = this.checkBoxGridHalf.Checked;
            if (debug_rendering)
            {
                if (debug_show_inch_grid)
                {
                    draw_inch_grid(e, papersize_normalized , 1.0f);
                }
                if (debug_show_halfinch_grid)
                {
                    draw_inch_grid(e, papersize_normalized, 0.5f);
                }
                if (debug_show_printable_area)
                {
                    e.Graphics.DrawRectangle(pen_green, printable_rect_normalized); // Green the printable area                    
                }
                if (debug_show_design_edges)
                {
                    // RED - the edges of the RDL design
                    e.Graphics.DrawRectangle(pen_red, rdl_rect); 
                }

            }

            // Draw the EMF into a region on the white rectangle
            e.Graphics.DrawImage(mf, rdl_rect);

            
        }

        private void draw_inch_grid(PaintEventArgs e, SD.SizeF papersize_normalized, float gridsize)
        {
            float inch = 100.0f * gridsize;

            float hor_pos = 0.0f;
            while (hor_pos < papersize_normalized.Width + inch)
            {
                hor_pos += inch;

                float top = 0.0f;
                float bottom = papersize_normalized.Height;

                var p1 = new System.Drawing.PointF(hor_pos, top);
                var p2 = new System.Drawing.PointF(hor_pos, bottom);
                e.Graphics.DrawLine(System.Drawing.Pens.Gray, p1, p2);

            }

            float ver_pos = 0.0f;
            while (ver_pos < papersize_normalized.Height + inch)
            {
                ver_pos += inch;

                float left = 0.0f;
                float right = papersize_normalized.Width;

                var p1 = new System.Drawing.PointF(left, ver_pos);
                var p2 = new System.Drawing.PointF(right, ver_pos);
                e.Graphics.DrawLine(System.Drawing.Pens.Gray, p1, p2);
            }
        }

        private void SSRSPrintPreview_Resize(object sender, EventArgs e)
        {
            this.panelRenderBox.Invalidate();
        }

        private void buttonSwitchOrientation_Click(object sender, EventArgs e)
        {
            var ps = this.GetCurentPrinterSettings();

            ps.DefaultPageSettings.Landscape = !ps.DefaultPageSettings.Landscape;

            this.m_printsettings = ps;
            this.UpdateUI();
            this.panelRenderBox.Invalidate();
        }

        private void comboBoxPaperSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ps = this.GetCurentPrinterSettings();

            var selected_papername = this.comboBoxPaperSizes.SelectedItem.ToString();

            if (ps.DefaultPageSettings.PaperSize.PaperName == selected_papername)
            {
                return;
            }

            var papersizes_dic = ps.PaperSizes.AsEnumerable().ToDictionary( size => size.PaperName );
            if (papersizes_dic.ContainsKey(selected_papername))
            {
                ps.DefaultPageSettings.PaperSize = papersizes_dic[selected_papername];

                this.m_printsettings = ps;
                this.UpdateUI();
                this.panelRenderBox.Invalidate();
            }
            else
            {
                MessageBox.Show("ERROR");
            }


        }

        private void checkBoxGrid_CheckedChanged(object sender, EventArgs e)
        {
            this.panelRenderBox.Invalidate();

        }

        private int cur_page_count = 0;
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (this.metafiles == null)
            {
                MessageBox.Show("Null Metafile collection");
                return;
            }

            if (this.metafiles.Count == 0)
            {
                MessageBox.Show("No pages to print");
                return;                
            }

            // Perform the printing
            this.cur_page_count = 0;
            var print_document = new System.Drawing.Printing.PrintDocument();
            print_document.PrinterSettings = this.GetCurentPrinterSettings();
            print_document.PrintPage += OnPrintPage;
            print_document.Print();

        }

        void OnPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            var metafile = this.metafiles[this.cur_page_count];
            send_metafile_to_printer(metafile, e);

            // increment page count and flag if additional pages are needed
            this.cur_page_count++;
            e.HasMorePages = this.cur_page_count < this.metafiles.Count;
        }

        private void send_metafile_to_printer(Metafile metafile, PrintPageEventArgs e)
        {

            // constants
            const float unit_factor = 100.0f;

            // prefs
            var desired_printing_dpi = new System.Drawing.SizeF(300.0f, 300.0f);

            // Retrieve metadfile information
            var metafile_header = metafile.GetMetafileHeader();
            var metafile_dpi = new System.Drawing.SizeF(metafile_header.DpiX, metafile_header.DpiY);

            // Based on the orientation command, adjust the printer orientation
            var orient_command = SSRSCommon.PageOrientationCommand.UseDefaultPrinterSetting;

            if (orient_command == SSRSCommon.PageOrientationCommand.ForceLandscape)
            {
                e.PageSettings.Landscape = true;
            }
            else if (orient_command == SSRSCommon.PageOrientationCommand.ForcePortrait)
            {
                e.PageSettings.Landscape = false;
            }
            else if (orient_command == SSRSCommon.PageOrientationCommand.UseDefaultPrinterSetting)
            {
                // do nothing
            }
            else if (orient_command == SSRSCommon.PageOrientationCommand.UseReportDesign)
            {
                bool design_landscape = metafile.Width > metafile.Height;
                e.PageSettings.Landscape = design_landscape;
            }

            // Retrieve information about the printer
            var papersize = new SD.SizeF(e.PageSettings.PaperSize.Width, e.PageSettings.PaperSize.Height);
            //var printable_size = new SD.SizeF(e.PageSettings.PrintableArea.Width, e.PageSettings.PrintableArea.Height);

            // ------------------------------------------------------------------
            // find the metafile size in printer units
            var dpi_factor = metafile_dpi.DivideBy(desired_printing_dpi);
            e.Graphics.ScaleTransform(dpi_factor.Width, dpi_factor.Height, System.Drawing.Drawing2D.MatrixOrder.Prepend);
            var metafile_size_normalized = metafile.Size.ToSizeF().DivideBy(desired_printing_dpi).MultiplyBy(unit_factor);
            var metafile_size_oriented = e.PageSettings.Landscape ? metafile_size_normalized.FlipSides() : metafile_size_normalized;

            // ----------------------------------------------------
            // Calculate the scaling factor to use on the metafile - do we need to shrink it down to fit the page or not.
            
            float scale_to_fit_factor = 1.0f; // 1.0 = don't scale the metafile at all
            if (!metafile_size_normalized.FitsInside(papersize))
            {
                scale_to_fit_factor = papersize.DivideBy(metafile_size_oriented).GetSmallestSide();
            }

            // we now have all the information we need to scale
            e.Graphics.ScaleTransform(scale_to_fit_factor, scale_to_fit_factor, System.Drawing.Drawing2D.MatrixOrder.Append);

            // shift the metafile by hard margin size to aling to the top left side of the paper
            int hm_offset_x = (int) e.PageSettings.HardMarginX*-1;
            int hm_offset_y = (int) e.PageSettings.HardMarginY * -1;
            var hm_offset = new SD.Point(hm_offset_x, hm_offset_y);
            var points = new[] { hm_offset };
            var matrix = e.Graphics.Transform;
            matrix.Invert();
            matrix.TransformPoints(points);

            // --------------------------------------------------------------------
            // Draw the image
            
            e.Graphics.DrawImageUnscaled(metafile, points[0]);
        }

        private void checkBoxUsePrintableArea_CheckedChanged(object sender, EventArgs e)
        {
            this.panelRenderBox.Invalidate();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.total_pages > 1)
            {
                if (this.page_index < (this.total_pages - 1))
                {

                    get_all_metafiles();

                    this.page_index++;

                    this.UpdateUI();

                    this.panelRenderBox.Invalidate();

                }

            }
        }

        private void get_all_metafiles()
        {
            if (this.metafiles.Count < this.total_pages)
            {
                //MessageBox.Show("Getting all metafiles");
                var printersettings = this.GetCurentPrinterSettings();
                var upp = new SSRSCommon.EMFRenderPrefs();
                upp.PageOrientation = SSRSCommon.PageOrientation.Landscape;
                upp.PaperHeight = printersettings.DefaultPageSettings.PaperSize.Height / 100.0f;
                upp.PaperWidth = printersettings.DefaultPageSettings.PaperSize.Width / 100.0f;


                this.metafiles = SSRSCommon.RSUtil.RenderMetafilesForReport(

                    this.rep_svc,
                    this.rep_exec_svc,
                    this.ReportPath,
                    upp,
                    -1
                    );
                
            }
        }

        private void buttonPrevPage_Click(object sender, EventArgs e)
        {
            if (this.page_index > 0)
            {
                this.page_index--;
            }

            this.UpdateUI();

            this.panelRenderBox.Invalidate();
        }

        private void checkBoxGridHalf_CheckedChanged(object sender, EventArgs e)
        {
            this.panelRenderBox.Invalidate();

        }
    }
}
