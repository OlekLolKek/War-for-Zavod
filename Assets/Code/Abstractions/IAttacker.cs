using UnityEngine;


namespace Abstractions
{
    public interface IAttacker
    {
        Fractions Fraction { get; }
        float AttackRange { get; }
        float AttackDamage { get; }
        float AttackCooldown { get; }
        Vector3 Position { get; }
    }
}