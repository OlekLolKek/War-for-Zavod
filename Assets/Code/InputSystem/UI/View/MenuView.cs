using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace InputSystem.UI.View
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;

        public IObservable<Unit> ContinueButtonClick => _continueButton.OnClickAsObservable();
    }
}