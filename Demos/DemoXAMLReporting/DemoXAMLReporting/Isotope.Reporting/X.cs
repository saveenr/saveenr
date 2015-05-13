using System.Data;
using System.Xml;

namespace Isotope.Reporting
{

    public static class X
    {

        public static void WriteStartFlowDocument(this System.Xml.XmlWriter x)
        {
            x.WriteStartElement("FlowDocument");
            x.WriteAttributeString("xmlns", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            x.WriteAttributeString("xmlns:x", "http://schemas.microsoft.com/winfx/2006/xaml");
        }

        public static void WriteEndFlowDocument(this System.Xml.XmlWriter x)
        {
            x.WriteEndElement();
        }


        public static void WriteStartParagraph(this System.Xml.XmlWriter x)
        {
            x.WriteStartElement("Paragraph");
        }

        public static void WriteEndParagraph(this System.Xml.XmlWriter x)
        {
            x.WriteEndElement();
        }

        public static void WriteStartSection(this System.Xml.XmlWriter x)
        {
            x.WriteStartElement("Section");
        }

        public static void WriteEndSection(this System.Xml.XmlWriter x)
        {
            x.WriteEndElement();
        }


        public static void WriteStartList(this System.Xml.XmlWriter x)
        {
            x.WriteStartElement("List");
        }

        public static void WriteEndList(this System.Xml.XmlWriter x)
        {
            x.WriteEndElement();
        }

        public static void WriteStartRun(this System.Xml.XmlWriter x)
        {
            x.WriteStartElement("Run");
        }

        public static void WriteEndRun(this System.Xml.XmlWriter x)
        {
            x.WriteEndElement();
        }

        public static void WriteRunValue(this System.Xml.XmlWriter x, string t)
        {
            x.WriteStartRun();
            if (t != null)
            {
                if (t.Length > 0)
                {
                    x.WriteString(t);
                }
            }
            x.WriteEndRun();
        }


        public static void WriteStartTable(this System.Xml.XmlWriter x)
        {
            x.WriteStartElement("Table");
        }

        public static void WriteEndTable(this System.Xml.XmlWriter x)
        {
            x.WriteEndElement();
        }

        public static void WriteStartTableRowGroup(this System.Xml.XmlWriter x)
        {
            x.WriteStartElement("TableRowGroup");
        }

        public static void WriteEndTableRowGroup(this System.Xml.XmlWriter x)
        {
            x.WriteEndElement();
        }

        public static void WriteStartTableRow(this System.Xml.XmlWriter x)
        {
            x.WriteStartElement("TableRow");
        }

        public static void WriteEndTableRow(this System.Xml.XmlWriter x)
        {
            x.WriteEndElement();
        }

        public static void WriteStartTableColumns(this System.Xml.XmlWriter x)
        {
            x.WriteStartElement("Table.Columns");
        }

        public static void WriteEndTableColumns(this System.Xml.XmlWriter x)
        {
            x.WriteEndElement();
        }

        public static void WriteStartTableColumn(this System.Xml.XmlWriter x)
        {
            x.WriteStartElement("TableColumn");
        }

        public static void WriteEndTableColumn(this System.Xml.XmlWriter x)
        {
            x.WriteEndElement();
        }

        public static void WriteStartTableCell(this System.Xml.XmlWriter x)
        {
            x.WriteStartElement("TableCell");
        }

        public static void WriteEndTableCell(this System.Xml.XmlWriter x)
        {
            x.WriteEndElement();
        }


        public static void WriteParagraphText(this System.Xml.XmlWriter x, string t)
        {
            x.WriteStartElement("Paragraph");
            if (t != null)
            {
                if (t.Length > 0)
                {
                    x.WriteString(t);
                }
            }
            x.WriteEndParagraph();
        }

        public static void WriteX(this System.Xml.XmlWriter x, Label label)
        {
            var t = label.Text;
            x.WriteStartElement("Paragraph");
            x.WriteTextStyleAttributes(label.TextStyle);
            if (t != null)
            {
                    x.WriteString(t);
            }
            x.WriteEndParagraph();
        }

        public static void WriteTextStyleAttributes(this System.Xml.XmlWriter x, TextStyle style)
        {
            if (style.FontFamily!= null)
            {
                x.WriteAttributeString("FontFamily",style.FontFamily);
            }
            if (style.FontSize >0 )
            {
                x.WriteAttributeString("FontSize", style.FontSize.ToString() );
            }
            if (style.FontWeight != FontWeight.None)
            {
                x.WriteAttributeString("FontWeight", style.FontWeight.ToString());
            }
        }

        public static void WriteX(this XmlWriter x, SimpleTable table)
        {

            while (table.ColumnStyles.Count<table.DataTable.Columns.Count)
            {
                var tcs = new TableColumnStyle();
                table.ColumnStyles.Add(tcs);
            }

            x.WriteStartTable();

            if (table.TableStyle.CellSpacing.HasValue)
            {
                x.WriteAttributeString("CellSpacing", table.TableStyle.CellSpacing.Value.ToString());
                
            }



            x.WriteStartTableColumns();
            int colindex = 0;
            foreach (System.Data.DataColumn col in table.DataTable.Columns)
            {
                x.WriteStartTableColumn();
                double width = table.TableStyle.DefaultColumnWidth;
                var col_style = table.ColumnStyles[colindex];
                if (col_style.Width>0)
                {
                    width = col_style.Width;
                }

                x.WriteAttributeString("Width", width.ToString());
                x.WriteEndTableColumn();
                colindex++;
            }
            x.WriteEndTableColumns();

            x.WriteStartTableRowGroup();
            x.WriteAttributeString("FontWeight", "Bold");

            x.WriteStartTableRow();
            x.WriteTextStyleAttributes(table.HeaderTextStyle);

            colindex = 0;
            foreach (System.Data.DataColumn col in table.DataTable.Columns)
            {
                var col_caption = col.Caption ?? col.ColumnName;
                x.WriteStartTableCell();
                var col_style = table.ColumnStyles[colindex];


                x.WriteStartElement("Paragraph");
                x.WriteAttributeString("TextAlignment", col_style.HorzontalAlignment.ToString());
                x.WriteString(col_caption);
                x.WriteEndParagraph();


                x.WriteEndTableCell();

                colindex++;


            }
            x.WriteEndTableRow();
            x.WriteEndTableRowGroup();
            x.WriteStartTableRowGroup();
            x.WriteTextStyleAttributes(table.DetailTextStyle);
            
            foreach (System.Data.DataRow row in table.DataTable.Rows)
            {
                x.WriteStartTableRow();
                for (int i = 0; i < table.DataTable.Columns.Count; i++)
                {
                    var col_obj_val = row[i];
                    var col_obj_type = col_obj_val.GetType();

                    var col_obj_val_str = col_obj_val.ToString();

                    x.WriteStartTableCell();

                    x.WriteAttributeString("BorderBrush", "Red");
                    x.WriteAttributeString("BorderThickness", "1");


                    var col_style = table.ColumnStyles[i];
                    x.WriteTextStyleAttributes(col_style.DetailTextStyle);

                    x.WriteStartElement("Paragraph");
                    x.WriteAttributeString("TextAlignment",col_style.HorzontalAlignment.ToString());
                    x.WriteString(col_obj_val_str);
                    x.WriteEndParagraph();

                    x.WriteEndTableCell();


                }
                x.WriteEndTableRow();

            }
            x.WriteEndTableRowGroup();
            x.WriteEndTable();
        }


    }
}
