using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Office.Core;
using Microsoft.Office.Tools.Ribbon;
using PP=Microsoft.Office.Interop.PowerPoint;

namespace PowerCode
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button_selection_Click(object sender, RibbonControlEventArgs e)
        {
            var app = Globals.ThisAddIn.Application;

            var pres = app.ActivePresentation;
            var wnd = app.ActiveWindow;
            var view = wnd.View;
            var slide = (PP.Slide) view.Slide;

            fix_quotes_slide(slide);
        }

        private static void fix_quotes_slide(PP.Slide slide)
        {
            foreach (PP.Shape shape in slide.Shapes)
            {
                fix_quotes_shape(shape);
            }
        }

        private void button_slide_Click(object sender, RibbonControlEventArgs e)
        {
            var app = Globals.ThisAddIn.Application;

            var pres = app.ActivePresentation;
            var wnd = app.ActiveWindow;
            var view = wnd.View;
            var slide = (PP.Slide ) view.Slide;

            var ac = app.AutoCorrect;
            fix_quotes_slide(slide);

        }

        private void button_doc_Click(object sender, RibbonControlEventArgs e)
        {
            var app = Globals.ThisAddIn.Application;

            var pres = app.ActivePresentation;
            var wnd = app.ActiveWindow;
            var view = wnd.View;

            foreach (PP.Slide slide in pres.Slides)
            {
                fix_quotes_slide(slide);
            }
        }

        private static void fix_quotes_shape(PP.Shape shape)
        {
            var tf = shape.TextFrame;

            if (tf != null)
            {
                var tr = tf.TextRange;

                while (tr.Find("”") != null)
                {
                    tr.Replace("”", "\"");                   
                }

                while (tr.Find("“") != null)
                {
                    tr.Replace("“", "\"");
                }

                while (tr.Find("‘") != null)
                {
                    tr.Replace("‘", "'");
                }

                while (tr.Find("’") != null)
                {
                    tr.Replace("’", "'");
                }

                
            }
        }

        private void button_fit_to_slide_width_Click(object sender, RibbonControlEventArgs e)
        {
            ResizeImageToFit(ResizeType.Width);
        }

        public enum ResizeType
        {
            Width, Height
        }

        private void ResizeImageToFit(ResizeType rt)
        {
            var app = Globals.ThisAddIn.Application;

            var pres = app.ActivePresentation;
            var wnd = app.ActiveWindow;
            var view = wnd.View;

            var sel = wnd.Selection;
            if (sel == null)
            {
                MessageBox.Show("Nothing selected; selection null");
                return;
            }

            if (sel.Type == PP.PpSelectionType.ppSelectionNone)
            {
                MessageBox.Show("Nothing selected; selection type null");
                return;
            }

            var pagesetup = pres.PageSetup;
            var slide_width = pagesetup.SlideWidth;
            var slide_height = pagesetup.SlideHeight;

            if (sel.Type == PP.PpSelectionType.ppSelectionShapes)
            {
                foreach (PP.Shape shape in sel.ShapeRange)
                {
                    if (shape.Type == MsoShapeType.msoPicture)
                    {

                        

                        if (rt == ResizeType.Width)
                        {
                            shape.Width = slide_width;                            
                        }
                        else if (rt == ResizeType.Height)
                        {
                            shape.Height = slide_height;                                                        
                        }

                        if (this.checkBox_auto_center.Checked)
                        {
                            var picwidth = shape.Width;
                            var picheight = shape.Height;

                            shape.Left = (slide_width / 2) - (picwidth/ 2);
                            shape.Top = (slide_height/ 2) - (picheight / 2);
                        }
                    }
                }
            }
        }

        private void button_fit_to_slide_height_Click(object sender, RibbonControlEventArgs e)
        {
            ResizeImageToFit(ResizeType.Height);
        }

    }
}
