using System;

using StateMachine.ConsoleApplication.Abstractions;
using StateMachine.ConsoleApplication.Models;

namespace StateMachine.ConsoleApplication.Transactions.RegistrateTransaction
{
    public sealed class ConfirmToExitTransition
        : ITransactionTransition<RegistrationBeginConfirmState, RegistrationBeginConfirmState>
    {
        public bool CanTransition(RegistrationStateMachine machine, RegistrationBeginConfirmState state, RegistrationTransactionModel input)
        {
            return input.UserInput.Equals("N");
        }

        public RegistrationBeginConfirmState DoTransition(RegistrationStateMachine machine, RegistrationBeginConfirmState state, RegistrationTransactionModel input)
        {
            machine.LogMachineState(this.GetType().Name);

            Environment.Exit(0);
            return state;
        }
    }
}
