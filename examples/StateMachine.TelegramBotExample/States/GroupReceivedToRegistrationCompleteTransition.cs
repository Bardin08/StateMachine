using StateMachine.TelegramBotExample.Abstractions;
using StateMachine.TelegramBotExample.Models;

namespace StateMachine.TelegramBotExample.States
{
    public sealed class GroupReceivedToRegistrationCompleteTransition : IRegistrationTransition<GroupReceivedState, RegistrationCompleteState>
    {
        public bool CanTransition(RegistrationStateMachine machine, GroupReceivedState state, RegistrationTransactionModel input)
        {
            return input.ReceivedMessage != null && !string.IsNullOrWhiteSpace(input.ReceivedMessage.Text);
        }

        public RegistrationCompleteState DoTransition(RegistrationStateMachine machine, GroupReceivedState state, RegistrationTransactionModel input)
        {
            input.UserModel.Group = input.ReceivedMessage.Text;
            input.ReceivedMessage = null;
            input.RegistrationState = new RegistrationCompleteState();

            input.BotClient.SendTextMessageAsync(input.UserId, $"{input.UserModel.FirstName}, now u`re successfully registered. Your last name is {input.UserModel.LastName} and group is {input.UserModel.Group}");

            return input.RegistrationState as RegistrationCompleteState;
        }
    }
}
