using System;
using Abstractions;
using UniRx;
using UnityEngine;


namespace Core
{
    public class MainBuilding : BaseBuilding, IAttackable
    {
        public Action<ISelectableItem> OnDied { get; set; }
        public Vector3 Position { get; }
        
        protected void Start()
        {
            UnitsManager.Instance.RegisterAttackable(this);
        }
        
        public void TakeDamage(float damage)
        {
            _reactiveHealth.Value -= damage;

            if (_reactiveHealth.Value <= 0.0f)
            {
                UnitsManager.Instance.UnregisterAttackable(this);
                _reactiveHealth.Value = 0;
                OnDied?.Invoke(this);
            }
        }
        
        public bool IsDead()
        {
            return _reactiveHealth.Value <= 0.0f;
        }
    }
}