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
        private AttackCommandExecutor _attackCommandExecutor;
        private MoveCommandExecutor _moveCommandExecutor;
        private bool _canPerformAutoAttack;
        
        private readonly Subject<AttackCommand> _nextAutoAttack = new Subject<AttackCommand>();
        
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

            _attackCommandExecutor = GetComponent<AttackCommandExecutor>();
            _moveCommandExecutor = GetComponent<MoveCommandExecutor>();
        }

        private void Start()
        {
            UnitsManager.Instance.RegisterUnit(this);
            _nextAutoAttack.ObserveOnMainThread()
                .Subscribe(command => _attackCommandExecutor.Execute(command))
                .AddTo(this);
        }

        private void Update()
        {
            _position = transform.position;
            _canPerformAutoAttack = !(_moveCommandExecutor.HasActiveCommand || _attackCommandExecutor.HasActiveCommand);
        }


        //TODO: move properties up, add regions
        public float AttackRange => 1.5f;
        public float AttackDamage => 25.0f;
        public float AttackCooldown => 1.25f;
        public float VisionRange => 3.0f;
        public Vector3 Position => _position;
        private Vector3 _position;

        public void AttackTarget(IAttackable target)
        {
            var command = new AttackCommand(target);
            _nextAutoAttack.OnNext(command);
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
            return _canPerformAutoAttack;
        }
    }
}