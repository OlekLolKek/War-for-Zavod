using System;
using JetBrains.Annotations;
using UnityEngine;
using Utils;
using Zenject;


namespace Abstractions
{
    public class CommandFactory<T> : PlaceholderFactory<T> where T : ICommand
    {
    }

    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [UsedImplicitly] [InjectAsset("TestUnitPrefab")] private GameObject _unitPrefab;
        [Inject (Id = "TestUnitProductionTime")] public int ProductionTime { get; }
        [Inject (Id = "TestUnitName")] public string UnitName { get; }
        public Sprite UnitIcon { get; }
        public GameObject UnitPrefab => _unitPrefab;
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
        public AttackCommand(IAttackable value)
        {
            Target = value;
        }

        public IAttackable Target { get; }
    }
    
    public class PatrolCommand : IPatrolCommand
    {
        public Vector3 From { get; }
        public Vector3 To { get; }

        public PatrolCommand(Vector3 from, Vector3 to)
        {
            From = from;
            To = to;
        }
    }

    public class StopCommand : IStopCommand
    {
    }
}