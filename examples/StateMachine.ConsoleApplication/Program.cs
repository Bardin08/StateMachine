using System;

using StateMachine.ConsoleApplication.Transactions.RegistrateTransaction;

namespace StateMachine.ConsoleApplication
{
    internal class Program
    {
        public static Models.RegistrationTransactionModel RegistrationModel { get; set; } = new Models.RegistrationTransactionModel();

        private static void Main()
        {
            var rsm = new RegistrationStateMachine();

            while (rsm.CurrentState.GetType() != typeof(DataConfirmedState))
            {
                RegistrationModel.UserInput = Console.ReadLine();

                rsm.TryTransition(RegistrationModel);
            }

            Console.WriteLine($"Welcome, {RegistrationModel.UserModel.UserFirstName}!");

        }
    }
}
