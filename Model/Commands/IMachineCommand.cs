namespace TuringMachine.Model.Commands
{
    interface IMachineCommand
    {
        bool IsValid(string action);
               
    }
}
