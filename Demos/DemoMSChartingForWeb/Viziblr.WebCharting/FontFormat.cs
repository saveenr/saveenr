using SD = System.Drawing;
using WEBCONTROLS = System.Web.UI.WebControls;
using MSCHART = System.Web.UI.DataVisualization.Charting;


namespace Viziblr.WebCharting
{
    public class FontFormat
    {
        public string Name;
        public float EmSize;

        public FontFormat(string name, float emsize)
        {
            this.Name = name;
            this.EmSize = emsize;
        }

        public SD.Font GetSDFont()
        {
            return new SD.Font(this.Name, this.EmSize);
        }

        public FontFormat Clone()
        {
            var o = new FontFormat(this.Name,this.EmSize);
            return o;
        }
    }

}