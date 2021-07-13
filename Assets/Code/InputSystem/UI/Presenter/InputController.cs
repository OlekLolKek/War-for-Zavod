using Abstractions;
using InputSystem.UI.Model;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;


namespace InputSystem.UI.Presenter
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private SelectedItemModel _currentSelected;
        [SerializeField] private GroundClickModel _groundClickModel;

        private void Start()
        {
            var clickStream = Observable.EveryUpdate()
                    .Where(_ => !_eventSystem.IsPointerOverGameObject() && Input.GetMouseButtonDown(0));
            clickStream.Subscribe(onNext => MousePointerRaycast());
        }

        private void MousePointerRaycast()
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hitInfo))
            {
                var selectableItem = hitInfo.collider.gameObject.GetComponent<ISelectableItem>();
                if (selectableItem != null)
                {
                    _currentSelected.SetValue(selectableItem);
                }
                else
                {
                    _groundClickModel.SetValue(hitInfo.point);
                    _currentSelected.SetValue(null);
                }
            }
        }
    }
}