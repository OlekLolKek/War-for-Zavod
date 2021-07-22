using System;
using Abstractions;
using DefaultNamespace.CommandExecutors;
using UniRx;
using UnityEngine;


namespace Core
{
    public abstract class BaseUnit : MonoBehaviour, IUnit
    {
        #region Fields
        
        [SerializeField] private Team _team;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Vector3 _selectionCircleOffset;
        [SerializeField] private string _name;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _health;
        
        private ReactiveProperty<float> _reactiveHealth;
        private AttackCommandExecutor _attackCommandExecutor;
        private MoveCommandExecutor _moveCommandExecutor;
        private Vector3 _position;
        private bool _canPerformAutoAttack;
        
        private readonly Subject<AttackCommand> _nextAutoAttack = new Subject<AttackCommand>();

        #endregion
        
        #region Properties

        public Action<ISelectableItem> OnDied { get; set; }

        public IObservable<float> Health => _reactiveHealth;
        public Team Team => _team;
        public Sprite Icon => _icon;
        public Transform SelectionParentTransform => transform;
        public Vector3 SelectionCircleOffset => _selectionCircleOffset;
        public Vector3 Position => _position;
        public string Name => _name;
        
        public float MaxHealth => _maxHealth;
        public abstract float AttackRange { get; }
        public abstract float AttackDamage { get; }
        public abstract float AttackCooldown { get; }
        public abstract float VisionRange { get; }

        #endregion
        
        #region UnityMethods

        protected void Awake()
        {
            _reactiveHealth = new ReactiveProperty<float>(_health);

            _attackCommandExecutor = GetComponent<AttackCommandExecutor>();
            _moveCommandExecutor = GetComponent<MoveCommandExecutor>();
        }

        protected void Start()
        {
            UnitsManager.Instance.RegisterUnit(this);
            _nextAutoAttack.ObserveOnMainThread()
                .Subscribe(command => _attackCommandExecutor.Execute(command))
                .AddTo(this);
        }

        protected void Update()
        {
            _position = transform.position;
            _canPerformAutoAttack = !(_moveCommandExecutor.HasActiveCommand || _attackCommandExecutor.HasActiveCommand);
        }

        #endregion


        #region Methods

        public void AttackTarget(IAttackable target)
        {
            var command = new AttackCommand(target);
            _nextAutoAttack.OnNext(command);
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
        
        public bool IsDead()
        {
            return _reactiveHealth.Value <= 0.0f;
        }

        public bool CanPerformAutoAttack()
        {
            return _canPerformAutoAttack;
        }

        #endregion
    }
}