using System;
using Abstractions;
using UniRx;
using UniRx.Diagnostics;


namespace InputSystem.UI.Model
{
    public class ProductionPanel
    {
        public IReadOnlyReactiveCollection<IProductionTask> ProductionLine = new ReactiveCollection<IProductionTask>();
        
        public IObservable<CollectionAddEvent<IProductionTask>> AddUnit => ProductionLine.ObserveAdd();
        public IObservable<CollectionRemoveEvent<IProductionTask>> RemoveUnit => ProductionLine.ObserveRemove();
        public IObservable<Unit> ClearLine => ProductionLine.ObserveReset();

        public void SetWorker(IProductionTaskWorker productionTaskWorker)
        {
            ProductionLine = productionTaskWorker.ProductionQueue;
        }
    }
}