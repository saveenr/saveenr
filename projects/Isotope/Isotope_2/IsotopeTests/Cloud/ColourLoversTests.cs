using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Isotope.Cloud.Tests
{
    [TestClass]
    public class ColourLoversTests
    {
        [TestMethod]
        public void TestListTypes()
        {
            var req = new Isotope.Cloud.ColourLovers.PaletteRequest();
            req.ListType = Isotope.Cloud.ColourLovers.PaletteListType.New;
            req.NumResults = 5; //  
            string url = req.GetURL();
            var theme_list = Isotope.Cloud.ColourLovers.PalettesResult.Load(url);
            //TEST.Assert.AreEqual(req.ItemsPerPage, theme_list.Themes.Count);
        }
    }
}