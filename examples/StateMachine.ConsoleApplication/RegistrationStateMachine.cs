using System;

using StateMachine.ConsoleApplication.Abstractions;

namespace StateMachine.ConsoleApplication
{
    public sealed class RegistrationStateMachine
        : StateMachine<ITransactionState, Models.RegistrationTransactionModel>
    {
        public RegistrationStateMachine() 
            : base(new Transactions.RegistrateTransaction.RegistrationBeginState())
        {
        }

        public void LogMachineState(string transitionName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{ this.GetType().Name } [ { CurrentState.GetType().Name } | { transitionName } ]: ");
            Console.ResetColor();
        }
    }
}
