namespace DemoCreateSlideShow
{
    public class DisplayResolution
    {
        int Width;
        int Height;
        string Name;
        int RatioHorizontal;
        int RatioVertical;

        public DisplayResolution(string name, int rwidth, int rheight, int w, int h)  
        {
            this.Name = name;
            this.Width =w;
            this.Height = h;
            this.RatioHorizontal = rwidth;
            this.RatioVertical =rheight;
            
        }

    }
}