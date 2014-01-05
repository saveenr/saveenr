using System;
using System.Collections.Generic;

namespace Isotope.DOM
{
    public class ElementDef
    {
        public string Name;

        public List<ElementDef> PossibleChildren;
        public List<AttributeDef> Attributes;
        
        public ElementDef(string name)
        {
            this.Attributes = new List<AttributeDef>();
            if (name==null)
            {
                throw new ArgumentNullException("name");
            }

            this.Name = name;
            this.PossibleChildren = new List<ElementDef>();
        }
    }
}