using Abstractions;
using UnityEngine;
using UnityEngine.AI;


namespace DefaultNamespace.CommandExecutors
{
    public class StopCommandExecutor : BaseCommandExecutor<IStopCommand>
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        protected override void ExecuteSpecificCommand(IStopCommand command)
        {
            _navMeshAgent.SetDestination(_navMeshAgent.transform.position);
            Debug.Log($"{_navMeshAgent.gameObject} Stopped");
        }
    }
}