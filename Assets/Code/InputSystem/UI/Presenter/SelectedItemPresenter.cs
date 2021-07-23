using System;
using InputSystem.UI.Model;
using InputSystem.UI.View;
using UniRx;
using UnityEngine;


namespace InputSystem.UI.Presenter
{
    public class SelectedItemPresenter : MonoBehaviour
    {
        [SerializeField] private SelectedItemModel _model;
        [SerializeField] private SelectedItemView _view;

        private IDisposable _healthUpdater;

        private void Start()
        {
            _model.OnUpdated += UpdateView;
            UpdateView();
        }

        private void OnDestroy()
        {
            _model.OnUpdated -= UpdateView;
        }

        private void UpdateView()
        {
            if (_healthUpdater != null)
            {
                _healthUpdater.Dispose();
                _healthUpdater = null;
            }

            if (_model.Value == null)
            {
                _view.gameObject.SetActive(false);
                _view.transform.parent.gameObject.SetActive(false);
                return;
            }

            _view.gameObject.SetActive(true);
            _view.transform.parent.gameObject.SetActive(true);
            _view.Icon = _model.Value.Icon;
            _view.Name = _model.Value.Name;

            _healthUpdater = _model.Value.Health.Subscribe(currentHealth =>
            {
                _view.HealthPercent = currentHealth / _model.Value.MaxHealth;
                _view.Health =$"Health: {currentHealth} / {_model.Value.MaxHealth}";
            });
        }
    }
}