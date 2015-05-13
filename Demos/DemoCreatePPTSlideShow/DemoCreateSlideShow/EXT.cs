namespace DemoCreateSlideShow
{
    static class EXT
    {
        public static Microsoft.Office.Interop.PowerPoint.Slide AddNew(this Microsoft.Office.Interop.PowerPoint.Slides slides, Microsoft.Office.Interop.PowerPoint.CustomLayout cl)
        {
            var slide = slides.AddSlide(slides.Count + 1, cl);
            return slide;
        }
    }
}