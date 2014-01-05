using System.Collections.Generic;
using System.Reflection;

namespace Isotope.Data
{
    public class TypeSchema<T>
    {
        private Schema schema;
        private FieldInfo[] fields;
        private PropertyInfo[] properties;
        private System.Type sourcetype;
        
        public TypeSchema()
        {
            sourcetype = typeof(T);

            if (sourcetype.IsPrimitive)
            {
                this.schema = get_schema_from_primitive_type(sourcetype);
                this.fields = null;
                this.properties = null;
                
            }
            else
            {
                this.schema = get_schema_from_non_primitive_type(sourcetype);
                this.fields = sourcetype.GetFields();
                this.properties = sourcetype.GetProperties();
            }
        }

        public System.Type Type
        {
            get
            {
                return this.sourcetype;
            }
        }

        public Schema Schema
        {
            get
            {
                return this.schema;
            }
        }

        public System.Type Sourcetype
        {
            get { return sourcetype; }
            set { sourcetype = value; }
        }

        private static Schema get_schema_from_non_primitive_type(System.Type type)
        {
            var fields = type.GetFields();
            var properties = type.GetProperties();
            var schema = new Schema();

            foreach (var field in fields)
            {
                var fieldtype = field.FieldType;
                if (is_nullable_type(fieldtype))
                {
                    fieldtype = fieldtype.GetGenericArguments()[0];
                }
                schema.AddField(field.Name, fieldtype, field);
            }

            foreach (var prop in properties)
            {
                var proptype = prop.PropertyType;
                if (is_nullable_type(proptype))
                {
                    proptype = proptype.GetGenericArguments()[0];
                }
                schema.AddField(prop.Name, proptype, prop);
            }
            return schema;
        }

        private static Schema get_schema_from_primitive_type(System.Type type)
        {
            var schema = new Schema();
            schema.AddField("Value", type, null);
            return schema;
        }

        public IEnumerable<T> EnumSourceItems(IEnumerable<T> items, object [] objarray)
        {
            if (objarray.Length != Schema.Fields.Count)
            {
                throw new System.ArgumentException("objarray");
            }

            if (this.sourcetype.IsPrimitive)
            {
                if (Schema.Fields.Count != 1)
                {
                    throw new System.ArgumentException();
                }
                foreach (var item in items)
                {
                    objarray[0] = item;
                    yield return item;
                }
            }
            else
            {
                foreach (var item in items)
                {
                    int col_index = 0;
                    foreach (var field in fields)
                    {
                        var value = field.GetValue(item);
                        objarray[col_index++] = value;
                    }

                    foreach (var prop in properties)
                    {
                        var value = prop.GetValue(item, null);
                        objarray[col_index++] = value;
                    }

                    yield return item;
                }                
            }
        }

        private static bool is_nullable_type(System.Type type)
        {
            // http://msdn.microsoft.com/en-us/library/ms366789.aspx
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>));
        }
    }
}