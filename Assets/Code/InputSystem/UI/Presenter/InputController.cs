using Abstractions;
using InputSystem.UI.Model;
using UnityEngine;


namespace InputSystem.UI.Presenter
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectedItemModel _currentSelected;

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hitInfo))
                {
                    var selectableItem = hitInfo.collider.gameObject.GetComponent<ISelectableItem>();
                    _currentSelected.SetValue(selectableItem);
                }
            }
        }
    }
}