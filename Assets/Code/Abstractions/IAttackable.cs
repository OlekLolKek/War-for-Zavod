using System;
using UnityEngine;


namespace Abstractions
{
    public interface IAttackable
    {
        Fractions Fraction { get; }
        Action<ISelectableItem> OnDied { get; set; }
        IObservable<float> Health { get; }
        Vector3 Position { get; }

        bool IsDead();
        void TakeDamage(float damage);
    }
}