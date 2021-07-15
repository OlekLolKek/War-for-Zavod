using System;
using UnityEngine;


namespace Abstractions
{
    public interface IAttackable
    {
        IObservable<float> Health { get; }
        Vector3 Position { get; }

        bool IsDead();
        void TakeDamage(float damage);
    }
}