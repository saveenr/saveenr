using System.Collections.Generic;

namespace Isotope.CodeGeneration
{
    public class CSharpCodeWriter : IndentedTextWriter
    {
        public CSharpCodeWriter(System.IO.TextWriter tw) :
            base(tw)
        {
        }

        public void StartBlock(string s)
        {
            if (s == null)
            {
                throw new System.ArgumentNullException("s");
            }

            this.WriteLine(s);
            this.WriteLine("{");
            this.Indent();
        }

        public void StartBlock(string fmt, params object[] items)
        {
            string s = string.Format(fmt, items);
            this.StartBlock(s);
        }

        public void EndBlock()
        {
            EndBlock("");
        }

        public void EndBlock(string s)
        {
            this.Dedent();
            this.WriteLine("}" + s);
        }

        public void StartNamespace(string s)
        {
            if (s == null)
            {
                throw new System.ArgumentNullException("s");
            }

            this.StartBlock("namespace " + s);
        }

        public void EndNamespace()
        {
            this.EndBlock();
        }

        public void StartClass(string s)
        {
            if (s == null)
            {
                throw new System.ArgumentNullException("s");
            }
            this.StartBlock("public class " + s);
        }

        public void StartClass(string s, string t)
        {
            if (s == null)
            {
                throw new System.ArgumentNullException("s");
            }

            if (t == null)
            {
                throw new System.ArgumentNullException("t");
            }
            this.StartBlock(string.Format("class {0} : {1} ", s, t));
        }

        public void EndClass()
        {
            this.EndBlock();
        }

        public void Newline()
        {
            this.WriteLine("");
        }

        public void Comment(string s)
        {
            if (s == null)
            {
                throw new System.ArgumentNullException("s");
            }

            this.WriteLine("// " + s);
        }

        public void Using(string s)
        {
            if (s == null)
            {
                throw new System.ArgumentNullException("s");
            }

            this.WriteLine("using {0};", s);
        }

        public void Using(string s, string t)
        {
            if (s == null)
            {
                throw new System.ArgumentNullException("s");
            }

            if (t == null)
            {
                throw new System.ArgumentNullException("t");
            }

            this.WriteLine("using {0} = {1};", s, t);
        }

        public void WriteLines(IList<string> strings, string sep)
        {
            if (strings == null)
            {
                throw new System.ArgumentNullException();
            }

            string actual_sep;
            int n = 0;
            foreach (var item in strings)
            {
                actual_sep = (n < (strings.Count - 1)) ? sep : string.Empty;
                this.WriteLine(item + actual_sep);
                n++;
            }
        }
    }
}