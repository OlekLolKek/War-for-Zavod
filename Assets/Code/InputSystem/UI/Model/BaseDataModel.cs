using System;
using System.Threading.Tasks;
using UnityEngine;
using Utils;


namespace InputSystem.UI.Model
{
    public abstract class BaseDataModel<T> : ScriptableObject, IAwaitable<T>
    {
        private T _value;
        public T Value => _value;
        public event Action OnUpdated;

        public virtual void SetValue(T value)
        {
            _value = value;
            OnUpdated?.Invoke();
        }

        public IAwaiter<T> GetAwaiter()
        {
            return new ValueChangedNotifier(this);
        }
        

        public Task<T> GetNextValue()
        {
            var task = new TaskCompletionSource<T>();
            OnUpdated += () =>
            {
                task.SetResult(Value);
                OnUpdated = null;
            };
            return task.Task;
        }

        private class ValueChangedNotifier : IAwaiter<T>
        {
            private readonly BaseDataModel<T> _valueContainer;
            private Action _continuation;
            private T _result;
            private bool _isCompleted;
            
            public ValueChangedNotifier(BaseDataModel<T> valueContainer)
            {
                _valueContainer = valueContainer;
                _valueContainer.OnUpdated += HandleValueChanged;
            }
            
            public void OnCompleted(Action continuation)
            {
                _continuation = continuation;
                if (IsCompleted)
                {
                    _continuation?.Invoke();
                }
            }

            private void HandleValueChanged()
            {
                _valueContainer.OnUpdated -= HandleValueChanged;
                _isCompleted = true;
                _result = _valueContainer.Value;
                _continuation?.Invoke();
            }

            public bool IsCompleted => _isCompleted;
            public T GetResult()
            {
                return _result;
            }
        }
    }
}