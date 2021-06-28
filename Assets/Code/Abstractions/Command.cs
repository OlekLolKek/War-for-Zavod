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

    public class ProduceUnitCommandHeir : ProduceUnitCommand
    {
    }
    
    public class ProduceUnitCommandTestHeir : ProduceUnitCommandHeir
    {
    }

    public class MoveCommand : IMoveCommand
    {
        public Vector3 To { get; }

        public MoveCommand(Vector3 to)
        {
            To = to;
        }
    }
    
    public class AttackCommand : IAttackCommand
    {
        public GameObject Target { get; }
    }
    
    public class PatrolCommand : IPatrolCommand
    {
        public Vector3 From { get; }
        public Vector3 To { get; }
    }

    public class StopCommand : IStopCommand
    {
    }
}