using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace InputSystem.UI.View
{
    public class TopPanelView : MonoBehaviour
    {
        [SerializeField] private Text _timeText;
        [SerializeField] private Button _menuButton;

        public string TimeText
        {
            set => _timeText.text = value;
        }
        
        public IObservable<Unit> MenuButton => _menuButton.OnClickAsObservable();
    }
}