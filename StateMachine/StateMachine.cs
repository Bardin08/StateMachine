using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using StateMachine.Abstractions;
using StateMachine.Extensions;

namespace StateMachine
{
    public class StateMachine<TState, TInput> : IStateMachine<TState, TInput>
        where TState : IState
    {
        private readonly Dictionary<HashSet<Assembly>, Dictionary<Type, TransitionEntry<IStateMachine<TState, TInput>, TState, TInput>[]>> knownAssemblies 
            = new(new HashSetEqualityComparer<Assembly>());
        private readonly Dictionary<Type, TransitionEntry<IStateMachine<TState, TInput>, TState, TInput>[]> _transitions;
        private TState _currentState;

        public TState CurrentState
        {
            get => _currentState;
            set => _currentState = value ?? throw new ArgumentNullException(nameof(value), "Current State can't be null!");
        }

        public void ForceState(TState state)
        {
            CurrentState = state;
        }

        public virtual bool TryTransition(TInput input)
        {
            var stateType = CurrentState.GetType();
            do
            {
                if (!_transitions.ContainsKey(stateType))
                    continue;

                foreach (var transition in _transitions[stateType])
                {
                    if (!transition.CanTransition(this, CurrentState, input))
                        continue;

                    CurrentState = transition.DoTransition(this, CurrentState, input);
                    return true;
                }
            }
            while ((stateType = stateType.GetBaseType<TState>()) != null);

            return false;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="StateMachine{TState, TInput}"/> class,
        /// looking for matching <see cref="Transition{TMachine, TState, TInputState, TInput, TOutputState}"/>s in the
        /// <see cref="Assembly"/> where the deriving class is defined.
        /// </summary>
        /// <param name="startState">The state that the state machine starts in.</param>
        protected StateMachine(TState startState)
        {
            CurrentState = startState;

            _transitions = BuildStateMachine(this.GetType().Assembly);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="StateMachine{TStates, TWith}"/> class, looking for matching
        /// <see cref="Transition{TMachine, TStates, TStateIn, TWith, TStateOut}"/>s in the given <see cref="Assembly"/>s.
        /// </summary>
        /// <param name="startState">The state that the state machine starts in.</param>
        /// <param name="assemblies">The <see cref="Assembly"/>s to look for matching Transitions in.</param>
        protected StateMachine(TState startState, params Assembly[] assemblies)
        {
            CurrentState = startState;

            _transitions = BuildStateMachine(assemblies);
        }

        private Dictionary<Type, TransitionEntry<IStateMachine<TState, TInput>, TState, TInput>[]> BuildStateMachine(params Assembly[] assemblies)
        {
            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));

            if (assemblies.Length == 0)
                throw new ArgumentException("There must be at least one assembly to look for Transitions in!");

            var assemblySet = new HashSet<Assembly>(assemblies);

            if (!knownAssemblies.ContainsKey(assemblySet))
            {
                var transitionsTypes = CollectMatchingTransitions(assemblies);

                knownAssemblies.Add(assemblySet, transitionsTypes.GroupBy(entry => entry.InputStateType).ToDictionary(g => g.Key, g => g.ToArray()));
            }

            return knownAssemblies[assemblySet];
        }

        /// <summary>
        /// Finds all <see cref="Transition{TMachine, TStates, TStateIn, TWith, TStateOut}"/> derivatives in the assemblies that match this state machine.
        /// </summary>
        private IEnumerable<TransitionEntry<IStateMachine<TState, TInput>, TState, TInput>> CollectMatchingTransitions(Assembly[] assemblies)
        {
            return assemblies.SelectMany(assembly =>
                assembly.GetTypes().Select(type =>
                {
                    var rType = type.GetInterface("ITransition`5");
                    var isTransition = rType != null && type.IsClass;

                    return new { IsTransition = isTransition, ConcreteType = type, TransitionType = rType };
                }))
                .Where(x => x.IsTransition == true)
                .Select(r => new TransitionEntry<IStateMachine<TState, TInput>, TState, TInput>(r.ConcreteType, r.TransitionType));
        }

        /// <summary>
        /// Compares <see cref="HashSet{T}"/>s by the elements they contain.
        /// </summary>
        private sealed class HashSetEqualityComparer<T> : IEqualityComparer<HashSet<T>>
        {
            public bool Equals(HashSet<T> x, HashSet<T> y)
            {
                return x.SetEquals(y);
            }

            public int GetHashCode(HashSet<T> obj)
            {
                return obj.Select(item => item.GetHashCode())
                    .Aggregate((acc, hash) => unchecked(acc + hash));
            }
        }
    }
}
