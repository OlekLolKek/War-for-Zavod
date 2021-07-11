using System.Linq;
using Abstractions;
using Core;
using UniRx;
using UnityEngine;
using Zenject;


namespace DefaultNamespace.CommandExecutors
{
    public class ProduceUnitCommandExecutor : BaseCommandExecutor<IProduceUnitCommand>, ITickable, IProductionTaskWorker
    {
        public IReadOnlyReactiveCollection<IProductionTask> ProductionQueue => _productionQueue;
        private readonly ReactiveCollection<IProductionTask> _productionQueue = new ReactiveCollection<IProductionTask>();
        
        protected override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            Debug.Log("Unit produced");
            var newTask = new ProductionTask(command.ProductionTime, command.UnitName, command.UnitIcon, command.UnitPrefab);
            _productionQueue.Add(newTask);
        }

        private void CreateUnit(ProductionTask task)
        {
            _productionQueue.Remove(task);
            Instantiate(task.UnitPrefab, transform.position + Vector3.forward * 2, Quaternion.identity);
        }

        public void Tick()
        {
            if (_productionQueue.Count == 0)
            {
                return;
            }

            var currentTask = (ProductionTask)ProductionQueue.First();

            if (currentTask.IsEnded())
            {
                CreateUnit(currentTask);
            }
            else
            {
                currentTask.Tick(Time.deltaTime);
            }
        }
    }
}