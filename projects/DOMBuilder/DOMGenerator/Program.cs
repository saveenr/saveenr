using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Isotope.DOM;
using Microsoft.CSharp; //We are building a CSharp class
using System.CodeDom.Compiler;

namespace DemoDOMBuilder
{
    class Program
    {
        static void Main(string[] args)
        {

            var domdef = new Isotope.DOM.DOMDef();


            var DefRect = new Isotope.DOM.ElementDef("Rectangle");
            var DefGroup = new Isotope.DOM.ElementDef("Group");
            var DefPage = new Isotope.DOM.ElementDef("Page");
            var DefDocument = new Isotope.DOM.ElementDef("Document");

            DefGroup.PossibleChildren.Add(DefRect);
            DefGroup.PossibleChildren.Add(DefGroup);

            DefRect.Attributes.Add(new AttributeDef("PinPosition", typeof(System.Drawing.PointF)));
            DefRect.Attributes.Add(new AttributeDef("Size", typeof(System.Drawing.SizeF)));

            DefPage.Attributes.Add(new AttributeDef("Size", typeof(System.Drawing.SizeF)));
            DefPage.PossibleChildren.Add(DefRect);
            DefPage.PossibleChildren.Add(DefGroup);

            DefDocument.PossibleChildren.Add(DefPage);


            domdef.defs.Add(DefDocument);
            domdef.defs.Add(DefPage);
            domdef.defs.Add(DefRect);
            domdef.defs.Add(DefGroup);


            string outputdll = "D:\\FooDOM.dll";
            string[] ref_asembleis = new string[] { "system.dll", "system.drawing.dll" };
            domdef.CreateDLL(outputdll,ref_asembleis);
        }
    }
}