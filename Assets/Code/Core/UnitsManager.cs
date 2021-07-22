using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using Core.Behaviours;
using UnityEngine;


namespace Core
{
    public class UnitsManager : MonoBehaviour
    {
        public ConcurrentBag<IAttacker> Attackers { get; private set; }
        public ConcurrentBag<IAttackable> Attackables { get; private set; }
        
        public static UnitsManager Instance { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Attackers = new ConcurrentBag<IAttacker>();
            Attackables = new ConcurrentBag<IAttackable>();
        }

        public void RegisterAttacker(IAttacker attacker)
        {
            Attackers.Add(attacker);
        }

        public void RegisterAttackable(IAttackable attackable)
        {
            Attackables.Add(attackable);
        }

        public void UnregisterAttacker(IAttacker attacker)
        {
            Attackers = new ConcurrentBag<IAttacker>(Attackers.Except(new List<IAttacker> {attacker}));
        }

        public void UnregisterAttackable(IAttackable attackable)
        {
            Attackables = new ConcurrentBag<IAttackable>(Attackables.Except(new List<IAttackable> {attackable}));
        }
    }
}