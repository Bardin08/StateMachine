using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Telegram.Bot;
using Telegram.Bot.Args;

namespace StateMachine.TelegramBotExample
{
    class Program
    {
        public static ITelegramBotClient BotClient { get; set; } = new TelegramBotClient("1630056054:AAEWV2RqQnMN46KiF0Jbf4JFXTdQnLQ7xCs");
        public static List<Models.RegistrationTransactionModel> _transactions = new();
        private static RegistrationStateMachine _registrationStateMachine = new RegistrationStateMachine();

        static void Main(string[] args)
        {
            Execute();
        }

        private static void Execute()
        {
            BotClient.OnMessage += MessageReceived;

            BotClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        private static void MessageReceived(object sender, MessageEventArgs e)
        {
            if (e.Message == null)
                return;

            Models.RegistrationTransactionModel registrationTransaction;

            if (_transactions.Any(t => t.UserId == e.Message.From.Id))
            {
                registrationTransaction = _transactions.First(t => t.UserId == e.Message.From.Id);
                registrationTransaction.ReceivedMessage = e.Message;
            }
            else
            {
                registrationTransaction = new Models.RegistrationTransactionModel()
                {
                    UserId = e.Message.From.Id,
                    BotClient = BotClient,
                    ReceivedMessage = e.Message,
                    UserModel = new Models.UserModel(),
                    RegistrationState = new States.RegistrationBeginState()
                };

                _transactions.Add(registrationTransaction);
            }

            _registrationStateMachine.CurrentState = registrationTransaction.RegistrationState;

            _registrationStateMachine.TryTransition(registrationTransaction);
        }
    }
}
