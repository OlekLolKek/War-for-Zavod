using System.Linq;
using UnityEngine;


namespace InputSystem.UI.View
{
    public class OutlineSelector : MonoBehaviour
    {
        [SerializeField] private Renderer[] _renderers;
        [SerializeField] private Material _outlineMaterial;

        private bool _isSelectedCache;

        public void SetSelected(bool isSelected)
        {
            if (isSelected == _isSelectedCache)
            {
                return;
            }

            for (int i = 0; i < _renderers.Length; i++)
            {
                var renderer1 = _renderers[i];
                var materialsList = renderer1.materials.ToList();
                if (isSelected)
                {
                    materialsList.Add(_outlineMaterial);
                }
                else
                {
                    materialsList.RemoveAt(materialsList.Count - 1);
                }

                renderer1.materials = materialsList.ToArray();
            }

            _isSelectedCache = isSelected;
        }
    }
}