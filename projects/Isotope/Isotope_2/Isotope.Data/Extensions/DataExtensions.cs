using System.Collections.Generic;

namespace Isotope.Data.Extensions
{
    public static class DataExtensions
    {
        public static IEnumerable<System.Data.DataColumn> AsEnumerable(this System.Data.DataColumnCollection datacolumns)
        {
            foreach (System.Data.DataColumn col in datacolumns)
            {
                yield return col;
            }
        }

        public static IEnumerable<System.Data.DataRow> AsEnumerable(this System.Data.DataRowCollection datarows)
        {
            foreach (System.Data.DataRow row in datarows)
            {
                yield return row;
            }
        }

        public static IEnumerable<System.Data.DataTable> AsEnumerable(this System.Data.DataTableCollection datatables)
        {
            foreach (System.Data.DataTable table in datatables)
            {
                yield return table;
            }
        }
    }
}