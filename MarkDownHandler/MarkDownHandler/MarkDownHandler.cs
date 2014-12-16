using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkDownHandler
{
    public class MarkDownHandler : System.Web.IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(System.Web.HttpContext context)
        {
            const bool wrap_in_html = true;
            const string css_url = "/default.css";

            const string html_content_type = "text/html";



            string input_file = context.Request.PhysicalPath;
            string input_text_markdown = System.IO.File.ReadAllText(input_file);


            var settings = new CommonMark.CommonMarkSettings();
            string output_text_html = CommonMark.CommonMarkConverter.Convert(input_text_markdown, settings);

            string title = System.IO.Path.GetFileNameWithoutExtension(input_file);
            

            context.Response.ContentType = html_content_type;

            if (wrap_in_html)
            {
                context.Response.Write("<html>\n");
                context.Response.Write("<head>\n");
                context.Response.Write(string.Format("<title>{0}</title>\n",title));

                if (css_url!=null)
                {
                    context.Response.Write(
                        string.Format(
                            "<link rel = \"stylesheet\" type = \"text/css\" href = \"{0}\"/ >", css_url));
                }
                context.Response.Write("</head>\n");
                context.Response.Write("<body>\n");
            }

            context.Response.Write(output_text_html);

            if (wrap_in_html)
            {
                context.Response.Write("</body>\n");
                context.Response.Write("</html>\n");
            }
        }

        public void Dispose()
        {
        }

        public void Init(System.Web.HttpApplication context)
        {
            context.PreRequestHandlerExecute += this.OnPreRquestHandlerExecute;
        }

        public void OnPreRquestHandlerExecute(object source, EventArgs e)
        {
        }
    }
}