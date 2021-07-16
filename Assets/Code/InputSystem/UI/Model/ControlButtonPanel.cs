using Abstractions;
using JetBrains.Annotations;
using UnityEngine;
using Utils;
using Zenject;


namespace InputSystem.UI.Model
{
    [UsedImplicitly]
    public class ControlButtonPanel
    {
        [Inject] private CommandCreator<IProduceUnitCommand> _produceUnitCommandCreator;
        [Inject] private CommandCreator<IAttackCommand> _attackCommandCreator;
        [Inject] private CommandCreator<IMoveCommand> _moveUnitCommandCreator;
        [Inject] private CommandCreator<IPatrolCommand> _patrolCommandCreator;
        [Inject] private CommandCreator<IStopCommand> _stopCommandCreator;

        private bool _isPending;

        public void HandleClick(ICommandExecutor executor)
        {
            _isPending = true;
            
            _produceUnitCommandCreator.CreateCommand(executor, command => ExecuteCommand(executor, command));
            _attackCommandCreator.CreateCommand(executor, command => ExecuteCommand(executor, command));
            _moveUnitCommandCreator.CreateCommand(executor, command => ExecuteCommand(executor, command));
            _patrolCommandCreator.CreateCommand(executor, command => ExecuteCommand(executor, command));
            _stopCommandCreator.CreateCommand(executor, command => ExecuteCommand(executor, command));
        }

        private void ExecuteCommand(ICommandExecutor executor, ICommand command)
        {
            _isPending = false;
            executor.Execute(command);
        }

        public void HandleSelectionChanged()
        {
            if (!_isPending)
            {
                return;
            }
            
            _isPending = false;
            
            _produceUnitCommandCreator.CancelCommand();
            //_attackCommandCreator.CancelCommand();
            _moveUnitCommandCreator.CancelCommand();
            _patrolCommandCreator.CancelCommand();
            _stopCommandCreator.CancelCommand();
        }
    }
}