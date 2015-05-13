using SD = System.Drawing;

namespace SSRSCommon.Extensions
{
    public static class ExtensionMethods
    {
        public static bool FitsInside( this SD.SizeF size , SD.SizeF s)
        {
            if (size.Width > s.Width)
            {
                return false;
            }

            if (size.Height > s.Height)
            {
                return false;
            }

            return true;
        }
        public static float GetSmallestSide(this SD.SizeF size)
        {
            return System.Math.Min(size.Width, size.Height);
        }

        public static SD.SizeF ToSizeF(this SD.Size size)
        {
            return new SD.SizeF( (float) size.Width , (float) size.Height );
        }

        public static SD.Rectangle ToRect(this SD.SizeF size)
        {
            return new SD.Rectangle(0, 0, (int)size.Width, (int)size.Height);
        }

        public static SD.RectangleF ToRectF(this SD.SizeF size)
        {
            return new SD.RectangleF(0, 0, size.Width, size.Height);
        }



        public static SD.SizeF DivideBy( this SD.SizeF size, SD.SizeF s)
        {
            return new SD.SizeF(size.Width/s.Width, size.Height/s.Height);
        }

        public static SD.Size Subtract( this SD.Size rectx, SD.Size s)
        {
            return new SD.Size( rectx.Width- s.Width, rectx.Height - s.Height);
        }

        public static SD.Size Multiply(this SD.Size rectx, float s )
        {
            return new SD.Size( (int) (rectx.Width * s), (int) (rectx.Height * s) );
        }


        public static SD.SizeF DivideBy(this SD.SizeF size, float s)
        {
            return new SD.SizeF(size.Width / s, size.Height / s);
        }

        public static SD.SizeF MultiplyBy(this SD.SizeF size, SD.SizeF s)
        {
            return new SD.SizeF(size.Width * s.Width, size.Height *  s.Height);
        }

        public static SD.SizeF MultiplyBy(this SD.SizeF size, float s)
        {
            return new SD.SizeF(size.Width * s, size.Height * s);
        }

        public static float GetAspectRatio(this SD.SizeF size)
        {
            return size.Height / size.Width;
        }

        public static SD.SizeF FlipSides(this SD.SizeF size)
        {
            return new SD.SizeF(size.Height, size.Width);
        }

        public static SD.RectangleF FlipSides(this SD.RectangleF size)
        {
            return new SD.RectangleF(size.X, size.Y, size.Height, size.Width);
        }
        
        public static SD.RectangleF Add(this SD.RectangleF size, System.Drawing.Point p)
        {
            return new SD.RectangleF(size.X + p.X , size.Y + p.Y, size.Width, size.Height);
        }

        public static SD.RectangleF Add(this SD.RectangleF size, System.Drawing.PointF p)
        {
            return new SD.RectangleF(size.X + p.X, size.Y + p.Y, size.Width, size.Height);
        }

        public static SD.RectangleF Add(this SD.RectangleF size, float x, float y)
        {
            return new SD.RectangleF(size.X + x, size.Y + y, size.Width, size.Height);
        }
        public static SD.SizeF Floor(this SD.SizeF size)
        {
            return new SD.SizeF((float)System.Math.Floor(size.Width), (float)System.Math.Floor(size.Height) );
        }

        public static SD.SizeF ResizeDownToFit(this SD.SizeF size, SD.SizeF max)
        {
            return GraphicsUtil.ResizeDownToFit(size, max);
        }
        
         public static SD.Rectangle GetInternalRectangle(this SD.Rectangle rect, SD.Size size)
         {
             return new SD.Rectangle( rect.X + size.Width, rect.Y + size.Height, rect.Width - (size.Width * 2), rect.Height - (size.Height * 2));
         }

         public static SD.Rectangle Scale(this SD.Rectangle rect, float s)
         {
             return new SD.Rectangle(rect.X, rect.Y , (int)(rect.Width * s), (int)(rect.Height * s) );
         }

         public static SD.Rectangle Add(this SD.Rectangle rect, SD.Size s)
         {
             return new SD.Rectangle(rect.X + s.Width, rect.Y + s.Height, rect.Width, rect.Height);
         }

         public static SD.PointF Add(this SD.PointF p, SD.SizeF s)
         {
             return new SD.PointF(p.X + s.Width, p.Y + s.Height);
         }

         public static SD.Rectangle FlipSides(this SD.Rectangle rect)
         {
             return new SD.Rectangle(rect.X, rect.Y, rect.Height, rect.Width);
         }

        public static SD.Size ToSize( this SD.Printing.PaperSize papersize)
        {
            return new SD.Size(papersize.Width, papersize.Height);
        }

        public static SD.SizeF ToSizeF(this SD.Printing.PaperSize papersize)
        {
            return new SD.SizeF(papersize.Width, papersize.Height);
        }

        public static SD.Rectangle ToRect(this SD.Size size)
        {
            return new SD.Rectangle(0, 0, size.Width, size.Height);
        }

        public static SD.Size ToSize(this SD.RectangleF rect)
        {
            return new SD.Size((int)rect.Width, (int)rect.Height);
        }



        public static SD.Size ToSize(this SD.Rectangle rect)
        {
            return new SD.Size((int)rect.Width, (int)rect.Height);
        }


        public static SSRSCommon.PageOrientation GetPageOrientation( this SD.Printing.PageSettings ps)
        {
            return ps.Landscape ? SSRSCommon.PageOrientation.Landscape : SSRSCommon.PageOrientation.Portrait;
        }

        public static System.Collections.Generic.IEnumerable<SD.Printing.PaperSize> AsEnumerable(this SD.Printing.PrinterSettings.PaperSizeCollection col)
        {
            for (int i = 0; i < col.Count; i++)
            {
                yield return col[i];
            }
        }

        public static SD.PointF MultiplyBy(this SD.PointF size, float s)
        {
            return new SD.PointF(size.X* s, size.Y* s);
        }

        public static bool FitsInside(this SD.Size s1, SD.Size s2)
        {
            return ((s1.Width <= s2.Width) && (s1.Height <= s2.Height));
        }

        public static SD.SizeF GetResolution(this SD.Imaging.Metafile mf)
        {
            return new SD.SizeF(mf.HorizontalResolution, mf.VerticalResolution);
        }

        public static SD.SizeF GetDPI(this SD.Imaging.MetafileHeader mf)
        {
            return new SD.SizeF(mf.DpiX, mf.DpiY);
        }


        public static System.Xml.Linq.XElement ElementRDL2005(this System.Xml.Linq.XElement el, string name)
        {
            return el.Element(SSRSCommon.RSUtil.RDL2005_namespace_x + name);
        }

        public static void DrawRectangle( this SD.Graphics g, SD.Pen pen, SD.RectangleF rect)
        {
            g.DrawRectangle(pen, rect.X,rect.Y,rect.Width,rect.Height);
        }

        public static void FillRectangle(this SD.Graphics g, SD.Brush pen, SD.RectangleF rect)
        {
            g.FillRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }
    }
}