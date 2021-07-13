using System.Runtime.CompilerServices;
using Abstractions;
using UnityEngine;


namespace DefaultNamespace
{
    public class MainBuilding : MonoBehaviour, ISelectableItem
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private Vector3 _selectionCircleOffset;
        [SerializeField] private string _name;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _health;

        public Sprite Icon => _icon;
        public GameObject GameObject => gameObject;
        public Vector3 SelectionCircleOffset => _selectionCircleOffset;
        public string Name => _name;
        public float Health => _health;
        public float MaxHealth => _maxHealth;
    }
}