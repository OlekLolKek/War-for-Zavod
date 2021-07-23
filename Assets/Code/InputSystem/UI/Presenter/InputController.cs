using Abstractions;
using InputSystem.UI.Model;
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

        public void Update()
        {
            if (_eventSystem.IsPointerOverGameObject())
            {
                return;
            }
            
            if (Input.GetMouseButtonDown(0))
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
}