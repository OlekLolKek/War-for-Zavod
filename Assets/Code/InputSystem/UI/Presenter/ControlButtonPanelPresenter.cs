using System;
using System.Linq;
using Abstractions;
using InputSystem.UI.Model;
using InputSystem.UI.View;
using UnityEngine;
using Utils;


namespace InputSystem.UI.Presenter
{
    public class ControlButtonPanelPresenter : MonoBehaviour
    {
        [SerializeField] private SelectedItemModel _model;
        [SerializeField] private ControlButtonPanelView _view;

        [SerializeField] private AssetsStorage _assets;

        private void Start()
        {
            _model.OnUpdated += SetButtons;
            _view.OnClick += ClickHandler;
            SetButtons();
        }

        private void OnDestroy()
        {
            _view.OnClick -= ClickHandler;
            _model.OnUpdated -= SetButtons;
        }

        private void ClickHandler(ICommandExecutor executor)
        {
            //TODO: исправить создание команд

            if (executor is BaseCommandExecutor<IProduceUnitCommand> produceUnitExecutor)
            {
                produceUnitExecutor.Execute(_assets.Inject(new ProduceUnitCommand()));
            }
            else if (executor is BaseCommandExecutor<IMoveCommand> moveExecutor)
            {
                moveExecutor.Execute(_assets.Inject(new MoveCommand()));
            }
            else if (executor is BaseCommandExecutor<IAttackCommand> attackExecutor)
            {
                attackExecutor.Execute(_assets.Inject(new AttackCommand()));
            }
            else if (executor is BaseCommandExecutor<IPatrolCommand> patrolExecutor)
            {
                patrolExecutor.Execute(_assets.Inject(new PatrolCommand()));
            }
            else if (executor is BaseCommandExecutor<IStopCommand> stopExecutor)
            {
                stopExecutor.Execute(_assets.Inject(new StopCommand()));
            }
            else
            {
                Debug.LogError($"{executor.GetType()} is not supported.");
            }
        }

        private void SetButtons()
        {
            _view.ClearButtons();
            if (_model.Value == null)
            {
                return;
            }

            var executors = (_model.Value as Component)?.GetComponents<ICommandExecutor>().ToList();
            _view.SetButtons(executors);
        }
    }
}