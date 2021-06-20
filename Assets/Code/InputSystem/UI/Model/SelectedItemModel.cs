using System;
using Abstractions;
using UnityEngine;


namespace InputSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(SelectedItemModel), menuName = "Strategy/Models")]
    public class SelectedItemModel : ScriptableObject
    {
        private ISelectableItem _value;
        public ISelectableItem Value => _value;

        public void SetValue(ISelectableItem value)
        {
            _value = value;
            OnUpdated?.Invoke();
        }
        
        public event Action OnUpdated;
    }
}