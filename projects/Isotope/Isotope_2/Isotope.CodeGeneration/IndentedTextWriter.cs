namespace Isotope.CodeGeneration
{
    public class IndentedTextWriter
    {
        private System.IO.TextWriter w;
        private int IndentLevel;
        private string indent_text = "    ";

        public IndentedTextWriter(System.IO.TextWriter tw) :
            base()
        {
            this.w = tw;
            this.IndentLevel = 0;
        }

        public void WriteLine(string s)
        {
            this.WriteIndent();
            this.w.WriteLine(s);
        }
        public void WriteLine(string fmt, params object[] items)
        {
            var s = string.Format(fmt, items);
            this.WriteLine(s);
        }

        public void Write(string s)
        {
            this.WriteIndent();
            this.w.Write(s);
        }

        public void WriteIndent()
        {
            for (int i = 0; i < this.IndentLevel; i++)
            {
                this.w.Write(indent_text);
            }
        }

        public void Indent()
        {
            this.IndentLevel++;
        }

        public void Dedent()
        {
            this.IndentLevel--;
        }
    }
}