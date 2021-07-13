using System;
using InputSystem.UI.Model;
using InputSystem.UI.View;
using UnityEngine;


namespace InputSystem.UI.Presenter
{
    public class SelectedItemCirclePresenter : MonoBehaviour
    {
        [SerializeField] private SelectedItemModel _model;
        [SerializeField] private SelectedItemCircleView _view;

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
            _view.Activate(_model.Value != null, _model.Value);
        }
    }
}