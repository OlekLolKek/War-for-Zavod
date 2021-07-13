using System;
using Abstractions;
using UniRx;
using UnityEngine;


namespace Core
{
    public class MainUnit : MonoBehaviour, ISelectableItem
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private Vector3 _selectionCircleOffset;
        [SerializeField] private string _name;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _health;

        public Sprite Icon => _icon;
        public Transform SelectionParentTransform => transform;
        public Vector3 SelectionCircleOffset => _selectionCircleOffset;
        public string Name => _name;
        public float MaxHealth => _maxHealth;
        public IObservable<float> Health => _reactiveHealth;

        private ReactiveProperty<float> _reactiveHealth;

        private void Awake()
        {
            _reactiveHealth = new ReactiveProperty<float>(_health);
        }
    }
}