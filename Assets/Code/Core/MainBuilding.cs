using System;
using Abstractions;
using UniRx;
using UnityEngine;


namespace Core
{
    public class MainBuilding : MonoBehaviour, ISelectableItem, IAttackable
    {
        [SerializeField] private Team _team;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Vector3 _selectionCircleOffset;
        [SerializeField] private string _name;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _health;
        
        private ReactiveProperty<float> _reactiveHealth;

        public Team Team => _team;
        public Sprite Icon => _icon;
        public Transform SelectionParentTransform => transform;
        public Vector3 SelectionCircleOffset => _selectionCircleOffset;
        public string Name => _name;
        public float MaxHealth => _maxHealth;
        public Action<ISelectableItem> OnDied { get; set; }
        public IObservable<float> Health => _reactiveHealth;
        public Vector3 Position { get; }

        public bool IsDead()
        {
            return _reactiveHealth.Value <= 0.0f;
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
        
        private void Awake()
        {
            _reactiveHealth = new ReactiveProperty<float>(_health);
        }

        public void SetFraction(Team team)
        {
            _team = team;
        }
    }
}