using Abstractions;
using UnityEngine;


namespace DefaultNamespace.CommandExecutors
{
    public class StopCommandExecutor : BaseCommandExecutor<IStopCommand>
    {
        protected override void ExecuteSpecificCommand(IStopCommand command)
        {
            Debug.Log("Unit Stopped");
        }
    }
}