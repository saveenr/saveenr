namespace Isotope.Automata
{
    [global::System.Serializable]
    public class AutomataRuntimeException : System.Exception
    {
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        public AutomataRuntimeException() { }
        
        public AutomataRuntimeException(string message) : base(message) { }

        public AutomataRuntimeException(string message, System.Exception inner) : base(message, inner) { }
        
        protected AutomataRuntimeException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}