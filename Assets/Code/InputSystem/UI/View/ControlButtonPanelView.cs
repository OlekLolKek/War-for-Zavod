using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using UnityEngine;
using UnityEngine.UI;


namespace InputSystem.UI.View
{
    public class ControlButtonPanelView : MonoBehaviour
    {
        [SerializeField] private Button _produceUnitButton;
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _patrolButton;
        [SerializeField] private Button _stopButton;

        private Dictionary<Type, Button> _executorsToButtons;
        public event Action<ICommandExecutor> _onClick;

        private void Awake()
        {
            _executorsToButtons = new Dictionary<Type, Button>
            {
                {typeof(BaseCommandExecutor<IProduceUnitCommand>), _produceUnitButton},
                {typeof(BaseCommandExecutor<IMoveCommand>), _moveButton},
                {typeof(BaseCommandExecutor<IAttackCommand>), _attackButton},
                {typeof(BaseCommandExecutor<IPatrolCommand>), _patrolButton},
                {typeof(BaseCommandExecutor<IStopCommand>), _stopButton},
            };
        }

        public void SetButtons(List<ICommandExecutor> executors)
        {
            foreach (var executor in executors)
            {
                var button = 
                    _executorsToButtons.FirstOrDefault(kvp => kvp.Key.IsInstanceOfType(executor))
                        .Value;
                
                if (button == null)
                {
                    Debug.LogError("Executor is not supported!");
                    continue;
                }
                
                button.gameObject.SetActive(true);
                button.onClick.AddListener(() => _onClick?.Invoke(executor));
            }
        }

        public void ClearButtons()
        {
            foreach (var button in _executorsToButtons.Values)
            {
                button.gameObject.SetActive(false);
                button.onClick.RemoveAllListeners();
            }
        }
    }
}