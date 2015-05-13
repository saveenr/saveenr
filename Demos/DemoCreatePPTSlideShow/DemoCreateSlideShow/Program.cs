using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PP = Microsoft.Office.Interop.PowerPoint;
using MOC = Microsoft.Office.Core;
namespace DemoCreateSlideShow
{
    class Program
    {
        // http://support.microsoft.com/kb/303718/
        // http://msdn.microsoft.com/hi-in/magazine/cc163471(en-us).aspx


        static void Main(string[] args)
        {
            double bitmap_pixels_per_inch = 96.0;


            double slide_points_per_inch = 72.0;
            double scale = 1.0; // 1.0 = 100% - pixel to pixel
            // WORKITEM: FOrce scale between 0>scale<=1.0

            double bitmap_w_in_pixels = 1280;
            double bitmap_h_in_pixels = 1024;
            double h_excess = 0.3;
            double v_excess = 0.1;

            double bitmap_w_in_inches = scale * (bitmap_w_in_pixels / bitmap_pixels_per_inch);
            double bitmap_h_in_inches = scale * (bitmap_h_in_pixels / bitmap_pixels_per_inch);

            double excess_w_in_points = (h_excess * bitmap_w_in_inches)* slide_points_per_inch;
            double excess_h_in_points = (v_excess * bitmap_w_in_inches) * slide_points_per_inch;

            double excess_w_in_inches = excess_w_in_points / slide_points_per_inch;
            double excess_h_in_inches = excess_h_in_points / slide_points_per_inch;

            double slide_w_in_inches = (bitmap_w_in_inches) + excess_w_in_inches;
            double slide_h_in_inches = (bitmap_h_in_inches) + excess_h_in_inches;


            int placeholder_width_points = (int)System.Math.Round(bitmap_w_in_inches * slide_points_per_inch, 0) ;
            int placeholder_height_points = (int)System.Math.Round(bitmap_h_in_inches * slide_points_per_inch, 0);

            int pic_left = (int)(excess_w_in_points);
            int pic_top = 0;
            int text_width = (int)(excess_w_in_points);
            int text_height = (int) (bitmap_h_in_inches * slide_points_per_inch);

            
            var app = new PP.ApplicationClass();
            app.Visible = MOC.MsoTriState.msoTrue;
            var presentation = app.Presentations.Add(MOC.MsoTriState.msoTrue);
            presentation.PageSetup.SlideWidth = (int)(slide_w_in_inches * slide_points_per_inch);
            presentation.PageSetup.SlideHeight = (int)(slide_h_in_inches * slide_points_per_inch);
            
            string input_folder = System.IO.Path.GetFullPath(args[0]);

            Console.WriteLine("PATH {0}",input_folder);
            var files = System.IO.Directory.GetFiles(input_folder, "*.png");

            string custom_layout_name = "ScreenShot";
            
            var customlayout = presentation.SlideMaster.CustomLayouts.Add(presentation.SlideMaster.CustomLayouts.Count + 1);
            customlayout.Name = custom_layout_name;
            var s1  = customlayout.Shapes[1];
            s1.Delete();


            var comment_shape = customlayout.Shapes.AddPlaceholder(Microsoft.Office.Interop.PowerPoint.PpPlaceholderType.ppPlaceholderBody, 0, 0, text_width, text_height);
            var pic_shape = customlayout.Shapes.AddPlaceholder(Microsoft.Office.Interop.PowerPoint.PpPlaceholderType.ppPlaceholderBitmap, pic_left, pic_top, placeholder_width_points, placeholder_height_points);

            int? debug_slide_limit = 3;

            var files_for_slides = files.AsEnumerable();
            if (debug_slide_limit.HasValue)
            {
                files_for_slides = files_for_slides.Take(debug_slide_limit.Value);
                
            }

            foreach (var file in files_for_slides)
            {

                var slide = presentation.Slides.AddNew(customlayout);
                var linktofile = MOC.MsoTriState.msoFalse; 
                var savewithdocument = MOC.MsoTriState.msoTrue;
                var bitmap_shape = slide.Shapes.AddPicture(file, linktofile, savewithdocument, pic_left, pic_top, placeholder_width_points, placeholder_height_points);
                bitmap_shape.ScaleHeight( (float)scale, MOC.MsoTriState.msoTrue, MOC.MsoScaleFrom.msoScaleFromTopLeft);
                bitmap_shape.ScaleWidth((float)scale, MOC.MsoTriState.msoTrue, MOC.MsoScaleFrom.msoScaleFromTopLeft);

                PP.Shape ns = null;
                if (slide.NotesPage.Shapes.Count < 1)
                {
                    //ns = slide.NotesPage.Shapes.AddShape(Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle, 0, 0, 0, 0);
                }
                else
                {
                    //ns = slide.NotesPage.Shapes[1];
                }

            }


        }
    }
}
