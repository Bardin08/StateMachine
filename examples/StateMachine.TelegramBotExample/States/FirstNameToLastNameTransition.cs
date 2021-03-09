using StateMachine.TelegramBotExample.Abstractions;
using StateMachine.TelegramBotExample.Models;

namespace StateMachine.TelegramBotExample.States
{
    public sealed class FirstNameToLastNameTransition : IRegistrationTransition<NameReceivedState, LastNameReceivedState>
    {
        public bool CanTransition(RegistrationStateMachine machine, NameReceivedState state, RegistrationTransactionModel input)
        {
            return input.ReceivedMessage != null && !string.IsNullOrWhiteSpace(input.ReceivedMessage.Text);
        }

        public LastNameReceivedState DoTransition(RegistrationStateMachine machine, NameReceivedState state, RegistrationTransactionModel input)
        {
            input.UserModel.FirstName = input.ReceivedMessage.Text;
            input.ReceivedMessage = null;
            input.RegistrationState = new LastNameReceivedState();

            input.BotClient.SendTextMessageAsync(input.UserId, "To continue send your last name.");

            return input.RegistrationState as LastNameReceivedState;
        }
    }
}
