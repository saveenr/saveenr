using SD = System.Drawing;
using WEBCONTROLS = System.Web.UI.WebControls;
using MSCHART = System.Web.UI.DataVisualization.Charting;


namespace WebCharting
{
    public class FontDesc
    {
        public string Name;
        public float EmSize;

        public FontDesc(string name, float emsize)
        {
            this.Name = name;
            this.EmSize = emsize;
        }

        public SD.Font GetSDFont()
        {
            return new SD.Font(this.Name, this.EmSize);
        }

        public FontDesc Clone()
        {
            var o = new FontDesc(this.Name,this.EmSize);
            return o;
        }
    }

}