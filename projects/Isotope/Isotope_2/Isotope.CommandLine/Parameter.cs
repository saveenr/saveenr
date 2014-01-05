using System.Linq;

namespace Isotope.CommandLine
{
    public class Parameter
    {
        protected string m_name;
        private string m_string_value;
        private string[] m_possible_values;

        private ParameterRequirement m_required;
        private ParameterType _parameter_type;
        private System.Action<CommandLineParser, Parameter> _on_post_parse;

        public System.StringComparer PossibleValueComparer = System.StringComparer.InvariantCultureIgnoreCase;

        public string[] PossibleValues
        {
            get { return this.m_possible_values; }
            set
            {
                if ((value == null) || (value.Length < 1))
                {
                    this.m_possible_values = null;
                }
                else
                {
                    var values = value.Select(s => s.Trim()).ToArray();
                    if (values.Any(s => string.IsNullOrEmpty(s)))
                    {
                        throw new Isotope.CommandLine.GrammarException("contains a null or empty value");
                    }

                    this.m_possible_values = values;
                }
            }
        }

        public Parameter(string name, ParameterType argtype, ParameterRequirement required)
        {
            init(argtype, name, required);
        }

        private void init(ParameterType argtype, string name, ParameterRequirement required)
        {
            this._parameter_type = argtype;
            this.m_possible_values = null;

            if (name == null)
            {
                throw new System.ArgumentNullException("name", "cannot be null");
            }

            if (name.Length == 0)
            {
                throw new Isotope.CommandLine.GrammarException("Name for argument must be at least one char");
            }

            m_name = name;
            m_required = required;
            m_string_value = null;
        }

        /// <summary>
        /// Clears the value stored in this Arg
        /// </summary>
        public void Reset()
        {
            m_string_value = null;
        }

        /// <summary>
        /// The text stored in the Arg. If there is no text stored, and a caller tries to retrieve it, then an exception will be thrown
        /// </summary>
        public string Text
        {
            get
            {
                if (this.m_string_value == null)
                {
                    string msg = string.Format("No Text for argument \"{0}\"", this.Name);
                    throw new Isotope.CommandLine.RuntimeErrorException(msg);
                }
                return m_string_value;
            }
            set { m_string_value = value; }
        }

        /// <summary>
        /// Returns the content of the Text converted to an int
        /// </summary>
        /// <returns>the int value</returns>
        public int GetInt()
        {
            return int.Parse(m_string_value.Trim());
        }

        /// <summary>
        /// Returns the content of the Text converted to an double
        /// </summary>
        /// <returns>the double value</returns>
        public double GetDouble()
        {
            return double.Parse(m_string_value.Trim());
        }

        /// <summary>
        /// Returns the content of the Text converted to a an Enum type
        /// </summary>
        /// <returns>the enum value</returns>
        public T GetEnum<T>()
        {
            return Isotope.CommandLine.EnumUtil.Parse<T>(m_string_value.Trim(), true);
        }

        /// <summary>
        /// Returns the content of the Text converted to an double
        /// </summary>
        /// <returns>the double value</returns>
        public bool GetBool()
        {
            return bool.Parse(m_string_value.Trim());
        }

        /// <summary>
        /// Retrieve the text contents in the Arg.
        /// </summary>
        /// <param name="defval">if there is no text value stored in the Arg, this is returned instead</param>
        /// <returns></returns>
        public string GetText(string defval)
        {
            return this.m_string_value ?? defval;
        }

        /// <summary>
        /// Returns the content of the Text converted to a an Enum type
        /// </summary>
        /// <returns>the enum value</returns>
        public bool GetYesNo()
        {
            return CommandLineUtil.ParseYesNo(m_string_value.Trim());
        }

        /// <summary>
        /// Retrieve the text contents in the Arg as in int
        /// </summary>
        /// <param name="defval">if there is no text value stored in the Arg, this is returned instead</param>
        /// <returns></returns>
        public int GetInt(int defval)
        {
            string s = this.GetText(null);
            if (s == null)
            {
                return defval;
            }
            return this.GetInt();
        }

        /// <summary>
        /// Retrieve the text contents in the Arg as a double.
        /// </summary>
        /// <param name="defval">if there is no text value stored in the Arg, this is returned instead</param>
        /// <returns></returns>
        public double GetDouble(double defval)
        {
            string s = this.GetText(null);
            if (s == null)
            {
                return defval;
            }
            return this.GetDouble();
        }

        /// <summary>
        /// Retrieve the text contents in the Arg as an enum.
        /// </summary>
        /// <param name="defval">if there is no text value stored in the Arg, this is returned instead</param>
        /// <returns></returns>
        public T GetEnum<T>(T defval)
        {
            string s = this.GetText(null);
            if (s == null)
            {
                return defval;
            }
            return this.GetEnum<T>();
        }

        /// <summary>
        /// Retrieve the text contents in the Arg as in int
        /// </summary>
        /// <param name="defval">if there is no text value stored in the Arg, this is returned instead</param>
        /// <returns></returns>
        public bool GetBool(bool defval)
        {
            string s = this.GetText(null);
            if (s == null)
            {
                return defval;
            }
            return this.GetBool();
        }

        /// <summary>
        /// Retrieve the text contents in the Arg as in int
        /// </summary>
        /// <param name="defval">if there is no text value stored in the Arg, this is returned instead</param>
        /// <returns></returns>
        public bool GetYesNo(bool defval)
        {
            string s = this.GetText(null);
            if (s == null)
            {
                return defval;
            }
            return this.GetYesNo();
        }

        /// <summary>
        /// The name of the Arg. Read-only
        /// </summary>
        public string Name
        {
            get { return m_name; }
        }

        /// <summary>
        /// Whether the Arg is required or not
        /// </summary>
        public bool IsRequired
        {
            get { return this.m_required == ParameterRequirement.Required; }
        }

        /// <summary>
        /// Whether the Arg contains a valueor not
        /// </summary>
        public bool HasValue
        {
            get { return (m_string_value != null); }
        }

        public ParameterType ParameterType
        {
            get { return _parameter_type; }
            set { _parameter_type = value; }
        }

        public System.Action<CommandLineParser, Parameter> OnPostParse
        {
            get { return _on_post_parse; }
            set { _on_post_parse = value; }
        }
    }
}