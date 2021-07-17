using System;
using Abstractions;
using DefaultNamespace.CommandExecutors;
using UniRx;
using UnityEngine;


namespace Core
{
    public class MainUnit : MonoBehaviour, ISelectableItem, IAttacker, IAttackable, ITeamMember
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
        public IObservable<float> Health => _reactiveHealth;
        public Action<ISelectableItem> OnDied { get; set; }

        private void Awake()
        {
            _reactiveHealth = new ReactiveProperty<float>(_health);
        }

        private void Start()
        {
            UnitsManager.Instance.RegisterUnit(this);
        }

        
        //TODO: move properties up, add regions
        public float AttackRange => 1.5f;
        public float AttackDamage => 25.0f;
        public float AttackCooldown => 1.25f;
        public float VisionRange => 10.0f;
        public Vector3 Position => transform.position;

        public void AttackTarget(IAttackable target)
        {
            var command = new AttackCommand(target);
            GetComponent<AttackCommandExecutor>().Execute(command);
        }
        
        public bool IsDead()
        {
            return _reactiveHealth.Value <= 0.0f;
        }
        
        public void TakeDamage(float damage)
        {
            _reactiveHealth.Value -= damage;

            if (IsDead())
            {
                UnitsManager.Instance.UnregisterUnit(this);
                Destroy(gameObject);
            }
        }

        public bool CanPerformAutoAttack()
        {
            return true;
        }
    }
}