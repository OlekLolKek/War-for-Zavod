using Abstractions;
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
        public GameObject GameObject => gameObject;
        public Vector3 SelectionCircleOffset => _selectionCircleOffset;
        public string Name => _name;
        public float MaxHealth => _maxHealth;
        public float Health => _health;

    }
}