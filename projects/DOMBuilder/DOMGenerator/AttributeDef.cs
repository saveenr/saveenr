namespace Isotope.DOM
{
    public class AttributeDef
    {
        public string Name;
        public System.Type DataType;

        public AttributeDef(string name, System.Type t)
        {
            this.Name = name;
            this.DataType = t;
        }
                
    }
}