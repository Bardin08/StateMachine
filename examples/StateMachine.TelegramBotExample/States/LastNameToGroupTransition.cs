using StateMachine.TelegramBotExample.Abstractions;
using StateMachine.TelegramBotExample.Models;

namespace StateMachine.TelegramBotExample.States
{
    public class LastNameToGroupTransition : IRegistrationTransition<LastNameReceivedState, GroupReceivedState>
    {
        public bool CanTransition(RegistrationStateMachine machine, LastNameReceivedState state, RegistrationTransactionModel input)
        {
            return input.ReceivedMessage != null && !string.IsNullOrWhiteSpace(input.ReceivedMessage.Text);
        }

        public GroupReceivedState DoTransition(RegistrationStateMachine machine, LastNameReceivedState state, RegistrationTransactionModel input)
        {
            input.UserModel.LastName = input.ReceivedMessage.Text;
            input.ReceivedMessage = null;
            input.RegistrationState = new GroupReceivedState();

            input.BotClient.SendTextMessageAsync(input.UserId, "To continue send your group.");

            return input.RegistrationState as GroupReceivedState;
        }
    }
}
