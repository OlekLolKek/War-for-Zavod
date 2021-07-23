using Abstractions;
using InputSystem.UI.Model;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;


namespace InputSystem.UI.Presenter
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private SelectedItemModel _currentSelected;
        [SerializeField] private GroundClickModel _groundClickModel;
        [SerializeField] private AttackableTargetModel _target;

        private void Start()
        {
            var leftClickStream = Observable.EveryUpdate()
                    .Where(_ => !_eventSystem.IsPointerOverGameObject() && Input.GetMouseButtonDown(0));
            leftClickStream.Subscribe(onNext => SelectableRaycast());

            var rightClickStream = Observable.EveryUpdate()
                .Where(_ => !_eventSystem.IsPointerOverGameObject() && Input.GetMouseButtonDown(1));
            rightClickStream.Subscribe(onNext => AttackRaycast());
        }

        private void SelectableRaycast()
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

        private void AttackRaycast()
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hitInfo))
            {
                var selectable = hitInfo.collider.gameObject.GetComponent<ISelectableItem>();
                var attackable = hitInfo.collider.gameObject.GetComponent<IAttackable>();
                
                if (selectable != null)
                {
                    if (attackable != null)
                    {
                        _target.SetValue(attackable);
                    }
                }
                else
                {
                    _groundClickModel.SetValue(hitInfo.point);
                }
            }
        }
    }
}