using Abstractions;
using InputSystem.UI.Model;
using InputSystem.UI.View;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


namespace InputSystem.UI.Presenter
{
    public class OutlineSelectorPresenter : MonoBehaviour
    {
        [SerializeField] private SelectedItemModel _model;

        private OutlineSelector[] _outlineSelectors;
        private ISelectableItem _currentSelectable;

        private void Start()
        {
            _model.OnUpdated += OnSelected;
            OnSelected();
        }

        private void OnSelected()
        {
            if (_currentSelectable != _model.Value)
            {
                _currentSelectable = _model.Value;
                SetSelected(_outlineSelectors, false);
                _outlineSelectors = null;

                if (_model.Value != null)
                {
                    _outlineSelectors = (_model.Value as Component).GetComponentsInParent<OutlineSelector>();
                    SetSelected(_outlineSelectors, true);
                }
            }
        }

        private void SetSelected(OutlineSelector[] selectors, bool value)
        {
            if (selectors != null)
            {
                for (int i = 0; i < selectors.Length; i++)
                {
                    selectors[i].SetSelected(value);
                }
            }
        }
    }
}