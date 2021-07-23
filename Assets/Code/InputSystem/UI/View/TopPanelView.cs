using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace InputSystem.UI.View
{
    public class TopPanelView : MonoBehaviour
    {
        [SerializeField] private Text _timeText;
        [SerializeField] private Text _coinText;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _addCoinsButton;

        public string TimeText
        {
            set => _timeText.text = value;
        }

        public string CoinText
        {
            set => _coinText.text = value;
        }
        
        public IObservable<Unit> MenuButton => _menuButton.OnClickAsObservable();
        public IObservable<Unit> AddCoinsButton => _addCoinsButton.OnClickAsObservable();
    }
}