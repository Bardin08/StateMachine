using StateMachine.ConsoleApplication.Abstractions;
using StateMachine.ConsoleApplication.Models;

namespace StateMachine.ConsoleApplication.Transactions.RegistrateTransaction
{
    public sealed class BeginToConfirmRegistrationTransition
        : ITransactionTransition<RegistrationBeginState, RegistrationBeginConfirmState>
    {
        public bool CanTransition(RegistrationStateMachine machine, RegistrationBeginState state, RegistrationTransactionModel input)
        {
            return true;
        }

        public RegistrationBeginConfirmState DoTransition(RegistrationStateMachine machine, RegistrationBeginState state, RegistrationTransactionModel input)
        {
            machine.LogMachineState(this.GetType().Name);

            state.PrintStateMessage();

            System.Console.WriteLine("Please, press 'Y' if you want to start a registration or 'N' to exit.");
            return new RegistrationBeginConfirmState();
        }
    }
}
