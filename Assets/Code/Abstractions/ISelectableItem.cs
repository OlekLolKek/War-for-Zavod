using UnityEngine;


namespace Abstractions
{
    public interface ISelectableItem
    {
        Sprite Icon { get; }
        Transform SelectionParentTransform { get; }
        Vector3 SelectionCircleOffset { get; }
        string Name { get; }
        float Health { get; }
        float MaxHealth { get; }
    }
}