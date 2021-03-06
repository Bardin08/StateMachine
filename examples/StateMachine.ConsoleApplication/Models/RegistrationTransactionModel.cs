namespace StateMachine.ConsoleApplication.Models
{
    public class RegistrationTransactionModel
    {
        public RegistrationTransactionModel()
        {
            UserModel = new UserModel();
        }

        public string TranasactionId { get; set; }
    
        public UserModel UserModel { get; set; }

        public string UserInput { get; set; }

        public bool IsComplete { get; set; }
    }
}
