using System;
using Abstractions;
using UnityEngine;
using Utils;
using Zenject;


namespace InputSystem.UI.Model
{
    public abstract class CommandCreator<T> where T : ICommand
    {
        public void CreateCommand(ICommandExecutor executor, Action<T> onCreated)
        {
            if (executor as BaseCommandExecutor<T>)
            {
                CreateSpecificCommand(onCreated);
            }
        }

        protected abstract void CreateSpecificCommand(Action<T> onCreated);
    }

    public class ProduceUnitCommandCreator : CommandCreator<IProduceUnitCommand>
    {
        [Inject] private AssetsStorage _assetsStorage;
        
        protected override void CreateSpecificCommand(Action<IProduceUnitCommand> onCreated)
        {
            onCreated?.Invoke(_assetsStorage.Inject(new ProduceUnitCommand()));
        }
    }
    
    public class MoveCommandCreator : CommandCreator<IMoveCommand>
    {
        private Action<IMoveCommand> _onCreated;
        private readonly GroundClickModel _currentGroundClick;

        [Inject]
        public MoveCommandCreator(GroundClickModel currentGroundClick)
        {
            _currentGroundClick = currentGroundClick;
            _currentGroundClick.OnUpdated += HandleGroundClick;
        }

        private void HandleGroundClick()
        {
            _onCreated?.Invoke(new MoveCommand(_currentGroundClick.Value));
        }
        
        protected override void CreateSpecificCommand(Action<IMoveCommand> onCreated)
        {
            _onCreated = onCreated;
        }
    }
    
    public class AttackCommandCreator : CommandCreator<IAttackCommand>
    {
        private Action<IAttackCommand> _onCreated;
        private readonly SelectedItemModel _currentSelectedItem;

        [Inject]
        public AttackCommandCreator(SelectedItemModel currentSelectedItem)
        {
            _currentSelectedItem = currentSelectedItem;
            _currentSelectedItem.OnUpdated += HandleItemSelection;
        }

        private void HandleItemSelection()
        {
            _onCreated?.Invoke(new AttackCommand(_currentSelectedItem.Value));
        }
        
        protected override void CreateSpecificCommand(Action<IAttackCommand> onCreated)
        {
            _onCreated = onCreated;
        }
    }
    
    public class PatrolCommandCreator : CommandCreator<IPatrolCommand>
    {
        private Action<IPatrolCommand> _onCreated;
        private readonly GroundClickModel _currentGroundClick;
        private Vector3 _firstClick;
        private Vector3 _secondClick;

        [Inject]
        public PatrolCommandCreator(GroundClickModel currentGroundClick)
        {
            _currentGroundClick = currentGroundClick;
            _currentGroundClick.OnUpdated += HandleGroundClick;
        }

        private void HandleGroundClick()
        {
            if (_firstClick == Vector3.zero)
            {
                _firstClick = _currentGroundClick.Value;
            }
            else if (_secondClick == Vector3.zero)
            {
                _secondClick = _currentGroundClick.Value;
                _onCreated?.Invoke(new PatrolCommand(_firstClick, _secondClick));
                _firstClick = Vector3.zero;
                _secondClick = Vector3.zero;
            }
        }
        
        protected override void CreateSpecificCommand(Action<IPatrolCommand> onCreated)
        {
            _onCreated = onCreated;
        }
    }
    
    public class StopCommandCreator : CommandCreator<IStopCommand>
    {
        protected override void CreateSpecificCommand(Action<IStopCommand> onCreated)
        {
            onCreated?.Invoke(new StopCommand());
        }
    }
}