namespace Isotope.CommandLine
{
    [System.Serializable]
    public class CommandLineException : System.Exception
    {
        public CommandLineException(string s)
            :
                base(s)
        {
        }
    }

    [System.Serializable]
    public class GrammarException : CommandLineException
    {
        public GrammarException(string s)
            :
                base(s)
        {
        }
    }

    [System.Serializable]
    public class ParseException : CommandLineException
    {
        private readonly CommandLineParser m_parser;

        public ParseException(CommandLineParser parser, string s)
            :
                base(s)
        {
            this.m_parser = parser;
        }

        public CommandLineParser Parser
        {
            get { return this.m_parser; }
        }
    }

    [System.Serializable]
    public class UnknownParameterException : ParseException
    {
        public UnknownParameterException(CommandLineParser parser, string s)
            :
                base(parser, s)
        {
        }
    }

    [System.Serializable]
    public class MissingRequiredParameterException : ParseException
    {
        public MissingRequiredParameterException(CommandLineParser parser, string s)
            :
                base(parser, s)
        {
        }
    }

    [System.Serializable]
    public class UnknownKeywordErrorException : ParseException
    {
        public UnknownKeywordErrorException(CommandLineParser parser, string s)
            :
                base(parser,s)
        {
        }
    }

    [System.Serializable]
    public class InvalidParameterValueException : CommandLineException
    {
        public InvalidParameterValueException(string s)
            :
                base(s)
        {
        }
    }

    [System.Serializable]
    public class RuntimeErrorException : CommandLineException
    {
        public RuntimeErrorException(string s)
            :
                base(s)
        {
        }
    }


}