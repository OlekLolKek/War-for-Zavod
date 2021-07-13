using UnityEngine;


namespace Abstractions
{
    public interface ICommand
    {
        
    }
    

    public interface IMoveCommand : ICommand
    {
        Vector3 To { get; }
    }

    public interface IAttackCommand : ICommand
    {
        ISelectableItem Target { get; }
    }

    public interface IPatrolCommand : ICommand
    {
        Vector3 From { get; }
        Vector3 To { get; }
    }

    public interface IStopCommand : ICommand
    {
    }
    

    public interface IProduceUnitCommand : ICommand
    {
        GameObject UnitPrefab { get; }
    }
}