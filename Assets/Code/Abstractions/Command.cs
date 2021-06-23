using UnityEngine;


namespace Abstractions
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        public GameObject UnitPrefab { get; }

        public ProduceUnitCommand(GameObject unitPrefab)
        {
            UnitPrefab = unitPrefab;
        }
    }
}