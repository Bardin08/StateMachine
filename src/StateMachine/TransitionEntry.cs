 using System;
using System.Linq.Expressions;

using StateMachine.Abstractions;

namespace StateMachine
{
    public class TransitionEntry<TStateMachine, TState, TInput> 
        where TStateMachine : IStateMachine<TState, TInput>
        where TState : IState
    {
        private Func<object, TStateMachine, TState, TInput, bool> _canTransition;
        private Func<object, TStateMachine, TState, TInput, TState> _doTransition;
        private readonly object _transitionInstance;

        public Type Transition { get; }
        
        public Type InputStateType { get;}
        public Type OutputStateType { get; }

        public TransitionEntry(Type transition, Type transitionDefinition)
        {
            Transition = transition;
            _transitionInstance = Activator.CreateInstance(transition);

            var genericArguments = transitionDefinition.GetGenericArguments();

            InputStateType = genericArguments[2];
            OutputStateType = genericArguments[4];

            CreateDelegates(genericArguments);
        }

        public bool CanTransition(TStateMachine stateMachine, TState state, TInput input)
        {
            return _canTransition(_transitionInstance, stateMachine, state, input);
        }

        public TState DoTransition(TStateMachine stateMachine, TState state, TInput input)
        {
            return _doTransition(_transitionInstance, stateMachine, state, input);
        }

        private void CreateDelegates(Type[] genericParameters)
        {
            var transitionParameterNode = Expression.Parameter(typeof(object));
            var transitionCast = Expression.Convert(transitionParameterNode, Transition);

            var machineParameterNode = Expression.Parameter(typeof(TStateMachine));
            var machineCast = Expression.Convert(machineParameterNode, genericParameters[0]);

            var stateParameterNode = Expression.Parameter(typeof(TState));
            var stateCast = Expression.Convert(stateParameterNode, InputStateType);

            var inputParameterNode = Expression.Parameter(typeof(TInput));

            _canTransition = Expression.Lambda<Func<object, TStateMachine, TState, TInput, bool>>(
                Expression.Call(transitionCast, Transition.GetMethod("CanTransition"), machineCast, stateCast, inputParameterNode),
                transitionParameterNode, machineParameterNode, stateParameterNode, inputParameterNode).Compile();

            var doTransitionMethod = Transition.GetMethod("DoTransition");

            _doTransition = Expression.Lambda<Func<object, TStateMachine, TState, TInput, TState>>(
                Expression.Convert(
                    Expression.Call(transitionCast, doTransitionMethod, machineCast, stateCast, inputParameterNode), typeof(TState))
                , transitionParameterNode, machineParameterNode, stateParameterNode, inputParameterNode).Compile();
        }
    }
}