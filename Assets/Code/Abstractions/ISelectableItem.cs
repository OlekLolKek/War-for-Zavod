using UnityEngine;


namespace Abstractions
{
    public interface ISelectableItem
    {
        Sprite Icon { get; }
        string Name { get; }
        float Health { get; }
        float MaxHealth { get; }
    }
}