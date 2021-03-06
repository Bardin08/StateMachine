# StateMachine
.NET state-machine implementation. 

## What is a state-machine
A state-machine (SM) is a mathematical model of computation. It is an abstract machine that can be in exactly one of a finite number of states at any given time. 
The SM can change from one state to another in response to some inputs; the change from one state to another is called a transition.

## Installation:
Add StateMachine.dll to your project's references and include StateMachine namespace.

## Usage Example

To use a state machine follow 3 simple steps:
1. Create a state-machine
2. Create states and transitions base types
3. Add some states and transitions

### State-Machine creation
To create a state-machine you need to inherit the`StateMachine.StateMachine` class or to implement a `StateMachine.Abstractions.IStateMachine` interface.
`StateMachine.StateMachine` is a default implementation of this interface. Class StateMachine is abstract. It has several protected constructors, so you need 
to add at least one public constructor which will call a base one. 
Also, it’s possible to add some properties and methods to the state-machine.

```CSharp
public sealed class RegistrationStateMachine : StateMachine<ITransactionState, Models.RegistrationTransactionModel>
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
```
Here `ITransactionState` is a state base type. It will be used to find all states in assembly. `Models.RegistrationTransactionModel`
is an input that every state will receive.


### States and transitions base types creation
States base types must implement `StateMachine.Abstractions.IState` interface.
```CSharp
public interface ITransactionState : StateMachine.Abstractions.IState
{
}
```

Transition base states must implement `StateMachine.Abstractions.ITransition<TStateMachine, TState, TInputState, TInput, TOutoutState>` interface.
TInputState and TOutputState must implement or inherit a TState. 
```CSharp
public interface ITransactionTransition<TInputState, TOutputState> 
    : StateMachine.Abstractions.ITransition<RegistrationStateMachine,
                                            ITransactionState,
                                            TInputState,
                                            RegistrationTransactionModel,
                                            TOutputState>
    where TInputState : ITransactionState
    where TOutputState : ITransactionState
{
}
```


### States and transitions creation
Here would be shown how to create a state and a transition. For a full example visit an example folder.

Each state must implement a base type which defined for states for a current state-machine. All states can contain additional methods and properties which could be used while
transiting.
```CSharp
public sealed class RegistrationBeginState : ITransactionState
{
    public void PrintStateMessage() 
    {
        System.Console.WriteLine("Welcome to this wonderful application. To begin using this application you should register.");
    }
}
```
Transition must implement a base type which defined for transitions for a current state-machine.
```CSharp
public sealed class BeginToConfirmRegistrationTransition
    : ITransactionTransition<RegistrationBeginState, RegistrationBeginConfirmState>
{
    public bool CanTransition(RegistrationStateMachine machine,
                              RegistrationBeginState state,
                              RegistrationTransactionModel input)
    {
        return true;
    }

    public RegistrationBeginConfirmState DoTransition(RegistrationStateMachine machine,
                                                      RegistrationBeginState state,
                                                      RegistrationTransactionModel input)
    {
        machine.LogMachineState(this.GetType().Name);

        state.PrintStateMessage();

        System.Console.WriteLine("Please, press 'Y' if you want to start a registration or 'N' to exit.");
        return new RegistrationBeginConfirmState();
    }
}
```
`CanTransition(TStateMachine machine, TState state, TInput input)` method should return a boolean value that would show if it's possible to do this transition.
`DoTransition(TStateMachine machine, TState state, TInput input)` method should contain all logic that should be done while this transition.

```CSharp
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
```

To try to do a transition you should call a `TryTransition(TInput input)` method.
