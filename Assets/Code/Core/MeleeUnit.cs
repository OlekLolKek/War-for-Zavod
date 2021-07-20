using System;
using Abstractions;
using DefaultNamespace.CommandExecutors;
using UniRx;
using UnityEngine;


namespace Core
{
    public class MeleeUnit : BaseUnit
    {
        public override float AttackRange => 1.5f;
        public override float AttackDamage => 25.0f;
        public override float AttackCooldown => 1.25f;
        public override float VisionRange => 4.0f;
    }
}