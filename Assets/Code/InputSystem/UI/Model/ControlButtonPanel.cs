using Abstractions;
using UnityEngine;
using Utils;
using Zenject;


namespace InputSystem.UI.Model
{
    public class ControlButtonPanel
    {
        [Inject] private CommandCreator<IProduceUnitCommand> _produceUnitCommandCreator;
        [Inject] private CommandCreator<IAttackCommand> _moveCommandCreator;
        [Inject] private CommandCreator<IMoveCommand> _moveUnitCommandCreator;
        [Inject] private CommandCreator<IPatrolCommand> _patrolCommandCreator;
        [Inject] private CommandCreator<IStopCommand> _stopCommandCreator;

        public void HandleClick(ICommandExecutor executor)
        {
            _produceUnitCommandCreator.CreateCommand(executor, executor.Execute);
            _moveCommandCreator.CreateCommand(executor, executor.Execute);
            _moveUnitCommandCreator.CreateCommand(executor, executor.Execute);
            _patrolCommandCreator.CreateCommand(executor, executor.Execute);
            _stopCommandCreator.CreateCommand(executor, executor.Execute);
        }
    }
}