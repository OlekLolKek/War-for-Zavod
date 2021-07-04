using System;
using Abstractions;
using InputSystem.UI.View;
using UniRx;
using UnityEngine;
using Zenject;


namespace InputSystem.UI.Presenter
{
    public class TopPanelPresenter : MonoBehaviour
    {
        [SerializeField] private TopPanelView _view;
        [SerializeField] private GameObject _menu;
        [Inject] private ITimeModel _timeModel;
        
        private void Awake()
        {
            _view.MenuButton.Subscribe(unit => HandleMenuButtonClick());

            _timeModel.GameTime.Subscribe(time => _view.TimeText = TimeSpan.FromSeconds(time).ToString());
        }

        private void HandleMenuButtonClick()
        {
            _menu.gameObject.SetActive(true);
            _timeModel.Pause();
        }
    }
}