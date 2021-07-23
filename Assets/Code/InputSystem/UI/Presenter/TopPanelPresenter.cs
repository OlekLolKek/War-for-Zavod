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
        [Inject] private ICoinModel _coinModel;
        
        private void Awake()
        {
            _view.MenuButton.Subscribe(unit => HandleMenuButtonClick());

            _view.AddCoinsButton.Subscribe(unit => HandleDebugAddCoinsButtonClick());

            _timeModel.GameTime.Subscribe(
                time => _view.TimeText = TimeSpan.FromSeconds(time).ToString());

            _coinModel.Coins.Subscribe(
                coins => _view.CoinText = "Coins: " + coins);
        }

        private void HandleMenuButtonClick()
        {
            _menu.gameObject.SetActive(true);
            _timeModel.Pause();
        }

        private void HandleDebugAddCoinsButtonClick()
        {
            _coinModel.AddCoins(100);
        }
    }
}