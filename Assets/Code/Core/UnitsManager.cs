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
        public ConcurrentBag<MainUnit> Units { get; private set; }
        
        public static UnitsManager Instance { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Units = new ConcurrentBag<MainUnit>();
        }

        public void RegisterUnit(MainUnit unit)
        {
            Units.Add(unit);
        }

        public void UnregisterUnit(MainUnit unit)
        {
            Units = new ConcurrentBag<MainUnit>(Units.Except(new List<MainUnit> {unit}));
        }
    }
}