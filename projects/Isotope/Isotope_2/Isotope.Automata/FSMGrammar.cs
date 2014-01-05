using System.Collections.Generic;

namespace Isotope.Automata
{
    public class FSMGrammar<STATE, INPUT, CONTEXT>
    {
        private readonly STATE m_init_state;
        private readonly Dictionary<STATE, List<FSMTransition<STATE, INPUT, CONTEXT>>> map_state_to_transitions;

        public FSM<STATE, INPUT, CONTEXT> GetMachineInstance(CONTEXT context)
        {
            return new FSM<STATE, INPUT, CONTEXT>(this, this.StartState, context);
        }
        
        public STATE StartState
        {
            get { return this.m_init_state; }
        }
        
        public FSMGrammar(STATE initstate)
        {
            this.m_init_state = initstate;
            this.map_state_to_transitions = new Dictionary<STATE, List<FSMTransition<STATE, INPUT, CONTEXT>>>(); 
        }

        public void Add(STATE from_state, STATE to_state, System.Func<INPUT, CONTEXT, bool> condition, System.Action<INPUT, CONTEXT> action)
        {
            var transition = new FSMTransition<STATE, INPUT, CONTEXT>();
            transition.FromState = from_state;
            transition.ToState = to_state;
            transition.Condition = condition;
            transition.Action = action;

            List<FSMTransition<STATE, INPUT, CONTEXT>> transitions = null;
            if (!map_state_to_transitions.ContainsKey(from_state))
            {
                transitions = new List<FSMTransition<STATE, INPUT, CONTEXT>>();
                map_state_to_transitions[from_state] = transitions;
            }
            else
            {
                transitions = map_state_to_transitions[from_state];
            }

            transitions.Add(transition);
        }

        public void Add(STATE from_state, STATE to_state, System.Func<INPUT, CONTEXT, bool> f_handles_input)
        {
            this.Add(from_state, to_state, f_handles_input, null);
        }

        public IList<FSMTransition<STATE, INPUT, CONTEXT>> GetTransitions(STATE state)
        {
            var transitions = this.map_state_to_transitions[state];
            return transitions;
        }
    }
}