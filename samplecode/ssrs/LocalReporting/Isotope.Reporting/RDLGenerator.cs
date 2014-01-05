using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using SXL=System.Xml.Linq;
using IR = Isotope.Reporting.Modeling;
using Microsoft.Reporting.WinForms;

namespace Isotope.Reporting.RDL2005
{
    public class RDLGenerator
    {
        private static Report CreateReport(Modeling.Report repdef)
        {
            var report = new Report();
            SetReportProperties(report, repdef);
            AddDataSources(report, repdef);
            AddBody(report, repdef);
            AddDataSets(report, repdef);
            return report;
        }

        private static void SetReportProperties(Report report, Modeling.Report report_model)
        {
            var repdef = report_model.PageLayout;

            report.Width = repdef.BodyWidth;
            report.PageWidth = repdef.PageWidth;
            report.PageHeight = repdef.PageHeight;
            report.LeftMargin = repdef.LeftMargin;
            report.RightMargin = repdef.RightMargin;
            report.TopMargin = repdef.TopMargin;
            report.BottomMargin = repdef.BottomMargin;

            var docprops = report_model.DocumentProperties;
            report.Language = docprops.Language;
            report.Author = docprops.Author;
        }

        private static void AddDataSources(Report report, Modeling.Report repdef)
        {
            var datasource0 = new DataSource();
            datasource0.DatasetName = repdef.DatasetName;
            report.DataSources.Add(datasource0);
        }

        private static void AddDataSets(Report report, Modeling.Report repdef)
        {
            var dataset = new XDataSet();

            report.DataSets.Add(dataset);
            dataset.Name = repdef.DatasetName;
            dataset.CommandText = "";
            dataset.DataSourceName = repdef.DatasetName;

            for (int colindex = 0; colindex < repdef.DataTable.Columns.Count; colindex++)
            {
                var dt_col = repdef.DataTable.Columns[colindex];

                var field = new Field();
                field.Name = dt_col.ColumnName;
                field.DataField = dt_col.ColumnName;
                dataset.Fields.Add(field);
            }
        }

        private static void AddBody(Report report, Modeling.Report repdef)
        {
            var coldefs = new List<Isotope.Reporting.RDL2005.ColumnDef>();

            coldefs.AddRange(repdef.ColumnDefinitions);


            while (coldefs.Count < repdef.DataTable.Columns.Count)
            {
                var coldef = new ColumnDef();
                coldef.Width = repdef.DefaultColumnWidth;
                coldefs.Add(coldef);
            }


            var body = new Body();
            SetBodyProperties(repdef, body);
            AddTitle(repdef, body);
            AddTableDataRegion(coldefs, repdef, body);

            report.Body = body;
        }

        private static void SetBodyProperties(Modeling.Report repdef, Body body)
        {
            body.Height = repdef.PageLayout.BodyHeight;
            body.ColumnSpacing = repdef.BodyColumnSpacing;
        }

        private static void AddTableDataRegion(List<ColumnDef> coldefs, Modeling.Report repdef, Body body)
        {
            var table = new Table();
            table.Name = "REPORT_DATAREGION";
            table.Top = repdef.DataRegionTop;
            table.DatasetName = repdef.DatasetName;
            AddTableHeader(repdef, table, coldefs);
            AddTableDetailCells(coldefs, repdef, table);
            AddTableDataColumns(coldefs, repdef, table);
            AddTableGrouping(repdef, table);
            body.ReportItems.Add(table);
        }

        private static void AddTableDetailCells(List<ColumnDef> coldefs, Modeling.Report repdef, Table table)
        {
            var tablerow = new TableRow();

            for (int colindex = 0; colindex < repdef.DataTable.Columns.Count; colindex++)
            {
                var coldef = coldefs[colindex];
                var datatable_col = repdef.DataTable.Columns[colindex];

                var tablecell = new TableCell();
                tablerow.TableCells.Add(tablecell);


                var tbval = Util.GetExprFieldValue(datatable_col.ColumnName);
                var tbname = "DETAIL_COL_" + colindex.ToString() + "_TEXTBOX";
                var textbox = new TextBox(tbname, tbval);
                tablecell.ReportItems.Add(textbox);

                textbox.CanGrow = true;
                textbox.Height = repdef.HeaderHeight;
                textbox.Width = coldef.Width;

                textbox.Style.FontFamily = repdef.DefaultFontFamily;
                textbox.Style.FontSize = repdef.DefaultDetailRowFontSize;

                textbox.Style.BorderStyle.Default = repdef.TableCellBorderStyle;
                textbox.Style.BorderColor.Default.ColorName = repdef.TableCellBorderColor;
                textbox.Style.BorderWidth.Default = repdef.TableCellBorderWidth;

                if (repdef.DefaultDetailRowBackgroundColor != null)
                {
                    if (repdef.DefaultDetailRowBackgroundColorAlternate == null)
                    {
                        textbox.Style.BackgroundColor.ColorName = repdef.DefaultDetailRowBackgroundColor;
                    }
                    else
                    {
                        textbox.Style.BackgroundColor.Expression =
                            string.Format("=IIF(RowNumber(Nothing) Mod 2 = 0,\"{0}\",\"{1}\")",
                                          repdef.DefaultDetailRowBackgroundColor,
                                          repdef.DefaultDetailRowBackgroundColorAlternate);
                    }
                }
            }

            tablerow.Height = repdef.DetailRowHeight;
            table.Details.TableRows.Add(tablerow);
        }

        private static void AddTableDataColumns(List<ColumnDef> coldefs, Modeling.Report repdef, Table table)
        {
            for (int colindex = 0; colindex < repdef.DataTable.Columns.Count; colindex++)
            {
                var dt_col = repdef.DataTable.Columns[colindex];
                var coldef = coldefs[colindex];
                var tablecol = new TableColumn();
                tablecol.Width = coldef.Width;

                table.TableColumns.Add(tablecol);
            }
        }

        private static void AddTitle(Modeling.Report repdef, Body body)
        {
            var tbtitle = new TextBox("REPORT_TITLE", repdef.ReportTitle);
            tbtitle.CanGrow = true;
            tbtitle.Height = repdef.TitleHeight;
            tbtitle.Style.FontFamily = repdef.DefaultFontFamily;
            tbtitle.Style.FontSize = repdef.DefautlReportTitleFontSize;

            body.ReportItems.Add(tbtitle);
        }

        private static void AddTableGrouping(Modeling.Report repdef, Table table)
        {
            for (int grouping_col_index = 0; grouping_col_index < repdef.GroupOnColumns.Count; grouping_col_index++)
            {
                var grouping_col = repdef.GroupOnColumns[grouping_col_index];
                var tablegroup = new TableGroup();

                tablegroup.Name = string.Format("TABLE_GROUP_ON_COL_{0}_{1}", grouping_col, grouping_col_index);

                var col = repdef.DataTable.Columns[grouping_col];
                tablegroup.GroupingExpressions.Add(Util.GetExprFieldValue(col.ColumnName));

                table.TableGroups.Add(tablegroup);

                var tableheight = new TableRow();
                tableheight.Height = repdef.DetailRowHeight;
                tablegroup.Header.TableRows.Add(tableheight);
                for (int colindex = 0; colindex < repdef.DataTable.Columns.Count; colindex++)
                {
                    var tablecell = new TableCell();
                    tableheight.TableCells.Add(tablecell);
                    var textbox = new TextBox(tablegroup.Name + "_" + colindex.ToString(), null);
                    tablecell.ReportItems.Add(textbox);

                    if (colindex == grouping_col_index)
                    {
                        textbox.Value = Util.GetExprFieldValue(col.ColumnName);
                    }
                    else
                    {
                        textbox.Value = "";
                    }
                    textbox.Height = repdef.DetailRowHeight;
                }

                // TODO hide the column that was grouped!
            }
        }

        private static void AddTableHeader(Modeling.Report repdef, Table table, IList<ColumnDef> coldefs)
        {
            var tablerow = new TableRow();
            for (int colindex = 0; colindex < repdef.DataTable.Columns.Count; colindex++)
            {
                var coldef = coldefs[colindex];
                var dt_col = repdef.DataTable.Columns[colindex];
                var tablecell = new TableCell();
                var celltb = new TextBox("HEADER_COL_" + colindex.ToString() + "_TEXTBOX", dt_col.Caption);
                celltb.CanGrow = true;
                celltb.Height = repdef.HeaderHeight;
                celltb.Width = coldef.Width;

                celltb.Style.FontFamily = repdef.DefaultFontFamily;
                celltb.Style.FontSize = repdef.DefaultHeaderRowFontSize;
                celltb.Style.FontWeight = repdef.DefaultHeaderRowFontWeight;


                tablecell.ReportItems.Add(celltb);
                tablerow.TableCells.Add(tablecell);
            }

            tablerow.Height = repdef.HeaderHeight;
            table.Header.TableRows.Add(tablerow);
        }

        public static System.Xml.Linq.XDocument ShowReport(ReportViewer reportviewer,
                                      Modeling.Report report_model,
                                      ViewingOptions viewingoptions,
                                      System.Action<Isotope.Reporting.RDL2005.Report> mod_report)
        {
            var report = CreateReport(report_model);

            if (mod_report != null)
            {
                mod_report(report);
            }

            var new_dom = report.ReportToXMLDOM();

            if (viewingoptions.SaveRDLFile)
            {
                string temp_rdl = viewingoptions.OutputRDLFilename;
                new_dom.Save(temp_rdl);
            }


            reportviewer.LocalReport.DataSources.Clear();
            var rep_ds = new Microsoft.Reporting.WinForms.ReportDataSource(report_model.DatasetName, report_model.DataTable);
            reportviewer.LocalReport.DataSources.Add(rep_ds);
            reportviewer.ProcessingMode = ProcessingMode.Local;


            var mem_stream = SaveXMLToMemoryStream(new_dom);
            //reportviewer.LocalReport.ReportPath = temp_rdl;
            reportviewer.LocalReport.LoadReportDefinition(mem_stream);

            reportviewer.RefreshReport();

            return new_dom;
        }

        private static MemoryStream SaveXMLToMemoryStream(SXL.XDocument new_dom)
        {
            var mem_stream = new System.IO.MemoryStream();
            var ts = new System.IO.StreamWriter(mem_stream);
            new_dom.Save(ts);
            mem_stream.Seek(0, System.IO.SeekOrigin.Begin);
            return mem_stream;
        }
    }
}