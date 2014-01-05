using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    
    
    [TestClass]
    public class XmlDocWriterInternalTest
    {


        [TestMethod]
        public void CreateHtml()
        {
            var ss = new System.IO.StringWriter();
            var xw = System.Xml.XmlWriter.Create(ss);
            var sx = new Isotope.HTML.SimpleHTMLWriter(xw);
            sx.StartHtml();
            sx.StartHead();
            sx.Title("Untitled");
            sx.StartMeta();
            sx.EndMeta();
            sx.Link("stylesheet","text/vss","default.css");
            sx.StartScript("text/javascript");
            sx.EndScript();
            sx.StartStyle("text/css");
            sx.EndStyle();
            sx.EndHead();
            sx.StartBody();
            sx.Header(1,"Title1");
            sx.Para("Hello World");
            sx.EndBody();
            sx.EndHtml();
            sx.Flush();
            sx.Close();
        }


        [TestMethod]
        public void TestStack()
        {
            var ss = new System.IO.StringWriter();
            var xw = System.Xml.XmlWriter.Create(ss);
            var sx = new Isotope.HTML.SimpleHTMLWriter(xw);
            sx.StartHtml();
            sx.StartHead();
            bool caught = false;
            try
            {
                sx.EndHtml();
            }
            catch
            {
                caught = true;   
            }
            if (!caught)
            {
                Assert.Fail();
            }
            sx.Flush();
            sx.Close();
        }

    }
}
