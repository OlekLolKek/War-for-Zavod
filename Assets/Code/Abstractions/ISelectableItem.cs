using System;
using UniRx;
using UnityEngine;


namespace Abstractions
{
    public interface ISelectableItem
    {
        Fractions Fraction { get; }
        Sprite Icon { get; }
        Transform SelectionParentTransform { get; }
        Vector3 SelectionCircleOffset { get; }
        string Name { get; }
        IObservable<float> Health { get; }
        float MaxHealth { get; }
        void SetFraction(Fractions fraction);
    }
}