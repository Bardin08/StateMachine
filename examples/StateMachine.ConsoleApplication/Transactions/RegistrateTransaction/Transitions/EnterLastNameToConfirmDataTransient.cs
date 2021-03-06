using StateMachine.ConsoleApplication.Abstractions;
using StateMachine.ConsoleApplication.Models;

namespace StateMachine.ConsoleApplication.Transactions.RegistrateTransaction
{
    public sealed class EnterLastNameToConfirmDataTransient
        : ITransactionTransition<EnterUserSurnameState, ConfirmDataState>
    {
        public bool CanTransition(RegistrationStateMachine machine, EnterUserSurnameState state, RegistrationTransactionModel input)
        {
            return input.UserInput != null;
        }

        public ConfirmDataState DoTransition(RegistrationStateMachine machine, EnterUserSurnameState state, RegistrationTransactionModel input)
        {
            machine.LogMachineState(this.GetType().Name);

            input.UserModel.UserLastName = input.UserInput;
            input.UserInput = null;

            System.Console.WriteLine("Enter 'Y' if all your data is correct and 'N' if no.");
            return new ConfirmDataState();
        }
    }
}
