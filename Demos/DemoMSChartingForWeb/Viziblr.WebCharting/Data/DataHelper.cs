using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Viziblr.WebCharting.Data
{
    public static class DataHelper
    {
        public static DataSetSingleSeries DataTableToChartDataSet(System.Data.DataTable dt, 
                                                                    int category_col,
                                                                  int val_col, bool exclude_dbnull_values)
        {
            var dt_cat_col = dt.Columns[category_col];
            var dt_val_col = dt.Columns[val_col];

            CheckCatCol(dt_cat_col);
            CheckValCol(dt_val_col);

            var cd = new DataSetSingleSeries();

            foreach (System.Data.DataRow row in dt.Rows)
            {
                var items = row.ItemArray;
                string cat = (string) items[category_col];
                object o = items[val_col];

                // exclude dbnull values
                var ot = o.GetType();
                if (exclude_dbnull_values && ot==typeof(System.DBNull))
                {
                    continue;
                }

                double val = GetVal(dt_val_col, o);
                var dp = new DataPoint(val);
                cd.Add(dp, cat);
            }

            return cd;
        }

        public static DataSetMultiSeries DataTableToChartDataSet(System.Data.DataTable dt, int category_col,
                                                          IList<int> val_cols, bool exclude_dbnull_values)
        {
            var dt_cat_col = dt.Columns[category_col];

            CheckCatCol(dt_cat_col);
            foreach (int val_col in val_cols)
            {
                var dt_val_col = dt.Columns[val_col];
                CheckValCol(dt_val_col);                
            }


            var list_of_series = new List<SeriesDataPoints>();
            foreach (int val_col in val_cols)
            {
                list_of_series.Add( new SeriesDataPoints());
            }

            var cats = new HashSet<string>();


            foreach (System.Data.DataRow row in dt.Rows)
            {

                var items = row.ItemArray;

                string cat = (string)items[category_col];
                cats.Add(cat);

                for (int i = 0; i < val_cols.Count;i++ )
                {
                    int val_col = val_cols[i];
                    var ser = list_of_series[i];

                    var dt_val_col = dt.Columns[val_col];
                    object o = items[val_col];

                    // exclude dbnull values
                    var ot = o.GetType();
                    if (exclude_dbnull_values && ot == typeof(System.DBNull))
                    {
                        continue;
                    }


                    double val = GetVal(dt_val_col, o);
                    var dp = new DataPoint(val);
                    ser.Add(dp);
                }
            }

            var axislabels = new WebCharting.Data.AxisLabels(cats);
            var cd = new DataSetMultiSeries( list_of_series, axislabels);


            return cd;
        }

        private static void CheckValCol(DataColumn col)
        {
            if (col.DataType != typeof(double) && col.DataType != typeof(int) && col.DataType != typeof(object))
            {
                string msg =
                    string.Format("Value column \"{0}\" must have type Double or Int32 or Object. Instead it has type \"{1}",
                                  col.ColumnName, col.DataType.FullName);

                throw new System.ArgumentException(msg);
            }
        }

        private static void CheckCatCol(DataColumn col)
        {
            if (col.DataType != typeof(string) && col.DataType != typeof(object))
            {
                string msg =
                    string.Format("Category column \"{0}\" must have type String or Object. Instead it has type \"{1}",
                                  col.ColumnName, col.DataType.FullName);

                throw new System.ArgumentException(msg);
            }
        }

        private static void CheckSerCol(DataColumn col)
        {
            if (col.DataType != typeof(string) && col.DataType != typeof(string))
            {
                string msg =
                    string.Format("Category column \"{0}\" must have type String. Instead it has type \"{1}",
                                  col.ColumnName, col.DataType.FullName);

                throw new System.ArgumentException(msg);
            }
        }


        public static DataSetMultiSeries DataTableToChartDataSet(System.Data.DataTable dt, int category_col, int val_col,
                                                                 int series_col, bool exclude_dbnull_values)
        {
            var dt_cat_col = dt.Columns[category_col];
            var dt_val_col = dt.Columns[val_col];
            var dt_ser_col = dt.Columns[series_col];

            CheckCatCol(dt_cat_col);
            CheckValCol(dt_val_col);
            CheckSerCol(dt_ser_col);


            var ser_to_values = new Dictionary<string, Dictionary<string,double>>();

            var cats = new HashSet<string>();

            foreach (System.Data.DataRow row in dt.Rows)
            {
                var items = row.ItemArray;
                string cat = (string)items[category_col];

                object o = items[val_col];

                // exclude dbnull values
                var ot = o.GetType();
                if (exclude_dbnull_values && ot == typeof(System.DBNull))
                {
                    continue;
                }

                cats.Add(cat);
            }


            foreach (System.Data.DataRow row in dt.Rows)
            {
                var items = row.ItemArray;
                string cat = (string) items[category_col];
                string ser = (string) items[series_col];
                object o = items[val_col];

                // exclude dbnull values
                var ot = o.GetType();
                if (exclude_dbnull_values && ot == typeof(System.DBNull))
                {
                    continue;
                }

                double val = GetVal(dt_val_col, o);

                if (!ser_to_values.ContainsKey(ser))
                {
                    ser_to_values[ser] = new Dictionary<string, double>(cats.Count);
                }

                var ser_list = ser_to_values[ser];

                ser_list[cat]=val;
            }



            var list_of_series = new List<SeriesDataPoints>();
            foreach (var ser_list in ser_to_values.Values)
            {
                var sdp = new SeriesDataPoints();
                foreach (string cat in cats)
                {
                    // Try to find a value for each category
                    if (ser_list.ContainsKey(cat))
                    {
                        double v = ser_list[cat];
                        var dp = new DataPoint(v);
                        sdp.Add(dp);
                    }
                    else
                    {
                        double v = 0.0;
                        var dp = new DataPoint(v);
                        sdp.Add(dp);
                    }
                }
            }
            var axislabels = new WebCharting.Data.AxisLabels(cats);
            var cd = new DataSetMultiSeries(list_of_series, axislabels);
            return cd;
        }

        private static double GetVal(System.Data.DataColumn col, object o)
        {
            double val;
            var ot = o.GetType();
            if (ot == typeof(double))
            {
                val = (double) o;
            }
            else if (ot == typeof(int))
            {
                val = (int) 0;
            }
            else if (ot == typeof(System.DBNull))
            {
                val = 0.0;
            }
            else
            {
                string msg = string.Format("Value in row has unexpected datatype {0}", ot.FullName);
                throw new SystemException(msg);
            }
            return val;
        }
    }
}