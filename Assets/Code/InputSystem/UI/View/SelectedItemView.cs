using UnityEngine;
using UnityEngine.UI;


namespace InputSystem.UI.View
{
    public class SelectedItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _name;
        [SerializeField] private Text _health;
        [SerializeField] private Slider _healthBar;

        public Sprite Icon
        {
            set
            {
                _icon.sprite = value;
            }
        }

        public string Name
        {
            set => _name.text = value;
        }

        public string Health
        {
            set => _health.text = value;
        }

        public float HealthPercent
        {
            set => _healthBar.value = value;
        }
    }
}