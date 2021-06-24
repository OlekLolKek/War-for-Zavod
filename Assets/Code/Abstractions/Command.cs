using JetBrains.Annotations;
using UnityEngine;
using Utils;


namespace Abstractions
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [UsedImplicitly] [InjectAsset("TestUnitPrefab")] private GameObject _unitPrefab;
        public GameObject UnitPrefab => _unitPrefab;
    }
}