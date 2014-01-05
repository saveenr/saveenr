using Isotope.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace IsotopeTests
{
    [TestClass]
    public class COMInteropUtilTest
    {
        [TestMethod]
        public void GetRunningObjectsTest()
        {
            var ros = Isotope.Interop.COMInterop.GetRunningObjects();
            foreach (Isotope.Interop.COMInterop.RunningObject ro in ros)
            {
            }
        }
    }
}