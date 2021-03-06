using StateMachine.ConsoleApplication.Abstractions;
using StateMachine.ConsoleApplication.Models;

namespace StateMachine.ConsoleApplication.Transactions.RegistrateTransaction
{
    public sealed class EnterFirstNameToEnterLastNameTransition
        : ITransactionTransition<EnterUserNameState, EnterUserSurnameState>
    {
        public bool CanTransition(RegistrationStateMachine machine, EnterUserNameState state, RegistrationTransactionModel input)
        {
            return input.UserInput != null;
        }

        public EnterUserSurnameState DoTransition(RegistrationStateMachine machine, EnterUserNameState state, RegistrationTransactionModel input)
        {
            machine.LogMachineState(this.GetType().Name);

            input.UserModel.UserFirstName = input.UserInput;

            input.UserInput = null;

            System.Console.Write("Enter your surname: ");
            return new EnterUserSurnameState();
        }
    }
}
