using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class AtomFeedTest
    {
        [TestMethod]
        public void LoadTest()
        {
            //var feedxml_string = IsotopeTests.Properties.Resources.AtomFeedSample1;
            //var feed = Isotope.Internet.Atom.AtomFeed.LoadXML(feedxml_string);
        }

        [TestMethod]
        public void AtomFeedConstructorTest()
        {
            var feed = new Isotope.Atom.AtomFeed();
            Assert.AreEqual(0, feed.Entries.Count);
        }

        [TestMethod]
        public void AddRemoveFeedItems()
        {
            var feed = new Isotope.Atom.AtomFeed();
            var entry1 = new Isotope.Atom.AtomEntry();
            var entry2 = new Isotope.Atom.AtomEntry();
            feed.Entries.Add(entry1);
            feed.Entries.Add(entry2);
            Assert.AreEqual(2, feed.Entries.Count);
        }
    }
}