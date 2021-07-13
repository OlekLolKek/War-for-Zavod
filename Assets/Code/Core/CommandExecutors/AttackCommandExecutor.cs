using System;
using System.Linq;
using Abstractions;
using Core;
using UniRx;
using UnityEngine;
using UnityEngine.AI;


namespace DefaultNamespace.CommandExecutors
{
    public class AttackCommandExecutor : BaseCommandExecutor<IAttackCommand>,
        IAttackTaskWorker
    {
        public IReadOnlyReactiveCollection<IAttackTask> AttackQueue => 
            _attackQueue;

        private readonly ReactiveCollection<IAttackTask> _attackQueue =
            new ReactiveCollection<IAttackTask>();
        
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _remainingDistanceThreshold = 0.75f;
        
        protected override void ExecuteSpecificCommand(IAttackCommand command)
        {
            if (command.Target != null)
            {
                var newTask =
                    new AttackTask(command.Target, _agent,
                        _remainingDistanceThreshold);
                _attackQueue.Add(newTask);
            }
            else
            {
                Debug.Log("You didn't select an enemy.");
            }
        }

        private void SetTarget(AttackTask task)
        {
            _attackQueue.Remove(task);
            _agent.SetDestination(task.Target.SelectionParentTransform.position);
        }

        private void Update()
        {
            if (_attackQueue.Count == 0)
            {
                return;
            }

            var currentTask = (AttackTask) AttackQueue.First();

            if (currentTask.IsEnded())
            {
                SetTarget(currentTask);
            }
        }
    }
}