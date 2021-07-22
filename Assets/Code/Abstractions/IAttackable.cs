using System;
using UnityEngine;


namespace Abstractions
{
    public interface IAttackable
    {
        Action<ISelectableItem> OnDied { get; set; }
        Vector3 Position { get; }
        IObservable<float> Health { get; }
        float MaxHealth { get; }
        
        void TakeDamage(float damage);
        bool IsDead();
    }
}