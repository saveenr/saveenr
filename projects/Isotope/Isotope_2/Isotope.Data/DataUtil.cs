using System.Collections.Generic;

namespace Isotope.Data
{
    internal static class DataUtil
    {

        internal static Isotope.Data.Formats.ExcelXMLWriter.DataType getdt(System.Type t)
        {
            var cdt = Isotope.Data.Formats.ExcelXMLWriter.DataType.String;
            if ((t == typeof(int))
                || t == typeof(double)
                || t == typeof(short)
                || t == typeof(long)
                || t == typeof(float)
                || t == typeof(byte)
                || t == typeof(sbyte)
                || t == typeof(ushort)
                || t == typeof(ulong)
                || t == typeof(ulong)
                || t == typeof(decimal))
            {
                cdt = Isotope.Data.Formats.ExcelXMLWriter.DataType.Number;
            }
            else if (t == typeof(string))
            {
                cdt = Isotope.Data.Formats.ExcelXMLWriter.DataType.String;
            }
            else if (t == typeof(System.DateTime))
            {
                cdt = Isotope.Data.Formats.ExcelXMLWriter.DataType.DateTime;
            }
            else if (t == typeof(System.DateTimeOffset))
            {
                cdt = Isotope.Data.Formats.ExcelXMLWriter.DataType.DateTime;
            }
            else
            {
                cdt = Isotope.Data.Formats.ExcelXMLWriter.DataType.String;
            }

            return cdt;
        }


        internal static string DataTableToExcelXML_get_cellstr(object item, Isotope.Data.Formats.ExcelXMLWriter.DataType excel_dt, System.Data.DataColumn datatable_col)
        {
            string cell_str;
            if (excel_dt == Isotope.Data.Formats.ExcelXMLWriter.DataType.Number)
            {
                cell_str = (item != null) ? item.ToString() : System.String.Empty;
            }
            else if (excel_dt == Isotope.Data.Formats.ExcelXMLWriter.DataType.String)
            {
                cell_str = (item != null) ? item.ToString() : System.String.Empty;
            }
            else if (excel_dt == Isotope.Data.Formats.ExcelXMLWriter.DataType.DateTime)
            {
                cell_str = DataTableToExcelXML_get_cellstr_datetime(item, datatable_col);
            }
            else
            {
                cell_str = (item != null) ? item.ToString() : System.String.Empty;
            }

            return cell_str;
        }

        internal static string DataTableToExcelXML_get_cellstr_datetime(object item, System.Data.DataColumn col)
        {
            string cell_str = null;
            string datetime_fmt = "yyyy-MM-ddTHH:mm:ss.fff";
            if (col.DataType == typeof(System.DateTime))
            {
                if (item == null)
                {
                    cell_str = System.String.Empty;
                }
                else if (item is System.DBNull)
                {
                    cell_str = System.String.Empty;
                }
                else
                {
                    var datetime = (System.DateTime)item;
                    cell_str = datetime.ToString(datetime_fmt);                                  
                }
            }
            else if (col.DataType == typeof(System.DateTimeOffset))
            {
                if (item == null)
                {
                    cell_str = System.String.Empty;
                }
                else if (item is System.DBNull)
                {
                    cell_str = System.String.Empty;
                }
                else
                {
                    var datetime = (System.DateTimeOffset)item;
                    var invariant_culture = System.Globalization.CultureInfo.InvariantCulture;
                    cell_str = datetime.ToString(datetime_fmt, invariant_culture);
                }
            }

            return cell_str;
        }


        internal static string DataTableToExcelXML_get_cellstr(object item, Isotope.Data.Formats.ExcelXMLWriter.DataType excel_dt, Schema.Field datatable_col)
        {
            string cell_str;
            if (excel_dt == Isotope.Data.Formats.ExcelXMLWriter.DataType.Number)
            {
                cell_str = (item != null) ? item.ToString() : System.String.Empty;
            }
            else if (excel_dt == Isotope.Data.Formats.ExcelXMLWriter.DataType.String)
            {
                cell_str = (item != null) ? item.ToString() : System.String.Empty;
            }
            else if (excel_dt == Isotope.Data.Formats.ExcelXMLWriter.DataType.DateTime)
            {
                cell_str = DataTableToExcelXML_get_cellstr_datetime(item, datatable_col);
            }
            else
            {
                cell_str = (item != null) ? item.ToString() : System.String.Empty;
            }

            return cell_str;
        }

        internal static string DataTableToExcelXML_get_cellstr_datetime(object item, Schema.Field col)
        {
            string cell_str = null;
            string datetime_fmt = "yyyy-MM-ddTHH:mm:ss.fff";
            if (col.Type == typeof(System.DateTime))
            {
                if (item == null)
                {
                    cell_str = System.String.Empty;
                }
                else if (item is System.DBNull)
                {
                    cell_str = System.String.Empty;
                }
                else
                {
                    var datetime = (System.DateTime)item;
                    cell_str = datetime.ToString(datetime_fmt);
                }
            }
            else if (col.Type == typeof(System.DateTimeOffset))
            {
                if (item == null)
                {
                    cell_str = System.String.Empty;
                }
                else if (item is System.DBNull)
                {
                    cell_str = System.String.Empty;
                }
                else
                {
                    var datetime = (System.DateTimeOffset)item;
                    var invariant_culture = System.Globalization.CultureInfo.InvariantCulture;
                    cell_str = datetime.ToString(datetime_fmt, invariant_culture);
                }
            }

            return cell_str;
        }
    }
}