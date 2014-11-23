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
            PP.Slide slide = (PP.Slide) view.Slide;

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
            PP.Slide slide = (PP.Slide ) view.Slide;

            PP.AutoCorrect ac = app.AutoCorrect;
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

        private void button_fit_to_slide_Click(object sender, RibbonControlEventArgs e)
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

            var width = pres.PageSetup.SlideWidth;

            if (sel.Type == PP.PpSelectionType.ppSelectionShapes)
            {
                foreach (PP.Shape shape in sel.ShapeRange)
                {
                    if (shape.Type == MsoShapeType.msoPicture)
                    {
                        shape.Width = width;
                    }                    
                }
            }

        }

    }
}
