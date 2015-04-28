using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HAP=HtmlAgilityPack;

namespace WordHTMLCleaner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input_file = this.textBoxInput.Text;
            string output_file = this.textBoxOutput.Text;

            var doc = new HAP.HtmlDocument();
            doc.Load( input_file );

            if (System.IO.File.Exists(output_file))
            {
                System.IO.File.Delete(output_file);
            }

            var root = doc.DocumentNode;
            var html = root.Element("html");

            var head = html.Element("head");
            if (head!=null)
            {
                head.Remove();
            }


            var targets = html.DescendantElements("o:p").ToList();
            foreach (var el in targets)
            {
                el.ReplaceWithInnerText();
            }

            targets = html.DescendantElements().Where(n=>n.HasAttributes && n.Attributes.Contains("style")).ToList();
            foreach (var el in targets)
            {
                el.Attributes.Remove("style");
            }

            targets = html.Descendants().Where(n => n.NodeType== HAP.HtmlNodeType.Comment).ToList();
            foreach (var el in targets)
            {
                el.Remove();
            }

            targets = html.DescendantElements("v:shape").ToList();
            foreach (var el in targets)
            {
                el.ReplaceWithText("MISSING IMAGE");
            }

            html.CollapseSpans();

            foreach (var el in html.Descendants())
            {
                if (el.NodeType == HAP.HtmlNodeType.Text)
                {
                    var tn = (HAP.HtmlTextNode) el;
                    string t = tn.Text;
                    t= t.Replace("·", "*");
                    t = t.Replace("●", "*");
                    t = t.Replace("&#9679;", "*");
                    tn.Text = t;
                }
            }

            //html.CollapsePre();
            doc.Save( output_file );
        }
    }

    public static class X
    {
        public static IEnumerable<HAP.HtmlNode> DescendantElements(this HAP.HtmlNode parent)
        {
            var elements = parent.Descendants().Where(n => n.NodeType == HAP.HtmlNodeType.Element);
            return elements;
        }

        public static IEnumerable<HAP.HtmlNode> DescendantElements(this HAP.HtmlNode parent, string name)
        {
            var elements = parent.DescendantElements().Where(n=>n.Name==name);
            return elements;
        }

        public static HAP.HtmlNode ReplaceWithInnerText(this HAP.HtmlNode el)
        {
            var newNode = HAP.HtmlNode.CreateNode(el.InnerText);
            el.ParentNode.ReplaceChild(newNode, el);
            return newNode;
        }

        public static HAP.HtmlNode ReplaceWithText(this HAP.HtmlNode el, string s)
        {
            var newNode = HAP.HtmlNode.CreateNode(s);
            el.ParentNode.ReplaceChild(newNode, el);
            return newNode;
        }

        public static void CollapseSpans(this HAP.HtmlNode el)
        {
            var children = el.ChildNodes.ToList();
            foreach (var child in children)
            {
                if (child.Name == "span")
                {
                    child.ReplaceWithInnerText();                        
                }
                else
                {


                    child.CollapseSpans();
                }
            }
        }

        public static bool containsonlytext(this HAP.HtmlNode el)
        {

            if (!el.HasChildNodes)
            {
                return true;
            }

            foreach (var child in el.ChildNodes)
            {
                if (child.NodeType != HAP.HtmlNodeType.Text)
                {
                    return false;
                }
            }
            return true;
        }

        public static void CollapsePre(this HAP.HtmlNode el)
        {

            var children = el.ChildNodes.ToList();

            var targets = new List<HtmlAgilityPack.HtmlNode>();

            var sb = new StringBuilder();
            foreach (var child in children)
            {
                if (child.Name == "p" && child.HasAttributes && child.Attributes["class"].Value == "Preformatted")
                {
                    bool onlytext = child.containsonlytext();
                    child.SetAttributeValue("PRE", "true");
                    child.SetAttributeValue("onlytext", onlytext.ToString());

                    sb.AppendLine(child.InnerText);
                    targets.Add(child);
                }
                else if (child.NodeType == HAP.HtmlNodeType.Text)
                {
                    //skip;
                }
                else
                {
                    if (targets.Count > 0)
                    {
                        var newpre = HAP.HtmlNode.CreateNode("<PRE>" + sb.ToString() + "</PRE>");
                        child.ParentNode.InsertBefore(newpre, targets[0]);

                        foreach (var t in targets)
                        {
                            t.Remove();
                        }

                        sb.Clear();
                        targets.Clear();
                    }
                }

                if (targets.Count > 0)
                {
                    var newpre = HAP.HtmlNode.CreateNode("<PRE>" + sb.ToString() + "</PRE>");
                    child.ParentNode.InsertBefore(newpre, targets[0]);

                    foreach (var t in targets)
                    {
                        t.Remove();
                    }

                    sb.Clear();

                    targets.Clear();
                    
                }
                child.CollapsePre();

            }
        }

    }
}
