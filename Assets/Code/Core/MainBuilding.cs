using Abstractions;
using UnityEngine;


namespace DefaultNamespace
{
    public class MainBuilding : MonoBehaviour, ISelectableItem
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _health;

        public Sprite Icon => _icon;
        public string Name => _name;
        public float Health => _health;
        public float MaxHealth => _maxHealth;
    }
}