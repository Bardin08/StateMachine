using StateMachine.TelegramBotExample.Abstractions;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace StateMachine.TelegramBotExample.Models
{
    public class RegistrationTransactionModel
    {
        public int UserId { get; set; }
        public IRegistrationState RegistrationState { get; set; }
        public ITelegramBotClient BotClient { get; set; }
        public Message ReceivedMessage { get; set; }
        public UserModel UserModel { get; set; }
    }
}
