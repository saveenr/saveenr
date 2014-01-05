namespace Isotope.Automata
{
    public sealed class FSMTransition<STATE, INPUT, CONTEXT>
    {
        public STATE FromState { get; set; }
        public STATE ToState { get; set; }
        public System.Func<INPUT, CONTEXT, bool> Condition { get; set; }
        public System.Action<INPUT, CONTEXT> Action { get; set; }
    }
}