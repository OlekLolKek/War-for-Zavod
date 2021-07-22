using UnityEngine;


namespace Abstractions
{
    public interface IAttacker : ITeamMember
    {
        float VisionRange { get; }
        float AttackRange { get; }
        float AttackDamage { get; }
        float AttackCooldown { get; }
        Vector3 Position { get; }

        void AttackTarget(IAttackable target);
        bool CanPerformAutoAttack();
    }
}