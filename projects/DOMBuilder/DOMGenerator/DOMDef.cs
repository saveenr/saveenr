using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CSharp;

namespace Isotope.DOM
{
    public class DOMDef
    {
        public List<Isotope.DOM.ElementDef> defs;
        public string Namespace="CustomDOM";

        public DOMDef()
        {
            this.defs = new List<Isotope.DOM.ElementDef>();
            
        }

        public void Gen(System.IO.StringWriter sw)
        {

            sw.WriteLine(
                @"
using System.Collections.Generic;
using System.Drawing;

");
            sw.WriteLine("namespace {0}", this.Namespace);
            sw.WriteLine("{");
            sw.WriteLine(@"

    public partial class Node
    {
        public readonly string _name;
        public string Name { get { return this._name; } }

        private Node parent;
        public Node Parent { get { return this.parent;} }

        private List<Node> children;
        public virtual IEnumerable<Node> Nodes()
        { 
            if ( this.children==null)
            {
                yield break;
            }
            else
            {
                foreach (Node n in this.children) { yield return n;}
            }
        }

        public virtual IEnumerable<T> Nodes<T>() where T : Node

        { 
            foreach (Node n in this.Nodes())
            {
                if (n is T) { yield return ((T) n); }
            }
        }

        public int NodeCount
        {
            get
            {
                    if ( this.children==null)
                    {
                        return 0;
                    }
                    else
                    {
                        return this.children.Count;
                    }
            }
        }

        protected Node(string name) 
        {
            this._name = name;
        }


        protected void AddNode( Node n)
        {
            if (n==null)
            {
                throw new System.ArgumentNullException(""n"");
            }

            if (n==this)
            {
                throw new System.ArgumentException(""n"");
            }

            if (n.Parent!=null)
            {
                throw new System.ArgumentException(""n"");
            }

            
            if ( this.children==null)
            { 
                this.children = new List<Node>();
            }

            n.parent= this;
            this.children.Add(n);
        }


        public IEnumerable<Node> Walk()
        {
            var q = new List<Node>();
            q.Add(this);

            while (q.Count > 0)
            {

                var cn = q[0];
                q.RemoveAt(0);
                yield return cn;

                foreach (var child in cn.Nodes())
                {
                    q.Add(child);
                }
            }
        }



        public IEnumerable<T> Walk<T>() where T : Node
        {
            foreach (var n in this.Walk())
            {
                if ( n is T)
                {
                    yield return (T) n;
                }
            }
        }

        

    }
");

            _gen(sw);

            sw.WriteLine("}");


        }

        public void _gen(System.IO.StringWriter sw)
        {
            foreach (var def in defs )
            {
                sw.WriteLine("");
                sw.WriteLine("\tpublic partial class {0} : Node", def.Name);
                sw.WriteLine("\t{");

                sw.WriteLine("\t\tpublic {0}()", def.Name);
                sw.WriteLine("\t\t\t: base(\"{0}\")", def.Name);
                sw.WriteLine("\t\t{");
                sw.WriteLine("\t\t}");

                foreach (var attr in def.Attributes)
                {
                    sw.WriteLine("\t\tpublic {0} {1};", attr.DataType.FullName , attr.Name);
                }

                foreach (var pc in def.PossibleChildren)
                {
                    sw.WriteLine("\t\tpublic void Add( {0} element )", pc.Name );
                    sw.WriteLine("\t\t{");
                    sw.WriteLine("\t\t\tthis.AddNode(element);");
                    sw.WriteLine("\t\t}");
                    
                }

                sw.WriteLine("\t}");
                

            }

        }

        public void CreateDLL(string outputfilename, string [] re)
        {
            var sb = new System.Text.StringBuilder();
            var sw = new System.IO.StringWriter(sb);
            Gen(sw);

            string source = sb.ToString();

            var po = new Dictionary<string, string>
                         {
                             {"CompilerVersion","v3.5"}
                         };

            var prov = new CSharpCodeProvider(po);

            var compile_params = new CompilerParameters();
            compile_params.OutputAssembly = outputfilename;
            foreach (var ra in re)
            {
                compile_params.ReferencedAssemblies.Add(ra);
            }

            // Run the compiler and build the assembly
            var results = prov.CompileAssemblyFromSource(compile_params, source);
            Console.WriteLine("> {0}" , results.Errors.Count);
            foreach (CompilerError err in results.Errors)
            {
                Console.WriteLine("-------------------");

                Console.WriteLine("FILE: {0}", err.FileName);
                Console.WriteLine("LINE: {0}", err.Line);
                Console.WriteLine("COL: {0}", err.Column);
                Console.WriteLine(err.ErrorText);
                
            }
        }
    }
}