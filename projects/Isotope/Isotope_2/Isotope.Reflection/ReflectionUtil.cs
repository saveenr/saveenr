using System.Collections.Generic;
using System.Linq;

namespace Isotope.Reflection
{
    public static class ReflectionUtil
    {
        public class ExtensionMethodRecord
        {
            public readonly System.Type ExtendingType;
            public readonly System.Reflection.MethodInfo Method;
            public readonly System.Type ExtendedType;

            public ExtensionMethodRecord(System.Type extending_type, System.Type extended_type, System.Reflection.MethodInfo method)
            {
                this.ExtendingType = extending_type;
                this.ExtendedType = extended_type;
                this.Method = method;
            }
        }

        /// <summary>
        /// Finds all Extension methods defined in an assembly
        /// </summary>
        /// <param name="asm"></param>
        /// <returns></returns>
        public static IEnumerable<ExtensionMethodRecord> EnumExtensionMethods(System.Reflection.Assembly asm)
        {
            var exported_types = asm.GetExportedTypes();

            foreach (var extending_type in exported_types)
            {
                foreach (var emi in EnumExtensionMethods(extending_type))
                {
                    yield return emi;
                }
            }
        }

        /// <summary>
        ///  Finds all Extension methods defined by a type
        /// </summary>
        /// <param name="extending_type"></param>
        /// <returns></returns>
        public static IEnumerable<ExtensionMethodRecord> EnumExtensionMethods(System.Type extending_type)
        {
            var ext_methods = extending_type.GetMethods().Where(IsExtensionMethod).ToList();
            foreach (var ext_method in ext_methods)
            {
                var first_param = ext_method.GetParameters()[0];
                var extended_type = first_param.ParameterType;
                var rec = new ExtensionMethodRecord(extending_type, extended_type, ext_method);
                yield return rec;
            }
        }

        /// <summary>
        /// returns true if the method is an extension method
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsExtensionMethod(System.Reflection.MethodInfo method)
        {
            if ((!method.IsPublic) || (!method.IsStatic))
            {
                // By definition an extension method must be public and static
                return false;
            }

            System.Type ext_attr_type = typeof(System.Runtime.CompilerServices.ExtensionAttribute);
            var ext_attrs =
                (System.Runtime.CompilerServices.ExtensionAttribute[]) method.GetCustomAttributes(ext_attr_type, false);

            return (ext_attrs.Length > 0);
        }

        /// <summary>
        /// Given an instance of a type, enumerates the name,value pairs for each public property of that anonymous type
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> EnumProperties(object instance)
        {
            if (instance == null)
            {
                throw new System.ArgumentNullException("instance");
            }

            var t = instance.GetType();
            var props = t.GetProperties();
            foreach (var prop in props)
            {
                object v = prop.GetValue(instance, System.Reflection.BindingFlags.Public, null, null, null);
                yield return new KeyValuePair<string, object>(prop.Name, v);
            }
        }

        /// <summary>
        /// Given an instance of a type, returns a dictionary of name,value pairs for each public property
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IDictionary<string, object> GetPropertyDictionary(object instance)
        {
            if (instance == null)
            {
                throw new System.ArgumentNullException("instance");
            }

            var dic = new Dictionary<string, object>();

            foreach (var kv in EnumProperties(instance))
            {
                dic[kv.Key] = kv.Value;
            }

            return dic;
        }

        public static string GetCSharpTypeAlias(System.Type type)
        {
            if (type == typeof(int))
            {
                return "int";
            }
            else if (type == typeof(string))
            {
                return "string";
            }
            else if (type == typeof(double))
            {
                return "double";
            }
            else if (type == typeof(bool))
            {
                return "bool";
            }
            else if (type == typeof(short))
            {
                return "short";
            }
            else if (type == typeof(ushort))
            {
                return "ushort";
            }
            else if (type == typeof(decimal))
            {
                return "decimal";
            }
            else if (type == typeof(double))
            {
                return "double";
            }
            else if (type == typeof(float))
            {
                return "float";
            }
            else if (type == typeof(char))
            {
                return "char";
            }
            else if (type == typeof(uint))
            {
                return "uint";
            }
            else if (type == typeof(long))
            {
                return "long";
            }
            else if (type == typeof(ulong))
            {
                return "ulong";
            }
            else if (type == typeof(byte))
            {
                return "byte";
            }
            else if (type == typeof(sbyte))
            {
                return "sbyte";
            }
            else if (type == typeof(object))
            {
                return "object";
            }
            else
            {
                return null;
            }
        }

        public static string GetNiceTypeName(System.Type type)
        {
            return GetNiceTypeName(type, GetCSharpTypeAlias);
        }

        public static string GetNiceTypeName(System.Type type, System.Func<System.Type, string> overridefunc)
        {
            if (overridefunc != null)
            {
                string s = overridefunc(type);
                if (s != null)
                {
                    return s;
                }
            }

            if (IsNullableType(type))
            {
                var actualtype = type.GetGenericArguments()[0];
                return GetNiceTypeName(actualtype, overridefunc) + "?";
            }

            if (type.IsArray)
            {
                var at = type.GetElementType();
                return string.Format("{0}[]", GetNiceTypeName(at, overridefunc));
            }

            if (type.IsGenericType)
            {
                var sb = new System.Text.StringBuilder();
                var tokens = type.Name.Split(new[] { '`' });

                sb.Append(tokens[0]);
                var gas = type.GetGenericArguments();
                var ga_names = gas.Select(i => GetNiceTypeName(i, overridefunc));

                sb.Append("<");
                Join(sb, ", ", ga_names);
                sb.Append(">");
                return sb.ToString();
            }

            return type.Name;
        }

        public static bool IsNullableType(System.Type colType)
        {
            return ((colType.IsGenericType) &&
                    (colType.GetGenericTypeDefinition() == typeof(System.Nullable<>)));
        }

        private static void Join(System.Text.StringBuilder sb, string s, IEnumerable<string> tokens)
        {
            int n = tokens.Count();
            int c = tokens.Select(t => t.Length).Sum();
            c += (n > 1) ? s.Length * n : 0;
            c += sb.Length;
            sb.EnsureCapacity(c);

            int i = 0;
            foreach (string t in tokens)
            {
                if (i > 0)
                {
                    sb.Append(s);
                }
                sb.Append(t);
                i++;
            }
        }   
    }
}