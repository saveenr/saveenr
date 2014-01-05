using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Isotope.Cloud.Tests
{
    [TestClass]
    public class AdobeKulerTests
    {
        private const string kuler_apikey = "85387E02911F6599FB93A8EFF7173821";
                             // substitue this with your own Kuler API key

        [TestMethod]
        public void TestKulerListTypes()
        {
            // Get 5 items from the different kinds of lists: recent, random, popular, etc..

            foreach (var listype in System.Enum.GetValues(typeof(Isotope.Cloud.Adobe.Kuler.ThemeListType)))
            {
                var req = new Isotope.Cloud.Adobe.Kuler.ThemeListRequest(kuler_apikey);
                req.ListType = Isotope.Cloud.Adobe.Kuler.ThemeListType.Recent;
                req.ItemsPerPage = 5;
                string url = req.GetURL();
                var theme_list = Isotope.Cloud.Adobe.Kuler.ThemesResult.Load(url);
                Assert.AreEqual(req.ItemsPerPage, theme_list.Themes.Count);
            }
        }

        [TestMethod]
        public void TestKulerPopularFeed()
        {
            // Get most popular feeds for the last 30 days 
            var req = new Isotope.Cloud.Adobe.Kuler.ThemeListRequest(kuler_apikey);
            req.ListType = Isotope.Cloud.Adobe.Kuler.ThemeListType.Popular;
            req.TimeSpan = 30;
            string url = req.GetURL();
            Isotope.Cloud.Adobe.Kuler.ThemesResult.Load(url);
        }

        [TestMethod]
        public void TestKulerItemsPerPage()
        {
            var req = new Isotope.Cloud.Adobe.Kuler.ThemeListRequest(kuler_apikey);
            req.ListType = Isotope.Cloud.Adobe.Kuler.ThemeListType.Popular;
            req.StartIndex = 0;
            req.ItemsPerPage = 5;
            string url = req.GetURL();
            Isotope.Cloud.Adobe.Kuler.ThemesResult.Load(url);
        }
    }
}