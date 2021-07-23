using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using InputSystem.UI.Model;
using InputSystem.UI.View;
using UniRx;
using UnityEngine;
using Zenject;


namespace InputSystem.UI.Presenter
{
    public class ProductionPanelPresenter : MonoBehaviour
    {
        [SerializeField] private ProductionPanelView _view;

        [Inject] private ProductionPanel _model;
        [Inject] private SelectedItemModel _selectedItem;

        private readonly List<IDisposable> _disposables = new List<IDisposable>();
        
        protected void Awake()
        {
            _selectedItem.OnUpdated += HandleSelectionChanged;
            HandleSelectionChanged();
        }

        private void HandleSelectionChanged()
        {
            _view.ClearLine();
            if (_selectedItem.Value == null)
            {
                _view.gameObject.SetActive(false);
                _view.ClearLine();
                return;
            }

            if (!((Component) _selectedItem.Value).TryGetComponent<IProductionTaskWorker>(out var productionTaskWorker))
            {
                _view.gameObject.SetActive(false);
                return;
            }
            
            _view.gameObject.SetActive(true);
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            _disposables.Clear();
            
            UpdateProductionQueue(productionTaskWorker);
        }

        private void UpdateProductionQueue(IProductionTaskWorker productionTaskWorker)
        {
            _model.SetWorker(productionTaskWorker);
            _view.SetLine(productionTaskWorker.ProductionQueue.ToList());

            _disposables.Add(_model.AddUnit.Subscribe(e => _view.AddNewTask(e.Value)));
            _disposables.Add(_model.RemoveUnit.Subscribe(e => _view.RemoveCurrentTask()));
        }

        private void OnDestroy()
        {
            _selectedItem.OnUpdated -= HandleSelectionChanged;
        }
    }
}