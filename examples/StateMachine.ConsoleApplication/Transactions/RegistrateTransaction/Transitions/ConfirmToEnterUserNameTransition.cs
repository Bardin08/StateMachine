using StateMachine.ConsoleApplication.Abstractions;
using StateMachine.ConsoleApplication.Models;

namespace StateMachine.ConsoleApplication.Transactions.RegistrateTransaction
{
    public sealed class ConfirmToEnterUserNameTransition
        : ITransactionTransition<RegistrationBeginConfirmState, EnterUserNameState>
    {
        public bool CanTransition(RegistrationStateMachine machine, RegistrationBeginConfirmState state, RegistrationTransactionModel input)
        {
            return input.UserInput.Equals("Y");
        }

        public EnterUserNameState DoTransition(RegistrationStateMachine machine, RegistrationBeginConfirmState state, RegistrationTransactionModel input)
        {
            machine.LogMachineState(this.GetType().Name);

            System.Console.Write("Enter your name: ");
            return new EnterUserNameState();
        }
    }
}
