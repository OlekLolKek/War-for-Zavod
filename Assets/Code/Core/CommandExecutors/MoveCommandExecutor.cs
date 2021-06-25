using Abstractions;
using UnityEngine;


namespace DefaultNamespace.CommandExecutors
{
    public class MoveCommandExecutor : BaseCommandExecutor<IMoveCommand>
    {
        protected override void ExecuteSpecificCommand(IMoveCommand command)
        {
            Debug.Log("Unit moved");
        }
    }
}