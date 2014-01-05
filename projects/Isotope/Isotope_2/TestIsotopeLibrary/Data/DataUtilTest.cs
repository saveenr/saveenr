using Isotope.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace IsotopeTests
{
    [TestClass]
    public class DataUtilTest
    {
        [TestMethod]
        public void DataTableFromEnumerableTest_linq()
        {
            var ints = new int[] {1, 2, 3};
            var items = from i in ints
                        select new {Value1 = i, Value2 = typeof (int).Name + "_" + i.ToString()};

            var datatable = Isotope.Data.DataTableUtil.DataTableFromEnumerable(items);

            Assert.AreEqual(3, datatable.Rows.Count);
            Assert.AreEqual(2, datatable.Columns.Count);
            Assert.AreEqual("Value1", datatable.Columns[0].ColumnName);
            Assert.AreEqual("Value1", datatable.Columns[0].Caption);
            Assert.AreEqual(typeof (int), datatable.Columns[0].DataType);
            Assert.AreEqual("Value2", datatable.Columns[1].ColumnName);
            Assert.AreEqual("Value2", datatable.Columns[1].Caption);
            Assert.AreEqual(typeof (string), datatable.Columns[1].DataType);
            Assert.AreEqual(1, datatable.Rows[0].ItemArray[0]);
            Assert.AreEqual(2, datatable.Rows[1].ItemArray[0]);
            Assert.AreEqual(3, datatable.Rows[2].ItemArray[0]);
            Assert.AreEqual("Int32_1", datatable.Rows[0].ItemArray[1]);
            Assert.AreEqual("Int32_2", datatable.Rows[1].ItemArray[1]);
            Assert.AreEqual("Int32_3", datatable.Rows[2].ItemArray[1]);
        }

        [TestMethod]
        public void DataTableFromEnumerableTest_linq_empty()
        {
            var ints = new int[] {1, 2, 3};
            var items = from i in ints
                        select new {Value1 = i, Value2 = typeof (int).Name + "_" + i.ToString()};

            var datatable = Isotope.Data.DataTableUtil.DataTableFromEnumerable(items);

            Assert.AreEqual(3, datatable.Rows.Count);
            Assert.AreEqual(2, datatable.Columns.Count);
            Assert.AreEqual("Value1", datatable.Columns[0].ColumnName);
            Assert.AreEqual("Value1", datatable.Columns[0].Caption);
            Assert.AreEqual(typeof (int), datatable.Columns[0].DataType);
            Assert.AreEqual("Value2", datatable.Columns[1].ColumnName);
            Assert.AreEqual("Value2", datatable.Columns[1].Caption);
            Assert.AreEqual(typeof (string), datatable.Columns[1].DataType);
        }

        public class ItemT
        {
            public string Name;
            public int? NullableAge;
        }

        [TestMethod]
        public void DataTableFromEnumerableTest_nullable()
        {
            var records = new[]
                              {
                                  new ItemT {Name = "Foo", NullableAge = null},
                                  new ItemT {Name = "Bar", NullableAge = 1}
                              };

            var items = from i in records
                        select new {i.Name, i.NullableAge};

            var datatable = Isotope.Data.DataTableUtil.DataTableFromEnumerable(items);

            Assert.AreEqual(2, datatable.Rows.Count);
            Assert.AreEqual(2, datatable.Columns.Count);
            Assert.AreEqual("Name", datatable.Columns[0].ColumnName);
            Assert.AreEqual("Name", datatable.Columns[0].Caption);
            Assert.AreEqual(typeof (string), datatable.Columns[0].DataType);
            Assert.AreEqual("NullableAge", datatable.Columns[1].ColumnName);
            Assert.AreEqual("NullableAge", datatable.Columns[1].Caption);
            Assert.AreEqual(typeof (int), datatable.Columns[1].DataType);
            Assert.AreEqual("Foo", datatable.Rows[0][0]);
            Assert.IsTrue(datatable.Rows[0][1] is System.DBNull);
            Assert.AreEqual("Bar", datatable.Rows[1][0]);
            Assert.AreEqual(1, datatable.Rows[1][1]);
        }

        public struct S1
        {
            public string Name { get; set; }
        }

        [TestMethod]
        public void DataTableFromEnumerableTest_struct()
        {
            var records = new[]
                              {
                                  new S1{Name = "Foo"},
                                  new S1{Name = "Bar"}
                              };

            var datatable = Isotope.Data.DataTableUtil.DataTableFromEnumerable(records);

            Assert.AreEqual(2, datatable.Rows.Count);
            Assert.AreEqual(1, datatable.Columns.Count);
            Assert.AreEqual("Name", datatable.Columns[0].ColumnName);
            Assert.AreEqual("Name", datatable.Columns[0].Caption);
            Assert.AreEqual(typeof(string), datatable.Columns[0].DataType);
            Assert.AreEqual("Foo", datatable.Rows[0][0]);
            Assert.AreEqual("Bar", datatable.Rows[1][0]);
        }


        /// <summary>
        ///A test for ExportToExcelXML
        ///</summary>
        [TestMethod]
        public void ExportToExcelXMLTest_test_null_values()
        {
            var datatable = new System.Data.DataTable();
            datatable.Columns.Add("DueDate", typeof (System.DateTimeOffset));
            datatable.Rows.Add(new object[] {null});

            string output_filename = System.IO.Path.GetTempFileName();

            DataExporter.ToExcelXML(datatable, output_filename, "Sheet1", true);

            System.IO.File.Delete(output_filename);
        }

    }
}