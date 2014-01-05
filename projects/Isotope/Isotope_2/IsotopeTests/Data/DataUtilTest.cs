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

            var datatable = Isotope.Data.DataTableBuilder.FromEnumerable(items);

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

            var datatable = Isotope.Data.DataTableBuilder.FromEnumerable(items);

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

            var datatable = Isotope.Data.DataTableBuilder.FromEnumerable(items);

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

        /// <summary>
        ///A test for DataTableToDataSetXML
        ///</summary>
        [TestMethod]
        public void xDataTableToDataSetXMLTest()
        {
            var dt_now = System.DateTime.Now;
            var dto_Now = System.DateTimeOffset.Now;

            var datatable1 = new System.Data.DataTable();
            datatable1.Columns.Add("Name", typeof (string));
            datatable1.Columns.Add("Age", typeof (int));
            datatable1.Columns.Add("Alive", typeof (bool));
            datatable1.Columns.Add("Updated", typeof (System.DateTime));
            datatable1.Columns.Add("Accessed", typeof (System.DateTimeOffset));
            datatable1.Rows.Add("Akuma", 38, true, dt_now, dto_Now);
            datatable1.Rows.Add("Ken", 38, true, dt_now, dto_Now);
            datatable1.Rows.Add("Ryu", null, true, dt_now, dto_Now);

            datatable1.TableName = "Competitors";
            datatable1.Rows[2][2] = 100;
            string filename = System.IO.Path.GetTempFileName();

            var dataset1 = new System.Data.DataSet();
            dataset1.Tables.Add(datatable1);
            DataExporter.ToXML(dataset1, filename);

            var dataset2 = DataSetBuilder.FromXML(filename);
            var datatable2 = dataset2.Tables[0];

            Assert.AreEqual(5, datatable2.Columns.Count);
            Assert.AreEqual("Name", datatable2.Columns[0].ColumnName);
            Assert.AreEqual(typeof (string), datatable2.Columns[0].DataType);
            Assert.AreEqual("Age", datatable2.Columns[1].ColumnName);
            Assert.AreEqual(typeof (int), datatable2.Columns[1].DataType);
            Assert.AreEqual("Alive", datatable2.Columns[2].ColumnName);
            Assert.AreEqual(typeof (bool), datatable2.Columns[2].DataType);
            Assert.AreEqual("Updated", datatable2.Columns[3].ColumnName);
            Assert.AreEqual(typeof (System.DateTime), datatable2.Columns[3].DataType);
            Assert.AreEqual("Accessed", datatable2.Columns[4].ColumnName);
            Assert.AreEqual(typeof (System.DateTimeOffset), datatable2.Columns[4].DataType);

            Assert.AreEqual(3, datatable2.Rows.Count);
            var row0 = datatable2.Rows[0].ItemArray;
            var row1 = datatable2.Rows[1].ItemArray;
            var row2 = datatable2.Rows[2].ItemArray;
            Assert.AreEqual("Akuma", row0[0]);
            Assert.AreEqual(38, row0[1]);
            Assert.AreEqual(true, row0[2]);

            var actual_datetime = (System.DateTime) row0[3];
            Assert.AreEqual(dt_now.Year, actual_datetime.Year);
            Assert.AreEqual(dt_now.Month, actual_datetime.Month);
            Assert.AreEqual(dt_now.Day, actual_datetime.Day);
            Assert.AreEqual(dt_now.Hour, actual_datetime.Hour);
            Assert.AreEqual(dt_now.Minute, actual_datetime.Minute);
            Assert.AreEqual(dt_now.Second, actual_datetime.Second);
            Assert.AreEqual(dt_now.Millisecond, actual_datetime.Millisecond);

            Assert.AreEqual(typeof (System.DBNull), row2[1].GetType());

            System.IO.File.Delete(filename);
        }

        [TestMethod]
        public void ToDataTableTest12()
        {

        }
    }
}