using System.Linq;
using Abstractions;
using Core;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;


namespace DefaultNamespace.CommandExecutors
{
    public class ProduceUnitCommandExecutor : BaseCommandExecutor<IProduceUnitCommand>,
        ITickable, IProductionTaskWorker
    {
        public IReadOnlyReactiveCollection<IProductionTask> ProductionQueue => 
            _productionQueue;
        
        private readonly ReactiveCollection<IProductionTask> _productionQueue =
            new ReactiveCollection<IProductionTask>();

        [SerializeField] private Transform _assemblyPoint;

        protected override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            var newTask =
                new ProductionTask(command.ProductionTime, command.UnitName,
                    command.UnitIcon, command.UnitPrefab);
            _productionQueue.Add(newTask);
        }

        private void CreateUnit(ProductionTask task)
        {
            _productionQueue.Remove(task);
            var unit = Instantiate(task.UnitPrefab, 
                transform.position + Vector3.forward * 2, 
                Quaternion.identity);
            unit.TryGetComponent<NavMeshAgent>(out var navMeshAgent);
            navMeshAgent.SetDestination(_assemblyPoint.position);
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