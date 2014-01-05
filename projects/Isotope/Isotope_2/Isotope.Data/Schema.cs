using System.Collections.Generic;

namespace Isotope.Data
{
    public class Schema
    {
        private List<Field> _fields;

        public Schema()
        {
            this._fields = new List<Field>();
        }

        public class Field
        {
            public readonly string Name;
            public readonly System.Type Type;
            public object Data;

            public Field(string name, System.Type type, object data)
            {
                this.Name = name;
                this.Type = type;
                this.Data = data;
            }
        }

        public List<Field> Fields
        {
            get { return _fields; }
        }

        public Field AddField(string name, System.Type type, object data)
        {
            var field = new Field(name, type, data);
            this._fields.Add(field);
            return field;
        }
    }
}