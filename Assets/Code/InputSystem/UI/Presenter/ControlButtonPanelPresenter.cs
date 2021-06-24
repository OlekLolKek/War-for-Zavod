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
            
            executor.Execute(_assets.Inject(new ProduceUnitCommand()));
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