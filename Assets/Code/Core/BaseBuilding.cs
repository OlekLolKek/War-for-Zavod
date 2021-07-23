using System;
using Abstractions;
using UniRx;
using UnityEngine;


namespace Core
{
    public class BaseBuilding : MonoBehaviour, ISelectableItem, ITeamMember
    {
        [SerializeField] private Team _team;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Vector3 _selectionCircleOffset;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _health;
        [SerializeField] private string _name;
        
        protected ReactiveProperty<float> _reactiveHealth;

        public Team Team => _team;
        public Sprite Icon => _icon;
        public Transform SelectionParentTransform => transform;
        public Vector3 SelectionCircleOffset => _selectionCircleOffset;
        public string Name => _name;
        public IObservable<float> Health => _reactiveHealth;
        public float MaxHealth => _maxHealth;

        protected void Awake()
        {
            _reactiveHealth = new ReactiveProperty<float>(_health);
        }
        
        public void SetFraction(Team team)
        {
            _team = team;
        }
    }
}