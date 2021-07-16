using UnityEngine;
using UnityEngine.UI;


namespace InputSystem.UI.View
{
    public class ProductionUnitView : MonoBehaviour
    {
        [SerializeField] private Image _unitIcon;
        [SerializeField] private Text _unitName;

        public Sprite Icon
        {
            set
            {
                _unitIcon.sprite = value;
            }
        }

        public string Name
        {
            set
            {
                _unitName.text = value;
            }
        }

        public void Clear()
        {
            gameObject.SetActive(false);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }
    }
}