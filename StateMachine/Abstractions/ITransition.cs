namespace StateMachine.Abstractions
{
    /// <summary>
    /// The marker interface for all machine states. It helps to identify that a current class is a state.
    /// </summary>
    /// <typeparam name="TMachine">The type of the <see cref="StateMachine{TState, TInput}"/> that executes the transition.</typeparam>
    /// <typeparam name="TState">The base type of the states used by the machine.</typeparam>
    /// <typeparam name="TInputState">The base type of the state that the machine must be in to use this transition.</typeparam>
    /// <typeparam name="TInput">The base type of the inputs of the machine.</typeparam>
    /// <typeparam name="TOutputState">The base type of the state that this transition leads to.</typeparam>

    public interface ITransition<TMachine, TState, TInputState, TInput, TOutputState>
            where TMachine : IStateMachine<TState, TInput>
            where TState : IState
            where TInputState : TState
            where TOutputState : TState
    {
        /// <summary>
        /// Determines whether this transition can be taken with the current state and input.
        /// </summary>
        /// <param name="machine">The machine attempting the transition.</param>
        /// <param name="state">The state that the machine is in.</param>
        /// <param name="input">The input that the transition is being attempted with.</param>
        /// <returns>Whether this transition can be taken with the current state and input.</returns>
        public abstract bool CanTransition(TMachine machine, TInputState state, TInput input);

            /// <summary>
            /// Executes this transition with the current state and input.
            /// </summary>
            /// <param name="machine">The machine attempting the transition.</param>
            /// <param name="state">The state that the machine is in.</param>
            /// <param name="input">The input that the transition is being attempted with.</param>
            /// <returns>The new state of the machine.</returns>
            public abstract TOutputState DoTransition(TMachine machine, TInputState state, TInput input);
    }
}
