namespace StateMachine.Abstractions
{
    /// <summary>
    /// An abstraction that represent a state machine.
    /// </summary>
    /// <typeparam name="TState"> The base type of states used by this machine. </typeparam>
    /// <typeparam name="TInput"> The base type of the transition inputs used by this machine. </typeparam>
    public interface IStateMachine<TState, TInput>
    {
        /// <summary>
        /// Gets the current state of the <see cref="StateMachine{TState, TInput}"/>.
        /// </summary>
        TState CurrentState { get; set; }

        /// <summary>
        /// Sets the current state of the machine to the given one.
        /// </summary>
        /// <param name="state"> The new state to use as the current state. </param>
        void ForceState(TState state);

        /// <summary>
        /// Tries transitioning from the current state with the given input.
        /// Tries all transitions for the current type of the state, up to the base state type of the machine. 
        /// </summary>
        /// <param name="input"> The input to try and transition with. </param>
        /// <returns> Whether a transition was successfully executed or not. </returns>
        bool TryTransition(TInput input);
    }
}
