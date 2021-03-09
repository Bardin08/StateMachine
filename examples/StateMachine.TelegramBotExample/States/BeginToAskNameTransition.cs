using StateMachine.TelegramBotExample.Abstractions;
using StateMachine.TelegramBotExample.Models;

namespace StateMachine.TelegramBotExample.States
{
    public sealed class BeginToAskNameTransition : IRegistrationTransition<RegistrationBeginState, NameReceivedState>
    {
        public bool CanTransition(RegistrationStateMachine machine, RegistrationBeginState state, RegistrationTransactionModel input)
        {
            return true;
        }

        public NameReceivedState DoTransition(RegistrationStateMachine machine, RegistrationBeginState state, RegistrationTransactionModel input)
        {
            input.BotClient.SendTextMessageAsync(input.UserId, "Welcome to this bot! Enter your name to continue.");
            input.RegistrationState = new NameReceivedState();

            return input.RegistrationState as NameReceivedState;
        }
    }
}
