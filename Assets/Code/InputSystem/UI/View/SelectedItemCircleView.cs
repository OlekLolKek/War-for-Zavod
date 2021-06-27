using Abstractions;
using UnityEngine;


namespace InputSystem.UI.View
{
    public class SelectedItemCircleView : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 10.0f;
        
        private void Update()
        {
            if (gameObject.activeSelf)
            {
                transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
            }
        }

        public void Activate(bool activate, ISelectableItem selectedItem)
        {
            var thisTransform = transform;
            
            gameObject.SetActive(activate);

            if (activate)
            {
                thisTransform.SetParent(selectedItem.SelectionParentTransform);
                thisTransform.position = selectedItem.SelectionParentTransform.position
                                         + selectedItem.SelectionCircleOffset;
            }
            else
            {
                thisTransform.SetParent(null);
            }
        }
    }
}