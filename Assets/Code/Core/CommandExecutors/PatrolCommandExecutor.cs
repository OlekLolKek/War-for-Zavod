using Abstractions;
using UnityEngine;
using UnityEngine.AI;


namespace DefaultNamespace.CommandExecutors
{
    public class PatrolCommandExecutor : BaseCommandExecutor<IPatrolCommand>
    {
        [SerializeField] private NavMeshAgent _agent;
        
        protected override void ExecuteSpecificCommand(IPatrolCommand command)
        {
            Debug.Log($"Patrolling from {command.From} to {command.To}");
        }
    }
}