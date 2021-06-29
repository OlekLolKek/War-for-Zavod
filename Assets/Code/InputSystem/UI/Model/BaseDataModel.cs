using System;
using UnityEngine;


namespace InputSystem.UI.Model
{
    public class BaseDataModel<T> : ScriptableObject
    {
        private T _value;
        public T Value => _value;

        public virtual void SetValue(T value)
        {
            _value = value;
            OnUpdated?.Invoke();
        }

        public event Action OnUpdated;
    }
}