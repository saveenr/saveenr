using System.Collections.Generic;
using System.Linq;

namespace Isotope.Data
{
    public static class DataTableUtil
    {

        public static System.Data.DataTable DataTableFromEnumerable<T>(IEnumerable<T> items) 
        {
            // based partially on: http://stackoverflow.com/questions/1253725/convert-ienumerable-to-datatable

            if (items == null)
            {
                return null;
            }
            
            // Use reflection to get Columns from the properties of the type
            var itemtype = typeof(T);
            var binding_flags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance;
            var props = itemtype.GetProperties(binding_flags);

            var datatable = new System.Data.DataTable();
            foreach (var prop in props)
            {
                var colType = prop.PropertyType;
                if ( (colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(System.Nullable<>)))
                {
                    // if the property is a nullable, then use the first generic argument as the column type
                    colType = colType.GetGenericArguments()[0];
                }
                var col = new System.Data.DataColumn(prop.Name, colType);
                datatable.Columns.Add(col);
            }

            // Optimize for retrieving the property values
            var prop_getters = new List<System.Func<T, object>>();
            foreach (var prop in props)
            {
                var func = ReflectionUtility.GetGetter<T>(prop);
                prop_getters.Add(func);
            }

            // Put data in the table
            datatable.BeginLoadData();
            var itemarray = new object[props.Length];
            foreach (T item in items)
            {
                for (int i = 0; i < props.Length; i++)
                {
                    var prop_info = props[i];
                    //var colvalue = prop_info.GetValue(item, null) ?? System.DBNull.Value;
                    var colvalue = prop_getters[i](item) ?? System.DBNull.Value;
                    itemarray[i] = colvalue;
                }
                var dr = datatable.Rows.Add(itemarray);
            }
            datatable.EndLoadData();
            return datatable;
        }

        public static System.Data.DataTable DataTableFromExcelWorksheet(string filename, string worksheet_name, bool first_row_column_names)
        {
            string connection_string = get_excel_ws_cxn_string(filename, first_row_column_names);
            string command_string = string.Format("select * From [{0}$]", worksheet_name);

            var dataset = new System.Data.DataSet();
            var connection = new System.Data.OleDb.OleDbConnection(connection_string);

            try
            {
                connection.Open();
                var oledbcmd = new System.Data.OleDb.OleDbCommand(command_string, connection);
                var dataadapter = new System.Data.OleDb.OleDbDataAdapter(oledbcmd);
                dataadapter.Fill(dataset);
            }
            finally
            {
                connection.Close();
            }

            return dataset.Tables[0];
        }

        public static System.Data.DataTable DataTableFromCSVFile(string path)
        {
            const bool first_row_column_names = false;

            var connection_string = get_csv_cxn_string(path, first_row_column_names);

            string abs_path = System.IO.Path.GetFullPath(path);
            string basename = System.IO.Path.GetFileName(abs_path);
            string command_string = "select * from " + basename;

            var datatable = new System.Data.DataTable();
            var dataadapter = new System.Data.OleDb.OleDbDataAdapter(command_string, connection_string);
            dataadapter.Fill(datatable);
            dataadapter.Dispose();

            return datatable;
        }

        private static string get_csv_cxn_string(string path, bool first_row_column_names)
        {
            string hdr = gethdr(first_row_column_names);
            const string provider = "Microsoft.Jet.Oledb.4.0";
            string abs_path0 = System.IO.Path.GetFullPath(path);
            string data_source = System.IO.Path.GetDirectoryName(abs_path0);
            string exprop = "text" + ";" + hdr + ";" + "FMT=Delimited";
            return build_cxn_string(provider, data_source, exprop);
        }

        private static string get_excel_ws_cxn_string(string filename, bool first_row_column_names)
        {
            ////const string provider1 = "Microsoft.Jet.Oledb.4.0";
            ////const string excel_12_xml = "Excel 12.0 Xml";
            const string provider2 = "Microsoft.ACE.OLEDB.12.0";
            const string excel_8 = "Excel 8.0";
            const string imex = "IMEX=1";
            string hdr = gethdr(first_row_column_names);

            return build_cxn_string(provider2, filename, excel_8 + ";" + hdr + ";" + imex);
        }

        private static string build_cxn_string(string provider, string ds, string exprops)
        {
            var dic = new Dictionary<string, string>()
                          {
                              { "Provider", provider },
                              { "Data Source", ds },
                              { "Extended Properties", exprops}
                          };
            var invariant_culture = System.Globalization.CultureInfo.InvariantCulture;
            var kvpairs = dic.Select(pair => string.Format(invariant_culture, "{0}=\"{1}\"", pair.Key, pair.Value));
            string connection_string = string.Join(";", kvpairs.ToArray());
            return connection_string;
        }

        private static string gethdr(bool first_row_column_names)
        {
            string hdr = first_row_column_names ? "HDR=Yes" : "HDR=No";
            return hdr;
        }

    }

    internal class ReflectionUtility
    {

        // based partially on : http://stackoverflow.com/questions/1253725/convert-ienumerable-to-datatable

        internal static System.Func<T, object> GetGetter<T>(System.Reflection.PropertyInfo property)
        {
            // get the get method for the property
            var method = property.GetGetMethod(true);

            // get the generic get-method generator (ReflectionUtility.GetSetterHelper<TTarget, TValue>)
            var genericHelper = typeof(ReflectionUtility).GetMethod(
                "GetGetterHelper",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            // reflection call to the generic get-method generator to generate the type arguments
            var constructedHelper = genericHelper.MakeGenericMethod(
                method.DeclaringType,
                method.ReturnType);

            // now call it. The null argument is because it's a static method.
            object ret = constructedHelper.Invoke(null, new object[] { method });

            // cast the result to the action delegate and return it
            return (System.Func<T, object>)ret;
        }

        static System.Func<object, object> GetGetterHelper<TTarget, TResult>(System.Reflection.MethodInfo method)
            where TTarget : class // target must be a class as property sets on structs need a ref param
        {
            // Convert the slow MethodInfo into a fast, strongly typed, open delegate
            var func = (System.Func<TTarget, TResult>)System.Delegate.CreateDelegate(typeof(System.Func<TTarget, TResult>), method);

            // Now create a more weakly typed delegate which will call the strongly typed one
            System.Func<object, object> ret = (object target) => (TResult)func((TTarget)target);
            return ret;
        }
    }
}