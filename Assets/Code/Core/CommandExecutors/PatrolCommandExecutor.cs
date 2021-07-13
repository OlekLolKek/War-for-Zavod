using Abstractions;
using UnityEngine;


namespace DefaultNamespace.CommandExecutors
{
    public class PatrolCommandExecutor : BaseCommandExecutor<IPatrolCommand>
    {
        protected override void ExecuteSpecificCommand(IPatrolCommand command)
        {
            Debug.Log("Patrol started");
        }
    }
}