using Abstractions;
using UnityEngine;
using Utils;
using Zenject;


namespace InputSystem.UI.Model
{
    public class ControlButtonPanel
    {
        private readonly AssetsStorage _assetsStorage;

        [Inject]
        public ControlButtonPanel(AssetsStorage assetsStorage)
        {
            _assetsStorage = assetsStorage;
        }

        public void HandleClick(ICommandExecutor executor)
        {
            if (executor is BaseCommandExecutor<IProduceUnitCommand> produceUnitExecutor)
            {
                produceUnitExecutor.Execute(_assetsStorage.Inject(new ProduceUnitCommandTestHeir()));
            }
            else if (executor is BaseCommandExecutor<IMoveCommand> moveExecutor)
            {
                moveExecutor.Execute(_assetsStorage.Inject(new MoveCommand()));
            }
            else if (executor is BaseCommandExecutor<IAttackCommand> attackExecutor)
            {
                attackExecutor.Execute(_assetsStorage.Inject(new AttackCommand()));
            }
            else if (executor is BaseCommandExecutor<IPatrolCommand> patrolExecutor)
            {
                patrolExecutor.Execute(_assetsStorage.Inject(new PatrolCommand()));
            }
            else if (executor is BaseCommandExecutor<IStopCommand> stopExecutor)
            {
                stopExecutor.Execute(_assetsStorage.Inject(new StopCommand()));
            }
            else
            {
                Debug.LogError($"{executor.GetType()} is not supported.");
            }
        }
    }
}