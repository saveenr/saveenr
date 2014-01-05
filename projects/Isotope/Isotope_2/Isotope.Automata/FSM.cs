namespace Isotope.Automata
{
    public class FSM<STATE, INPUT, CONTEXT>
    {
        private STATE m_state;
        private CONTEXT m_context;
        public event System.Action<INPUT, CONTEXT, STATE, STATE> OnTransition;
        public event System.Action<CONTEXT, STATE> OnStart;

        private FSMGrammar<STATE, INPUT, CONTEXT> m_grammar;


        public STATE State
        {
            get { return this.m_state; }
        }

        public FSMGrammar<STATE, INPUT, CONTEXT> Grammar
        {
            get { return this.m_grammar; }
        }

        public CONTEXT Context
        {
            get
            {
                return this.m_context;
            }
        }

        public FSM(FSMGrammar<STATE, INPUT, CONTEXT> g, STATE init, CONTEXT t)
        {
            this.init(g, init, t);
        }

        protected void init(FSMGrammar<STATE, INPUT, CONTEXT> g, STATE init, CONTEXT t)
        {
            this.m_grammar = g;
            this.m_state = init;
            this.m_context = t;

            if (this.OnStart != null)
            {
                this.OnStart(this.Context, this.m_state);
            }
        }

        public void HandleInput(INPUT input)
        {
            var actions = this.Grammar.GetTransitions(this.State);
            bool found = false;
            foreach (var a in actions)
            {
                bool f = a.Condition(input, this.Context);
                if (f)
                {
                    found = true;
                    
                    if (a.Action != null)
                    {
                        a.Action(input, this.Context);
                    }

                    var old_state = this.m_state;
                    var new_state = a.ToState;
                    this.m_state = new_state;

                    if (this.OnTransition != null)
                    {
                        this.OnTransition(input, this.Context, old_state, new_state);
                    }

                    break;
                }
            }

            if (!found)
            {
                string msg = string.Format("Unhandled input \"{0}\" in state \"{1}\"", input.ToString(), this.State.ToString());
                throw new AutomataRuntimeException(msg);
            }
        }
    }
}