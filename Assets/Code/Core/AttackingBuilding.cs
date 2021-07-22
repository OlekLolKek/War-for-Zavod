using System;
using Abstractions;
using DefaultNamespace.CommandExecutors;
using UniRx;
using UnityEngine;


namespace Core
{
    [RequireComponent(typeof(AttackCommandExecutor))]
    public class AttackingBuilding : BaseBuilding, IAttackable, IAttacker
    {
        private AttackCommandExecutor _attackCommandExecutor;
        private Vector3 _position;
        private bool _canPerformAutoAttack;
        
        private readonly Subject<AttackCommand> _nextAutoAttack = new Subject<AttackCommand>();
        
        public Action<ISelectableItem> OnDied { get; set; }
        public Vector3 Position => _position;
        public float AttackRange => 7.5f;
        public float AttackDamage => 5.0f;
        public float AttackCooldown => 1.5f;
        
        protected new void Awake()
        {
            base.Awake();
            _attackCommandExecutor = GetComponent<AttackCommandExecutor>();
            
            _nextAutoAttack.ObserveOnMainThread()
                .Subscribe(command => _attackCommandExecutor.Execute(command))
                .AddTo(this);
        }

        private void Update()
        {
            _position = transform.position;
            _canPerformAutoAttack = !_attackCommandExecutor.HasActiveCommand;
        }

        public void AttackTarget(IAttackable target)
        {
            var command = new AttackCommand(target);
            _nextAutoAttack.OnNext(command);
        }

        public void TakeDamage(float damage)
        {
            _reactiveHealth.Value -= damage;

            if (_reactiveHealth.Value <= 0.0f)
            {
                _reactiveHealth.Value = 0;
                OnDied?.Invoke(this);
            }
        }
        
        public bool IsDead()
        {
            return _reactiveHealth.Value <= 0.0f;
        }
        
        public bool CanPerformAutoAttack()
        {
            return _canPerformAutoAttack;
        }
    }
}