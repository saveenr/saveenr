using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsotopeTests
{
    [TestClass]
    public class DisplayResolutionTest
    {
        /// <summary>
        ///A test for DisplayResolution Constructor
        ///</summary>
        [TestMethod]
        public void DisplayResolutionConstructorTest()
        {
            var resolutions = Isotope.Graphics.DisplayResolutions.GetResolutions().ToList();
            foreach (var r in resolutions)
            {
                double a = ((double) r.RatioWidth)/((double) r.RatioHeight);
                double b = ((double) r.Width)/((double) r.Height);

                if (a != b)
                {
                    Assert.Fail(r.Name);
                }
            }
        }
    }
}