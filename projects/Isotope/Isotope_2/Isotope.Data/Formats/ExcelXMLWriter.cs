using System.Collections.Generic;
using System.Linq;

namespace Isotope.Data.Formats
{
    public class ExcelXMLWriter
    {
        public enum DataType
        {
            String,
            Number,
            DateTime
        }

        private System.Xml.XmlWriter xwriter;

        private const string s_nsp = "s";
        private const string s_ns = "urn:schemas-microsoft-com:office:spreadsheet";

        private const string x_nsp = "x";
        private const string x_ns = "urn:schemas-microsoft-com:office:excel";

        private const string o_nsp = "o";
        private const string o_ns = "urn:schemas-microsoft-com:office:office";

        private const string datetime_styleid = "s62";

        public ExcelXMLWriter(string filename)
        {
            var settings = new System.Xml.XmlWriterSettings();
            settings.Indent = true;
            xwriter = System.Xml.XmlWriter.Create(filename, settings);
        }

        public ExcelXMLWriter(System.Xml.XmlWriter xmlwriter)
        {
            this.xwriter = xmlwriter;
        }

        public void StartDocument()
        {
            xwriter.WriteStartDocument();
            xwriter.WriteProcessingInstruction("mso-application", "progid='Excel.Sheet'");
        }

        public void EndDocument()
        {
            xwriter.WriteEndDocument();
        }

        public void StartWorkBook()
        {
            xwriter.WriteStartElement(s_nsp, "Workbook", s_ns);
            xwriter.WriteAttributeString(x_nsp, "xmlns", x_ns);
            xwriter.WriteAttributeString(o_nsp, "xmlns", o_ns);
            xwriter.WriteAttributeString(s_nsp, "xmlns", s_ns);

            this.StartStyles();
            this.Style(datetime_styleid, null, @"[$-409]yyyy/mm/dd\ hh:mm\ AM/PM;@");
            this.EndStyles();
        }

        public void EndWorkBook()
        {
            xwriter.WriteEndElement();
        }

        public void StartWorkSheet(string name, int numcols)
        {
            this.StartWorkSheet(name, numcols, null);
        }

        public void StartWorkSheet(string name, int numcols, IList<DataType> coltypes)
        {
            xwriter.WriteStartElement(s_nsp, "Worksheet", s_ns);
            xwriter.WriteAttributeString(s_nsp, "Name", s_ns, name);
            xwriter.WriteStartElement(s_nsp, "Table", s_ns);

            this.StartColumns();
            for (int i = 0; i < numcols; i++)
            {
                if (coltypes == null)
                {
                    this.Column();
                }
                else
                {
                    this.Column(coltypes[i]);
                }
            }
            this.EndColumns();
        }

        public void StartColumns()
        {
        }

        public void EndColumns()
        {
        }

        public void Column()
        {
            xwriter.WriteStartElement(s_nsp, "Column", s_ns);
            xwriter.WriteEndElement(); // s:Column            
        }

        public void Column(DataType dt)
        {
            xwriter.WriteStartElement(s_nsp, "Column", s_ns);
            if (dt == DataType.DateTime)
            {
                xwriter.WriteAttributeString(s_nsp, "StyleID", s_ns, datetime_styleid);
            }
            xwriter.WriteEndElement(); // s:Column            
        }

        private void StartStyles()
        {
            xwriter.WriteStartElement(s_nsp, "Styles", s_ns);
        }

        private void Style(string id, string name, string numberformat)
        {
            xwriter.WriteStartElement(s_nsp, "Style", s_ns);
            xwriter.WriteAttributeString("ID", s_ns, id);
            if (name != null)
            {
                xwriter.WriteAttributeString("Name", s_ns, name);
            }
            if (numberformat != null)
            {
                xwriter.WriteStartElement(s_nsp, "NumberFormat", s_ns);
                xwriter.WriteAttributeString("Format", s_ns, numberformat);
                xwriter.WriteEndElement();
            }
            xwriter.WriteEndElement(); // s:Style            
        }

        private void EndStyles()
        {
            xwriter.WriteEndElement(); // s:Styles            
        }

        public void StartRow()
        {
            xwriter.WriteStartElement(s_nsp, "Row", s_ns);
        }

        public void EndRow()
        {
            xwriter.WriteEndElement(); // s:Row
        }

        public void Cell(string data, DataType type)
        {
            xwriter.WriteStartElement(s_nsp, "Cell", s_ns);

            xwriter.WriteStartElement(s_nsp, "Data", s_ns);
            string datatype_str = null;
            if (type == DataType.String)
            {
                datatype_str = "String";
            }
            else if (type == DataType.Number)
            {
                datatype_str = "Number";
            }
            else if (type == DataType.DateTime)
            {
                datatype_str = "DateTime";
            }
            else
            {
                datatype_str = "String";
            }

            xwriter.WriteAttributeString(s_nsp, "Type", s_ns, datatype_str);

            xwriter.WriteString(data);
            xwriter.WriteEndElement(); // s:Data
            xwriter.WriteEndElement(); // s:Cell
        }

        public void EndWorkSheet()
        {
            xwriter.WriteEndElement(); // s:Table
            xwriter.WriteEndElement(); //s:Worksheet
        }

        public void Close()
        {
            xwriter.Close();
        }
    }
}