using System.Linq;
using System.Threading.Tasks;
using Abstractions;
using UnityEngine;


namespace Core.Behaviours
{
    public class GlobalAutoAttackBehaviour : MonoBehaviour
    {
        private void Update()
        {
            Parallel.ForEach(UnitsManager.Instance.Attackers, AutoAttackNearestTarget);
        }

        private void AutoAttackNearestTarget(IAttacker attacker)
        {
            if (!attacker.CanPerformAutoAttack())
            {
                return;
            }

            IAttackable closestAttackable = null;
            float closestDistance = float.MaxValue;
            
            foreach (var attackable in UnitsManager.Instance.Attackables)
            {
                if (attackable.Team != attacker.Team)
                {
                    if ((attackable.Position - attacker.Position).magnitude <= attacker.VisionRange)
                    {
                        if ((attackable.Position - attacker.Position).magnitude < closestDistance)
                        {
                            closestDistance = (attackable.Position - attacker.Position).magnitude;
                            closestAttackable = attackable;
                        }
                    }
                }
            }
        
            if (closestAttackable != null)
            {
                attacker.AttackTarget(closestAttackable);
            }
        }
    }
}