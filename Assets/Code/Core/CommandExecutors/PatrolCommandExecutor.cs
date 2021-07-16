using System.Threading;
using Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.AI;


namespace DefaultNamespace.CommandExecutors
{
    public class PatrolCommandExecutor : BaseCommandExecutor<IPatrolCommand>
    {
        [SerializeField] private NavMeshAgent _agent;
        
        private Vector3 _targetPosition1;
        private Vector3 _targetPosition2;
        private Vector3 _characterPosition;
        
        private float _deltaTime;

        private readonly Subject<Vector3> _switchedPosition = new Subject<Vector3>();

        protected override void ExecuteSpecificCommand(IPatrolCommand command)
        {
            var currentPatrol = new PatrolOperation(this);
            _switchedPosition.ObserveOnMainThread().Subscribe(SetPath).AddTo(this);
            _targetPosition1 = command.From;
            _targetPosition2 = command.To;
        }

        private void SetPath(Vector3 to)
        {
            _agent.SetDestination(to);
        }

        private void Update()
        {
            _deltaTime = Time.deltaTime;
            _characterPosition = transform.position;
        }

        private class PatrolOperation
        {
            private readonly PatrolCommandExecutor _executor;

            public PatrolOperation(PatrolCommandExecutor executor)
            {
                _executor = executor;
                var thread = new Thread(PatrolRoutine);
                thread.Start();
            }

            private void PatrolRoutine()
            {
                while (true)
                {
                    MoveTo(_executor._targetPosition1);
                    MoveTo(_executor._targetPosition2);
                }
            }

            private void MoveTo(Vector3 to)
            {
                Thread.Sleep(1);
                _executor._switchedPosition.OnNext(to);
                while ((_executor._characterPosition - to).magnitude >= 0.6f)
                {
                    Thread.Sleep((int)(_executor._deltaTime * 1000));
                }
            }
        }
    }
}