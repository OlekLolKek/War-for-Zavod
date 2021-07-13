using Abstractions;
using InputSystem.UI.View;
using UniRx;
using UnityEngine;
using Zenject;


namespace InputSystem.UI.Presenter
{
    public class MenuPresenter : MonoBehaviour
    {
        [SerializeField] private MenuView _view;
        [Inject] private ITimeModel _timeModel;
        
        private void Awake()
        {
            _view.ContinueButtonClick.Subscribe(unit => HandleContinueButtonClick());
        }

        private void HandleContinueButtonClick()
        {
            gameObject.SetActive(false);
            _timeModel.Unpause();
        }
    }
}