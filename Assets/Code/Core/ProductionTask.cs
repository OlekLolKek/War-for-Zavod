using System;
using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;


namespace Core
{
    public class ProductionTask : IProductionTask
    {
        public readonly ReactiveProperty<float> _productionTimeLeft = new ReactiveProperty<float>();
        
        public IObservable<int> ProductionTimeLeft => _productionTimeLeft.Select(value => (int) value);
        public int ProductionTime { get; }
        public string UnitName { get; }
        public Sprite UnitIcon { get; }
        public GameObject UnitPrefab { get; }

        public ProductionTask(int productionTime,  string unitName, 
            Sprite unitIcon, GameObject unitPrefab)
        {
            ProductionTime = productionTime;
            _productionTimeLeft.Value = productionTime;
            UnitName = unitName;
            UnitIcon = unitIcon;
            UnitPrefab = unitPrefab;
        }

        public bool IsEnded()
        {
            return _productionTimeLeft.Value <= 0.0f;
        }

        public void Tick(float deltaTime)
        {
            _productionTimeLeft.Value -= Math.Min(deltaTime, _productionTimeLeft.Value);
        }

#if DEBUG

        public float GetProductionTime_DEBUG()
        {
            return _productionTimeLeft.Value;
        }
        
#endif
    }
}