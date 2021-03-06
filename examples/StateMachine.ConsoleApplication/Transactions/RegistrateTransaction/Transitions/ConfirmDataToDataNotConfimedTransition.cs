using StateMachine.ConsoleApplication.Abstractions;
using StateMachine.ConsoleApplication.Models;

namespace StateMachine.ConsoleApplication.Transactions.RegistrateTransaction
{
    public sealed class ConfirmDataToDataNotConfimedTransition
        : ITransactionTransition<ConfirmDataState, RegistrationBeginState>
    {
        public bool CanTransition(RegistrationStateMachine machine, ConfirmDataState state, RegistrationTransactionModel input)
        {
            return input.UserInput.Equals("N");
        }

        public RegistrationBeginState DoTransition(RegistrationStateMachine machine, ConfirmDataState state, RegistrationTransactionModel input)
        {
            machine.LogMachineState(this.GetType().Name);

            System.Console.WriteLine("Okay. Let`s try again.");
            return new RegistrationBeginState();
        }
    }
}
