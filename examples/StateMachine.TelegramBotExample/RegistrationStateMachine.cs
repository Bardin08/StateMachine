using StateMachine.TelegramBotExample.Abstractions;
using StateMachine.TelegramBotExample.Models;

namespace StateMachine.TelegramBotExample
{
    public class RegistrationStateMachine : StateMachine<IRegistrationState, RegistrationTransactionModel>
    {
        public RegistrationStateMachine() : base(new States.RegistrationBeginState())
        {
        }
    }
}
