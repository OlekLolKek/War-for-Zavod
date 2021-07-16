using System;
using System.Threading;
using Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.AI;


namespace DefaultNamespace.CommandExecutors
{
    public class AttackCommandExecutor : BaseCommandExecutor<IAttackCommand>
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        private AttackOperation _currentAttack;
        private IAttackable _target;
        private IAttacker _attacker;
        
        private Vector3 _targetPosition;
        private Vector3 _attackerPosition;

        private Subject<Vector3> _calculatedPositions = new Subject<Vector3>();
        private Subject<IAttackable> _calculatedTargets = new Subject<IAttackable>();

        protected override void ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log("Attack");
            _target = command.Target;
            _attacker = GetComponent<IAttacker>();
            _currentAttack = new AttackOperation(this, _attacker, _target);

            _calculatedPositions.ObserveOnMainThread().Subscribe(Move).AddTo(this);
            _calculatedTargets.ObserveOnMainThread().Subscribe(DoAttack).AddTo(this);
        }

        private void Update()
        {
            if (_currentAttack == null)
            {
                return;
            }
            
            lock (_attacker)
            {
                if (_target == null || _target.IsDead())
                {
                    return;
                }

                _targetPosition = _target.Position;
                _attackerPosition = _attacker.Position;
            }
        }
        
        private void Move(Vector3 to)
        {
            _navMeshAgent.SetDestination(to);
        }

        private void DoAttack(IAttackable target)
        {
            _navMeshAgent.ResetPath();
            target.TakeDamage(_attacker.AttackDamage);
        }

        public class AttackOperation
        {
            private readonly AttackCommandExecutor _attackCommandExecutor;
            private readonly IAttacker _attacker;
            private readonly IAttackable _target;
            private bool _targetIsDead;
            
            public AttackOperation(AttackCommandExecutor attackCommandExecutor,
                IAttacker attacker, IAttackable target)
            {
                _attackCommandExecutor = attackCommandExecutor;
                _attacker = attacker;
                _target = target;
                _target.Health.Subscribe(value =>
                {
                    _targetIsDead = value <= 0;
                });
                
                var thread = new Thread(AttackRoutine);
                thread.Start();
            }

            private void AttackRoutine()
            {
                while (!_targetIsDead)
                {
                    Vector3 targetPosition;
                    Vector3 attackerPosition;
                
                    lock (_attacker)
                    {
                        targetPosition = _attackCommandExecutor._targetPosition;
                        attackerPosition = _attackCommandExecutor._attackerPosition;
                    }
                
                    var distance = (targetPosition - attackerPosition).magnitude;
                    if (distance <= _attacker.AttackRange)
                    {
                        _attackCommandExecutor._calculatedTargets.OnNext(_target);
                        Thread.Sleep((int) (_attacker.AttackCooldown * 1000));
                    }
                    else
                    {
                        _attackCommandExecutor._calculatedPositions.OnNext(targetPosition);
                        Thread.Sleep(100);
                    }
                }
            }
        }
    }
}