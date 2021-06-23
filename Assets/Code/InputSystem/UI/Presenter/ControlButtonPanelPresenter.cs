using System;
using System.Linq;
using Abstractions;
using InputSystem.UI.Model;
using InputSystem.UI.View;
using UnityEngine;


namespace InputSystem.UI.Presenter
{
    public class ControlButtonPanelPresenter : MonoBehaviour
    {
        [SerializeField] private SelectedItemModel _model;
        [SerializeField] private ControlButtonPanelView _view;

        private void Start()
        {
            _model.OnUpdated += SetButtons;
            _view._onClick += ClickHandler;
            SetButtons();
        }

        private void OnDestroy()
        {
            _view._onClick -= ClickHandler;
            _model.OnUpdated -= SetButtons;
        }

        private void ClickHandler(ICommandExecutor executor)
        {
            //TODO: исправить создание команд
            
            executor.Execute(new ProduceUnitCommand(null));
        }

        private void SetButtons()
        {
            if (_model.Value == null)
            {
                _view.ClearButtons();
                return;
            }

            var executors = (_model.Value as Component)?.GetComponents<ICommandExecutor>().ToList();
            _view.SetButtons(executors);
        }
    }
}