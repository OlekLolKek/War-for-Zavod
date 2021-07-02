using System;
using System.Threading.Tasks;
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
            Patrol(command.From, command.To);
        }

        private async void Patrol(Vector3 from, Vector3 to)
        {
            while (true)
            {
                await MoveTo(to);
                await MoveTo(from);
            }
        }

        private async Task MoveTo(Vector3 to)
        {
            _agent.SetDestination(to);
            while ((transform.position - to).magnitude >= 0.6f)
            {
                await Task.Yield();
            }
        }
    }
}