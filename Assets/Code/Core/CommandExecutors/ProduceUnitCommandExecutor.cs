using System;
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

        [Inject] private CoinModel _coinModel;
        private ISelectableItem _thisSelectableItem;

        private void Awake()
        {
            _thisSelectableItem = GetComponent<ISelectableItem>();
        }

        protected override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            if (_coinModel.TryBuyItem(command.UnitPrice))
            {
                var newTask =
                    new ProductionTask(command.ProductionTime, command.UnitName,
                        command.UnitIcon, command.UnitPrefab);
                _productionQueue.Add(newTask);
            }
        }

        private void CreateUnit(ProductionTask task)
        {
            _productionQueue.Remove(task);
            var unit = Instantiate(task.UnitPrefab, 
                transform.position + Vector3.forward * 2, 
                Quaternion.identity);
            unit.TryGetComponent<NavMeshAgent>(out var navMeshAgent);
            navMeshAgent.SetDestination(_assemblyPoint.position);
            unit.TryGetComponent<ISelectableItem>(out var unitSelectable);
            //TODO: add fraction setting when unit is created
            // ok
            //unitSelectable.SetFraction(_thisSelectableItem.Fraction);
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