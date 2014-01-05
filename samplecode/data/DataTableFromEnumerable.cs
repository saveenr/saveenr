using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTableFromEnumerable
{
    public class DataTableUtil
    {
        public System.Data.DataTable DataTableFromEnumerable<T>(IEnumerable<T> items)
        {
            if (items == null)
            {
                return null;
            }

            var datatable = new System.Data.DataTable();

            // Use reflection to get Columns from the properties of the type
            var itemtype = typeof(T);
            System.Reflection.PropertyInfo[] propinfo_array = itemtype.GetProperties();
            foreach (var propinfo in propinfo_array)
            {
                var colType = propinfo.PropertyType;
                if (
                    (colType.IsGenericType) &&
                    (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }

                var col = new System.Data.DataColumn(propinfo.Name, colType);
                datatable.Columns.Add(col);
            }

            // Put data in the table
            datatable.BeginLoadData();
            var itemarray = new object[propinfo_array.Length];
            foreach (T item in items)
            {
                for (int i = 0; i < propinfo_array.Length; i++)
                {
                    var prop_info = propinfo_array[i];
                    var colvalue = prop_info.GetValue(item, null) ?? DBNull.Value;
                    itemarray[i] = colvalue;
                }

                var dr = datatable.Rows.Add(itemarray);
            }
            datatable.EndLoadData();
            return datatable;
        }
    }
}
