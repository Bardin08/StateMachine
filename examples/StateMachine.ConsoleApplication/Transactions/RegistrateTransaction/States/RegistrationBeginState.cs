using StateMachine.ConsoleApplication.Abstractions;

namespace StateMachine.ConsoleApplication.Transactions.RegistrateTransaction
{
    public sealed class RegistrationBeginState : ITransactionState
    {
        public void PrintStateMessage() 
        {
            System.Console.WriteLine("Welcome to this wonderful application. To begin using this application you should register.");
        }
    }
}
