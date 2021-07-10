using System.Linq;
using Abstractions;
using Core;
using UniRx;
using UnityEngine;
using Zenject;


namespace DefaultNamespace.CommandExecutors
{
    public class ProduceUnitCommandExecutor : BaseCommandExecutor<IProduceUnitCommand>, ITickable
    {
        private ReactiveCollection<ProductionTask> _productionQueue = new ReactiveCollection<ProductionTask>();
        
        protected override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            Debug.Log("Unit produced");
            var newTask = new ProductionTask(command.ProductionTime, command.UnitName, command.UnitIcon, command.UnitPrefab);
            Debug.Log(newTask.ProductionTime);
            _productionQueue.Add(newTask);
        }

        private void CreateUnit(ProductionTask task)
        {
            _productionQueue.RemoveAt(0);
            Instantiate(task.UnitPrefab, transform.position + Vector3.forward * 2, Quaternion.identity);
        }

        public void Tick()
        {
            if (_productionQueue.Count == 0)
            {
                return;
            }

            var currentTask = _productionQueue.First();

            if (currentTask.IsEnded())
            {
                CreateUnit(currentTask);
            }
            else
            {
                currentTask.Tick(Time.deltaTime);
                Debug.Log(currentTask.GetProductionTime_DEBUG());
            }
        }
    }
}