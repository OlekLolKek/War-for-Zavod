using System.Linq;
using Abstractions;
using Core;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;


namespace DefaultNamespace.CommandExecutors
{
    public class MoveCommandExecutor : BaseCommandExecutor<IMoveCommand>,
        IMoveTaskWorker
    {
        public IReadOnlyReactiveCollection<IMoveTask> MovementQueue => 
            _moveQueue;

        private readonly ReactiveCollection<IMoveTask> _moveQueue =
            new ReactiveCollection<IMoveTask>();
        
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _remainingDistanceThreshold = 0.75f;
        
        protected override void ExecuteSpecificCommand(IMoveCommand command)
        {
            var newTask =
                new MoveTask(command.To, _agent,
                    _remainingDistanceThreshold);
            _moveQueue.Add(newTask);
        }

        private void SetDestination(MoveTask task)
        {
            _moveQueue.Remove(task);
            _agent.SetDestination(task.MovementPoint);
        }

        //TODO: replace with Tick()
        // UPD: Tick method doesn't inject from MoveCommandCreator for some reason,
        // unlike all the [Inject] fields in ProduceUnitCommandExecutor
        public void Update()
        {
            if (_moveQueue.Count == 0)
            {
                return;
            }

            var currentTask = (MoveTask) MovementQueue.First();

            if (currentTask.IsEnded())
            {
                SetDestination(currentTask);
            }
        }
    }
}