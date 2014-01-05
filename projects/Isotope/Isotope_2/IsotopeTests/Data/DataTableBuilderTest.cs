using Enumerable = System.Linq.Enumerable;
using Isotope.Data.Extensions;

namespace IsotopeTests
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class DataTableBuilderTest
    {
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void LoadFromExcelXLSX()
        {

            var filename = IsotopeTestCommon.Helper.GetTestMethodOutputFilename("Customers.xlsx");
            var data = IsotopeTests.Properties.Resources.Customers;
            var fp = System.IO.File.Create(filename);
            fp.Write(data, 0, data.Length);
            fp.Close();

            var dt = Isotope.Data.DataTableBuilder.FromExcelWorksheet(filename, "Sheet1", true);

            var colnames = Enumerable.Select<System.Data.DataColumn, string>(dt.Columns.AsEnumerable(), col => col.ColumnName);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(Enumerable.Contains(colnames, "Telephone"));

        }
    }
}