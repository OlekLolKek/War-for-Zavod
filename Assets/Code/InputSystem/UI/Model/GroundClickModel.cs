using System;
using Abstractions;
using UnityEngine;


namespace InputSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(GroundClickModel), menuName = "Strategy/" + nameof(GroundClickModel))]
    public class GroundClickModel : ScriptableObject
    {
        private Vector3 _value;
        public Vector3 Value => _value;

        public void SetValue(Vector3 value)
        {
            _value = value;
            OnUpdated?.Invoke();
        }
        
        public event Action OnUpdated;
    }
}