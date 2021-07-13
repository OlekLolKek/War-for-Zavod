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
            if (_model.Value == null)
            {
                _view.transform.SetParent(null);
                _view.gameObject.SetActive(false);
                return;
            }

            _view.gameObject.SetActive(true);
            _view.transform.position = _model.Value.GameObject.transform.position
                                       + _model.Value.SelectionCircleOffset;
            _view.transform.SetParent(_model.Value.GameObject.transform);
        }
    }
}