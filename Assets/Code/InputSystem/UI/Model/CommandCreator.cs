using System;
using Abstractions;
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
        private GroundClickModel _currentGroundClick;

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
        protected override void CreateSpecificCommand(Action<IAttackCommand> onCreated)
        {
        }
    }
    
    public class PatrolCommandCreator : CommandCreator<IPatrolCommand>
    {
        protected override void CreateSpecificCommand(Action<IPatrolCommand> onCreated)
        {
        }
    }
    
    public class StopCommandCreator : CommandCreator<IStopCommand>
    {
        protected override void CreateSpecificCommand(Action<IStopCommand> onCreated)
        {
            
        }
    }
}