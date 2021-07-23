using UniRx;


namespace Abstractions
{
    public interface IAttackTaskWorker
    {
        IReadOnlyReactiveCollection<IAttackTask> AttackQueue { get; }
    }
}