using System;
using UnityEngine;


namespace Abstractions
{
    public interface IProductionTask
    {
        IObservable<int> ProductionTimeLeft { get; }
        int ProductionTime { get; }
        string UnitName { get; }
        Sprite UnitIcon { get; }
        GameObject UnitPrefab { get; }
    }
}