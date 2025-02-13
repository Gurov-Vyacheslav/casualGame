using System.Collections.Generic;

using LearnGame.Exceptions;
using UnityEngine;

namespace LearnGame.FSM
{
    public class BaseStateMachine
    {
        private BaseState _currentSate;
        private List<BaseState> _states;

        private Dictionary<BaseState, List<Transition>> _transitions;

        public BaseStateMachine()
        {
            _states = new List<BaseState>();
            _transitions = new Dictionary<BaseState, List<Transition>>();
        }
        public void SetInitialState(BaseState state)
        {
            _currentSate = state;
        }

        public void AddState(BaseState state, List<Transition> transotions) 
        {
            if (!_states.Contains(state)) 
            { 
                _states.Add(state);
                _transitions.Add(state, transotions);
            }
            else
            {
                throw new AlreadyExistsException($"State {state.GetType()} already exists in state machine!");
            }
        }

        public void Update()
        {
            foreach (var transition in _transitions[_currentSate])
            {
                if (transition.Condition())
                {
                    _currentSate = transition.ToState;
                    break;
                }
            }
            _currentSate.Execute();
        }
    }
}
