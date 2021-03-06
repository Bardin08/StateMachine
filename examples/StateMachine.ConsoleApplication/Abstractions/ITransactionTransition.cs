using StateMachine.ConsoleApplication.Models;

namespace StateMachine.ConsoleApplication.Abstractions
{
    public interface ITransactionTransition<TInputState, TOutputState> 
        : StateMachine.Abstractions.ITransition<RegistrationStateMachine, ITransactionState, TInputState, RegistrationTransactionModel, TOutputState>
        where TInputState : ITransactionState
        where TOutputState : ITransactionState
    {
    }
}
