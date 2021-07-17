using UnityEngine;


namespace Abstractions
{
    public interface IAttacker
    {
        float AttackRange { get; }
        float AttackDamage { get; }
        float AttackCooldown { get; }
        Vector3 Position { get; }

        void AttackTarget(IAttackable target);
    }
}