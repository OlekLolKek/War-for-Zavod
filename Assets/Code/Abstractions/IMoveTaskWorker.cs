using UniRx;


namespace Abstractions
{
    public interface IMoveTaskWorker
    {
        IReadOnlyReactiveCollection<IMoveTask> MovementQueue { get; }
    }
}