using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

namespace SSRSCommon
{
    public static class RSUtil
    {
        public static ReportExecutionService.ReportExecutionService ConnectToReportExecutionService()
        {
            Console.WriteLine("Connecting To Report Execution Service");
            var rep_exec_svc = new ReportExecutionService.ReportExecutionService();
            rep_exec_svc.Credentials = System.Net.CredentialCache.DefaultCredentials;
            return rep_exec_svc;
        }

        public static ReportService2005.ReportingService2005 ConnectToReportingService()
        {
            Console.WriteLine("Connecting To Reporting Service");
            var rep_svc = new ReportService2005.ReportingService2005();
            rep_svc.Credentials = System.Net.CredentialCache.DefaultCredentials;
            return rep_svc;
        }

        public static string GetReportServerUrl(SSRSCommon.ReportExecutionService.ReportExecutionService svc)
        {
            var s = svc.Url;
            var tokens = s.Split(new char[] { '/' });
            return string.Join("/", tokens.Take(tokens.Length - 1).ToArray()) + "/";
        }


        private static string get_correct_url(string s)
        {
            var tokens = s.Split(new[] { '/' });
            var tokens2 = tokens.Take(tokens.Length - 1).ToArray();
            return string.Join("/", tokens2);
        }

        static System.Drawing.Size emf_render_dpi = new System.Drawing.Size(300, 300);

        public static List<System.Drawing.Imaging.Metafile> RenderMetafilesForReport(
            SSRSCommon.ReportService2005.ReportingService2005  rep_svc,
            SSRSCommon.ReportExecutionService.ReportExecutionService rep_exec_svc, string reportpath, EMFRenderPrefs userprintprefs, int FirstNPages)
        {

            var exec_header = new SSRSCommon.ReportExecutionService.ExecutionHeader();

            rep_exec_svc.ExecutionHeaderValue = exec_header;
            string historyid = null;

            Console.WriteLine("Loading Report");
            var exec_info = rep_exec_svc.LoadReport(reportpath, historyid);

            //rs2.SetExecutionParameters(parameters, "en-us");

            var session_id = rep_exec_svc.ExecutionHeaderValue.ExecutionID;
            Console.WriteLine("Session ID: {0}", rep_exec_svc.ExecutionHeaderValue.ExecutionID);

            var streams = new List<System.IO.Stream>();
            var metafiles = new List<System.Drawing.Imaging.Metafile>();
            string render_format = "IMAGE";

            int cur_page_count = 0;

            try
            {
                // Create the Viewer and Bind it to the Server Report
                Console.WriteLine("Creating the ReportViewer Control");
                var viewer = new Microsoft.Reporting.WinForms.ReportViewer();
                viewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                var server_report = viewer.ServerReport;
                Console.WriteLine("Server Report DisplayName: {0}", server_report.DisplayName);
                Console.WriteLine("Server Report ReportPath: {0}", server_report.ReportPath);
                Console.WriteLine("Server Report ReportServerUrl: {0}", server_report.ReportServerUrl);
                Console.WriteLine("Server Report Timeout: {0}", server_report.Timeout);


                viewer.ServerReport.ReportServerUrl = new Uri(get_correct_url(rep_svc.Url));
                viewer.ServerReport.ReportPath = reportpath;
                Console.WriteLine("Report Viewer ProcessingMode: {0}", viewer.ProcessingMode);
                Console.WriteLine("Report Viewer ReportServerUrl: {0}", viewer.ServerReport.ReportServerUrl);
                Console.WriteLine("Report Viewer ReportPath: {0}", viewer.ServerReport.ReportPath);

                // Setup URL Parameters
                Console.WriteLine("Creating the URL Parameters");
                var url_params = new System.Collections.Specialized.NameValueCollection();
                url_params.Add("rs:PersistStreams", "True");

                // Create DeviceInfo XML
                var devinfo = new SSRSCommon.DeviceInfo();
                devinfo.OutputFormat = "EMF"; // force it to emf
                devinfo.Toolbar = false;
                devinfo.PrintDpiX = emf_render_dpi.Width;
                devinfo.PrintDpiY = emf_render_dpi.Height;
                // Finished with DeviceInfo XML

                string device_info = devinfo.ToString();
                Console.WriteLine("DeviceInfo: {0}", device_info);

                // render first stream
                Console.WriteLine("Starting Rendering First Stream");

                string rendered_mimetype;
                string rendered_extension;

                var rendered_stream = server_report.Render(
                    render_format,
                    device_info,
                    url_params,
                    out rendered_mimetype,
                    out rendered_extension);

                Console.WriteLine("Finished Rendering First Stream");

                streams.Add(rendered_stream);

                cur_page_count++;
                // Handle the addtional streams

                Console.WriteLine("Retrieving Additional Streams");
                url_params.Remove("rs:PersistStreams");
                url_params.Add("rs:GetNextStream", "True");
                do
                {
                    // Check to see if user only wanted the first N pages of the report

                    if (FirstNPages > 0)
                    {
                        if (cur_page_count >= FirstNPages)
                        {
                            break;
                        }
                    }

                    // ------------
                    Console.WriteLine("Starting Rendering Additional Stream");
                    rendered_stream = server_report.Render(
                        render_format,
                        device_info,
                        url_params,
                        out rendered_mimetype,
                        out rendered_extension);

                    if (rendered_stream.Length != 0)
                    {
                        Console.WriteLine("Storing stream");
                        streams.Add(rendered_stream);
                    }
                    else
                    {
                        Console.WriteLine("Received stream of length zero");
                    }

                    Console.WriteLine("Finished Rendering Additional Stream");
                    cur_page_count++;
                } while (rendered_stream.Length > 0);
                Console.WriteLine("Finished Retrieving Additional Streams");
            }
            catch (System.Web.Services.Protocols.SoapException err)
            {
                Console.WriteLine("Caught SoapException");
                Console.WriteLine("Message: {0}", err.Message);
                throw err;
            }
            finally
            {
                // Convert each stream into a metafile

                foreach (var stream in streams)
                {
                    try
                    {
                        Console.WriteLine("Stream type: {0}", stream.GetType());
                        Console.WriteLine("Converting to metafile");

                        var memstream = (System.IO.MemoryStream)stream;
                        Console.WriteLine("memstream size: {0}", memstream.Length);
                        var metafile = new System.Drawing.Imaging.Metafile(memstream);
                        metafiles.Add(metafile);
                        Console.WriteLine("metafile size: {0}", metafile.Size);
                    }
                    finally
                    {
                        // Get rid of the steams
                        stream.Close();
                        stream.Dispose();
                    }
                }

                streams = null;
            }
            return metafiles;
        }

        public static System.Xml.Linq.XDocument GetRDLXML(ReportService2005.ReportingService2005 rep_svc, Microsoft.Reporting.WinForms.ServerReport report)
        {
            var xml_bytes = rep_svc.GetReportDefinition(report.ReportPath);
            var memstream = new System.IO.MemoryStream(xml_bytes);
            var xmlreader = new System.Xml.XmlTextReader(memstream);
            var xdoc = System.Xml.Linq.XDocument.Load(xmlreader);
            return xdoc;
        }


        public static string GetNamespace(System.Xml.Linq.XDocument doc)
        {
            return doc.Root.Name.Namespace.ToString();
        }

        public static string RDL2005_namespace = "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition";
        public static string RDL2005_namespace_x = "{http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition}";

    }
}