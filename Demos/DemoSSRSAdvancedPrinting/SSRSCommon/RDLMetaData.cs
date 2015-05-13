using ExtensionMethods = SSRSCommon.Extensions.ExtensionMethods;

namespace SSRSCommon
{
    public class RDLMetaData
    {
        public string Namespace;

        public System.Drawing.SizeF PageSize;

        public float MarginLeft;
        public float MarginRight;
        public float MarginTop;
        public float MarginBottom;

        private RDLMetaData()
        {

        }

        public static RDLMetaData Load(System.Xml.Linq.XDocument doc)
        {


            var m = new RDLMetaData();
            m.Namespace = SSRSCommon.RSUtil.GetNamespace(doc);

            var root = doc.Root;


            var ph = m.ParseLength(ExtensionMethods.ElementRDL2005(root, "PageHeight").Value);
            var pw = m.ParseLength(ExtensionMethods.ElementRDL2005(root, "PageWidth").Value);

 
            
            var ml = m.ParseLength(ExtensionMethods.ElementRDL2005(root, "LeftMargin").Value);
            var mr = m.ParseLength(ExtensionMethods.ElementRDL2005(root, "RightMargin").Value);
            var mt = m.ParseLength(ExtensionMethods.ElementRDL2005(root, "TopMargin").Value);
            var mb = m.ParseLength(ExtensionMethods.ElementRDL2005(root, "BottomMargin").Value);


            m.PageSize = new System.Drawing.SizeF(pw, ph);
            m.MarginBottom = mb;
            m.MarginLeft = ml;
            m.MarginRight = mr;
            m.MarginTop = mt;

            return m;
        }

        private float ParseLength(string s)
        {
            if (s.EndsWith("mm"))
            {
                float mm = float.Parse(s.Replace("mm", ""));
                float inches = mm / 25.4f;
                return inches;
            }
            else if (s.EndsWith("in"))
            {
                float inches = float.Parse(s.Replace("in", ""));
                return inches;
            }
            else
            {
                float inches = float.Parse(s);
                return inches;

            }
        }
    }
}