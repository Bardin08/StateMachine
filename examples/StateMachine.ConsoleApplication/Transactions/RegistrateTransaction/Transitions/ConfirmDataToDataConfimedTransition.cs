using System;

using StateMachine.ConsoleApplication.Abstractions;
using StateMachine.ConsoleApplication.Models;

namespace StateMachine.ConsoleApplication.Transactions.RegistrateTransaction
{
    public sealed class ConfirmDataToDataConfimedTransition
        : ITransactionTransition<ConfirmDataState, DataConfirmedState>
    {
        public bool CanTransition(RegistrationStateMachine machine, ConfirmDataState state, RegistrationTransactionModel input)
        {
            return input.UserInput.Equals("Y");
        }

        public DataConfirmedState DoTransition(RegistrationStateMachine machine, ConfirmDataState state, RegistrationTransactionModel input)
        {
            machine.LogMachineState(this.GetType().Name);

            Console.WriteLine("Great! Now your registration is complete.");
            return new DataConfirmedState();
        }
    }
}
