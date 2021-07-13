using System;
using System.Linq;
using Abstractions;
using InputSystem.UI.Model;
using InputSystem.UI.View;
using UnityEngine;
using Utils;
using Zenject;


namespace InputSystem.UI.Presenter
{
    public class ControlButtonPanelPresenter : MonoBehaviour
    {
        [SerializeField] private SelectedItemModel _selectedItemModel;
        [SerializeField] private ControlButtonPanelView _view;

        [Inject] private ControlButtonPanel _controlButtonPanelModel;

        private void Start()
        {
            _selectedItemModel.OnUpdated += SetButtons;
            _view.OnClick += ClickHandler;
            SetButtons();
        }

        private void OnDestroy()
        {
            _view.OnClick -= ClickHandler;
            _selectedItemModel.OnUpdated -= SetButtons;
        }

        private void ClickHandler(ICommandExecutor executor)
        {
            _controlButtonPanelModel.HandleClick(executor);
        }

        private void SetButtons()
        {
            _view.ClearButtons();
            if (_selectedItemModel.Value == null)
            {
                return;
            }

            var executors = (_selectedItemModel.Value as Component)?.GetComponents<ICommandExecutor>().ToList();
            _view.SetButtons(executors);
        }
    }
}