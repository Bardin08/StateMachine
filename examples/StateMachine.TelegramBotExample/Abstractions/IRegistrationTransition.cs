namespace StateMachine.TelegramBotExample.Abstractions
{
    public interface IRegistrationTransition<TInputState, TOutputState>
        : StateMachine.Abstractions.ITransition<RegistrationStateMachine,
                                                IRegistrationState,
                                                TInputState,
                                                Models.RegistrationTransactionModel,
                                                TOutputState>
        where TInputState : IRegistrationState
        where TOutputState : IRegistrationState
    {
    }
}
