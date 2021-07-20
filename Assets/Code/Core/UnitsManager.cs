using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Core.Behaviours;
using UnityEngine;


namespace Core
{
    public class UnitsManager : MonoBehaviour
    {
        //TODO: change to BaseUnit
        public ConcurrentBag<BaseUnit> Units { get; private set; }
        
        public static UnitsManager Instance { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Units = new ConcurrentBag<BaseUnit>();
        }

        public void RegisterUnit(BaseUnit unit)
        {
            Units.Add(unit);
        }

        public void UnregisterUnit(BaseUnit unit)
        {
            Units = new ConcurrentBag<BaseUnit>(Units.Except(new List<BaseUnit> {unit}));
        }
    }
}