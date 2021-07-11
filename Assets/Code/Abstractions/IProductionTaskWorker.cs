using UniRx;


namespace Abstractions
{
    public interface IProductionTaskWorker
    {
        IReadOnlyReactiveCollection<IProductionTask> ProductionQueue { get; }
    }
}