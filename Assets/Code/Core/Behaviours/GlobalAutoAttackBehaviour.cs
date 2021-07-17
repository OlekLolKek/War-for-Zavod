using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


namespace Core.Behaviours
{
    public class GlobalAutoAttackBehaviour : MonoBehaviour
    {
        private void Update()
        {
            Parallel.ForEach(UnitsManager.Instance.Units, AutoAttackNearestTarget);
        }

        private void AutoAttackNearestTarget(MainUnit unit)
        {
            if (!unit.CanPerformAutoAttack())
            {
                return;
            }

            var closestEnemy = UnitsManager.Instance.Units
                .Where(other => 
                    other.Team != unit.Team && 
                    (other.Position - unit.Position).magnitude <= unit.VisionRange)
                .OrderBy(other => 
                    (other.Position - unit.Position).sqrMagnitude)
                .FirstOrDefault();

            unit.AttackTarget(closestEnemy);
        }
    }
}