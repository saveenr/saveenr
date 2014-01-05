using System.Collections.Generic;
using System.Linq;

namespace Isotope.CommandLine
{

    public class CommandLineParser
    {
        private Dictionary<string, Parameter> dic_parameters;
        private readonly List<string> unassigned_tokens;

        /// <summary>
        /// Constuctor
        /// </summary>
        public CommandLineParser()
        {
            this.initialize_parameter_definitions();
            this.unassigned_tokens = new List<string>();
        }

        private IEnumerable<Parameter> pos_parameters
        {
            get { return this.Parameters.Where(p => p.ParameterType == ParameterType.Positional); }
        }


        private IEnumerable<Parameter> keyword_parameters
        {
            get { return this.Parameters.Where(p => p.ParameterType == ParameterType.Named); }
        }



        public IEnumerable<Parameter> Parameters
        {
            get { return this.dic_parameters.Select( kv => kv.Value ); }
        }


        protected void initialize_parameter_definitions()
        {
            this.dic_parameters = new Dictionary<string, Parameter>();
        }

        /// <summary>
        /// Gets a nice string that represents the syntax of the parser
        /// </summary>
        /// <returns>the syntax string</returns>
        public string GetSyntax()
        {

            var sb = new System.Text.StringBuilder();

            string sep = " ";
            var pos_param_text = sep.Join(this.pos_parameters.Select(a => get_pos_parameter_syntax(a)));
            var key_param_text = sep.Join(this.keyword_parameters.Select(a => get_keyword_parameter_syntax(a)));
            string syntax = sep.Join(new[] {pos_param_text, key_param_text});
            sb.AppendLine(syntax);
            sb.AppendLine();
            foreach (var param in this.Parameters)
            {
                if ((param.PossibleValues != null) && (param.PossibleValues.Length > 0))
                {
                    string ps = string.Join(", ", param.PossibleValues);
                    sb.AppendFormat(" {0} = one of [ {1} ]", param.Name, ps);
                }
            }
            return sb.ToString();
        }

        private static string get_keyword_parameter_syntax(Parameter p)
        {
            return string.Format("<-{0} <value> >",p.Name);
        }

        private static string get_pos_parameter_syntax(Parameter p)
        {
            return string.Format("<{0}>",p.Name);
        }

        /// <summary>
        /// Finds an param with the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>the param object or null if it cannot be found</returns>
        public Parameter FindParameter(string name)
        {
            Parameter p;
            bool found = dic_parameters.TryGetValue(name, out p);
            if (!found)
            {
                return null;
            }
            return p;
        }

        /// <summary>
        /// Add a positional parameter
        /// </summary>
        /// <param name="name">the name of the parameter</param>
        /// <returns>the arg object</returns>
        public Parameter AddPositionalParameter(string name)
        {
            if (name== null)
            {
                throw new System.ArgumentNullException("name");
            }

            check_valid_param_name(name);

            var param = new Parameter(name, ParameterType.Positional, ParameterRequirement.Required);
            Add(param);
            return param;
        }

        private void check_valid_param_name( string name)
        {
            if (name.Length < 1)
            {
                throw new System.ArgumentException("must be at least one character in length","name");
            }

            if (!char.IsLetter(name[0]))
            {
                throw new System.ArgumentException("must begin with a letter","name");
            }

        }

        /// <summary>
        /// Add a named parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="required">the name of the param</param>
        /// <returns>the parameter</returns>
        public Parameter AddNamedParameter(string name, ParameterRequirement required)
        {
            if (name == null)
            {
                throw new System.ArgumentNullException("name");
            }

            check_valid_param_name(name);

            var param = new Parameter(name, ParameterType.Named, required);
            Add(param);
            return param;
        }

        /// <summary>
        /// Adds an parameter
        /// </summary>
        /// <param name="parameter">the parameter to add</param>
        /// <returns>the parameter object</returns>
        public Parameter Add(Parameter parameter)
        {
            if (parameter == null)
            {
                throw new System.ArgumentNullException("parameter");
            }

            if (this.dic_parameters.ContainsKey(parameter.Name))
            {
                throw new Isotope.CommandLine.GrammarException("Parameter object already added");
            }

            if (FindParameter(parameter.Name) != null)
            {
                throw new Isotope.CommandLine.GrammarException("Duplicate Parameter Name");
            }

            if (parameter.ParameterType == ParameterType.Positional && parameter.IsRequired == false)
            {
                throw new Isotope.CommandLine.GrammarException("Positional parameters must be have required set to true");
            }

            if (parameter.ParameterType == ParameterType.Positional)
            {
                dic_parameters[parameter.Name] = parameter;
            }
            else if (parameter.ParameterType == ParameterType.Named)
            {
                dic_parameters[parameter.Name] = parameter;
            }
            else
            {
                throw new System.ArgumentException("Unsupported Parameter type","parameter");
            }

            return parameter;
        }

        /// <summary>
        /// For simple command-line apps 
        /// </summary>
        /// <param name="args">arguments to parse</param>
        public void Main(string[] args)
        {
            if (args == null)
            {
                throw new System.ArgumentNullException("args","must not be null");
            }

            try
            {
                this.Parse(args);
            }
            catch (Isotope.CommandLine.ParseException e)
            {
                string appname = System.IO.Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location);
                string msg = string.Format("Syntax: {0} {1}", appname, e.Parser.GetSyntax());
                System.Console.WriteLine(msg);

                System.Console.WriteLine("ERROR: " + e.Message);
                if (e.Parser == null)
                {
                    System.Console.WriteLine("Parser object in exception is null");
                }

                System.Environment.Exit(-1);
            }
        }


        public IList<string> GetUnassignedTokens()
        {
            if (this.unassigned_tokens==null)
            {
                return new List<string>(0);
            }
            return this.unassigned_tokens.ToList();
        }

        protected void reset_parse_state()
        {
            foreach (var a in Parameters)
            {
                a.Reset();
            }
            this.unassigned_tokens.Clear();
        }

        protected IEnumerable<Parameter> enum_params_missing_required_values()
        {
            var param = Parameters.Where(a => !a.HasValue && a.IsRequired).ToList();
            return param;
        }

        protected static bool token_is_keyword( string token )
        {
            if ( token ==null)
            {
                throw new System.ArgumentNullException("token");
            }
            // keywords are strings od >= length 2 that begin with '-' and are followed by some alphanumerid

            // "a" is a not a keyword
            // "-1" is a not a keyword
            // "-2.0" is a not a keyword
            // "-foo" is a keyword

            return (token.Length > 1 && token[0] == '-' && char.IsLetter(token[1]));
        }

        /// <summary>
        /// Given a set of tokens, parse it
        /// </summary>
        /// <param name="tokens">the tokens</param>
        public void Parse(string[] tokens)
        {

            if (tokens == null)
            {
                throw new System.ArgumentNullException("tokens");
            }

            reset_parse_state();
            var tokenQ = new Queue<string>(tokens);

            foreach (var param in this.pos_parameters)
            {
                if (tokenQ.Count < 1)
                {
                    string msg = string.Format("Missing value for {0}", param.Name);
                    throw new Isotope.CommandLine.MissingRequiredParameterException(this, msg);
                }
                set_param_text(param, tokenQ.Dequeue()); 
            }

            while (tokenQ.Count > 0)
            {
                string token = tokenQ.Dequeue();

                if ( token_is_keyword(token))
                {
                    // this could be a keyword
                    var kw_name = token.Substring(1);
                    var formal_param = this.Get(kw_name);

                    if (formal_param == null)
                    {
                        string msg = string.Format("Parameter {0} not defined", token);
                        throw new Isotope.CommandLine.UnknownParameterException(this, msg);
                    }

                    if (formal_param.HasValue)
                    {
                        // in this case the parameter already has a value
                        string msg = string.Format("Parameter {0} already has a value", formal_param.Name);
                        throw new Isotope.CommandLine.ParseException(this, msg);
                    }

                    if (formal_param.ParameterType == ParameterType.Named)
                    {
                        if (tokenQ.Count > 0)
                        {
                            // if there is a token available use it for this keyword parameter
                            set_param_text( formal_param, tokenQ.Dequeue());
                        }
                        else
                        {
                            // there is no token following this keyword parameter
                            // just keep it null
                            if ( formal_param.Text ==null)
                            {
                                throw new ParseException(this,"Internal Error");
                            }
                        }
                    }
                    else
                    {
                        // in this case someone gave the name of a positional parameter. What should we do?
                        string msg = string.Format("Internal Error");
                        throw new Isotope.CommandLine.ParseException(this, msg);
                    }
                }
                else
                {
                        // the a bucket of tokens that the grammar did not account for
                        this.unassigned_tokens.Add(token);
                }
            }


            if (tokenQ.Count != 0)
            {
                throw new Isotope.CommandLine.ParseException(this, "Internal Error: TokenQ should be empty - if it is not not all tokens were correctly processed");
            }

            var missing_params = this.enum_params_missing_required_values().ToList();
            if (missing_params.Count > 0)
            {
                var sb = new System.Text.StringBuilder();
                sb.Append("Missing values for: ");
                var names_of_missing_params = missing_params.Select(a => a.Name).ToArray();
                sb.Append( string.Join( " ", names_of_missing_params));
                throw new Isotope.CommandLine.MissingRequiredParameterException(this, sb.ToString());
            }

            post_parsing();

        }




        protected void set_param_text(Parameter parameter, string text)
        {
            if ( parameter ==null)
            {
                throw new System.ArgumentNullException("parameter");
            }

            if (text == null)
            {
                throw new ParseException(this, "Internal Error");
            }

            if ((parameter.PossibleValues != null) && (parameter.PossibleValues.Length > 0))
            {
                // Possible values are defined; now verify that the input corresponds to the possible values

                if (parameter.PossibleValues.Contains(text, parameter.PossibleValueComparer))
                {
                    parameter.Text = text;
                }
                else
                {
                    string msg = string.Format("{0} can only be one of these values: {1}", parameter.Name,
                                               string.Join(", ", parameter.PossibleValues));
                    throw new InvalidParameterValueException(msg);
                }
            }
            else
            {
                parameter.Text = text;
                
            }

        }

        protected void post_parsing()
        {
            // for each param that recieved a value do the callback
            var params_to_callbacks = this.Parameters.Where(a => a.HasValue && a.OnPostParse != null);
            foreach (var param in params_to_callbacks)
            {
                param.OnPostParse(this, param);
            }
            
        }


        /// <summary>
        /// Retrieves the param with the given name
        /// </summary>
        /// <param name="name">The name of the parameter</param>
        /// <returns></returns>
        public Parameter Get(string name)
        {
            if (name== null)
            {
                throw new System.ArgumentNullException("name");
            }

            check_valid_param_name(name);


            if (!ContainsParameter(name))
            {
                throw new Isotope.CommandLine.UnknownKeywordErrorException(this, name);
            }
            return this.dic_parameters[name];
        }

        /// <summary>
        /// Checks if an param with a given name is in the parser
        /// </summary>
        /// <param name="name">the name of the parameter</param>
        /// <returns></returns>
        public bool ContainsParameter(string name)
        {
            if (name == null)
            {
                throw new System.ArgumentNullException("name");
            }

            check_valid_param_name(name);

            return dic_parameters.ContainsKey(name);
        }
    }
}