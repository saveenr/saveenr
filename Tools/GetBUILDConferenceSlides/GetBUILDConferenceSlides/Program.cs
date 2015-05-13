using System;
using System.Linq;
using HAP=HtmlAgilityPack;

namespace GetBUILDConferenceSlides
{
    class Program
    {
        static void Main(string[] args)
        {
            string rss_feed = "http://channel9.msdn.com/Events/Build/2015/RSS";
            string outputdir = @"d:\demo";

            var reader = new SimpleFeedReader.FeedReader();
            var items = reader.RetrieveFeed(rss_feed);

            foreach (var i in items)
            {
                Console.WriteLine(string.Format("{0}\t{1}\t{2}",
                        i.PublishDate.ToString("g"),
                        i.Title,
                        i.Uri
                ));

                var webclient = new System.Net.WebClient();

                var html = webclient.DownloadString(i.Uri.ToString());

                var doc = new HAP.HtmlDocument();
                doc.LoadHtml(html);

                var a_elements = doc.DocumentNode.Descendants().Where(n => n.Name == "a").Where(n=>n.HasAttributes && n.Attributes.Contains("href")).ToList();

                string safe_title = i.Title.SanitizeForFileSystem();

                foreach (var a in a_elements)
                {
                    var link = GetHREFLink(a);
                    if (string.IsNullOrEmpty(link))
                    {
                        continue;
                    }

                    // Exclude inter-document references
                    // Exclude queries in URLs
                    if (link.StartsWith("#") || link.Contains("?"))
                    {
                        continue;
                    }

                    // Deal with urls that are missing the server part
                    if (link.StartsWith("/"))
                    {
                        link = i.Uri.GetBase()  + link;
                    }

                    // validate that it is a good URL
                    var link_uri = TryParse(link);
                    if (link_uri == null)
                    {
                        Console.WriteLine("Could not parse {0}",link);
                        continue;
                    }

                    Console.WriteLine(link);
                    Console.WriteLine(safe_title);

                    // only handle PPT and PPTX files
                    if (link.EndsWithCaseInsensitive(".ppt") || link.EndsWithCaseInsensitive(".pptx"))
                    {



                        string folder_name = System.IO.Path.Combine(outputdir, safe_title);
                        if (!System.IO.Directory.Exists(folder_name))
                        {
                            System.IO.Directory.CreateDirectory(folder_name);
                        }

                        string filename = System.IO.Path.Combine(folder_name, System.IO.Path.GetFileName(link));
                        if (!System.IO.File.Exists(filename))
                        {
                            try
                            {
                                Console.WriteLine("Downloading {0}", link );
                                webclient.DownloadFile( link, filename);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Failed to download {0}", filename);
                            }
                        }
                    }
                }
               
            }

        }

        private static string GetHREFLink(HAP.HtmlNode a)
        {
            string link = a.Attributes["href"].Value;
            if (link != null)
            {
                link = link.Trim();
            }
            return link;
        }

        private static System.Uri TryParse(string link)
        {
            try
            {
                var ppt_uri = new System.Uri(link);
                return ppt_uri;
            }
            catch (System.UriFormatException e)
            {
                return null;
            }
        }
    }
}
