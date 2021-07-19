using System;
using System.Threading;
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
            if (executor is BaseCommandExecutor<T>)
            {
                CreateSpecificCommand(onCreated);
            }
        }

        protected abstract void CreateSpecificCommand(Action<T> onCreated);
        public abstract void CancelCommand();
    }

    public abstract class CancelableCommandCreator<T, TParam> : CommandCreator<T> where T : ICommand
    {
        private CancellationTokenSource _tokenSource;
        [Inject] private IAwaitable<TParam> _param;

        protected override async void CreateSpecificCommand(Action<T> onCreated)
        {
            _tokenSource = new CancellationTokenSource();
            try
            {
                var paramResult = await _param.AsTask().WithCancellation(_tokenSource.Token);
                onCreated?.Invoke(GetCommand(paramResult));
            }
            catch (OperationCanceledException e)
            {
                Debug.Log("Operation Cancelled");
            }
        }

        protected abstract T GetCommand(TParam param);

        public override void CancelCommand()
        {
            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
                _tokenSource.Dispose();
                _tokenSource = null;
            }
        }
    }

    public class ProduceUnitCommandCreator : CommandCreator<IProduceUnitCommand>
    {
        [Inject] private AssetsStorage _assetsStorage;
        //TODO: remove
        //[Inject] private DiContainer _container;
        [Inject] private CommandFactory<IProduceUnitCommand> _factory;

        protected override void CreateSpecificCommand(Action<IProduceUnitCommand> onCreated)
        {
            var command = _factory.Create();
            onCreated?.Invoke(_assetsStorage.Inject(command));
        }

        public override void CancelCommand()
        {
        }
    }

    public class MoveCommandCreator : CancelableCommandCreator<IMoveCommand, Vector3>
    {
        protected override IMoveCommand GetCommand(Vector3 param)
        {
            return new MoveCommand(param);
        }
    }

    public class AttackCommandCreator : CancelableCommandCreator<IAttackCommand, IAttackable>
    {
        protected override IAttackCommand GetCommand(IAttackable param)
        {
            return new AttackCommand(param);
        }
    }

    public class PatrolCommandCreator : CancelableCommandCreator<IPatrolCommand, Vector3>
    {
        [Inject] private SelectedItemModel _selectedItem;
        protected override IPatrolCommand GetCommand(Vector3 param)
        {
            return new PatrolCommand(_selectedItem.Value.SelectionParentTransform.position, param);
        }
    }

    public class StopCommandCreator : CommandCreator<IStopCommand>
    {
        protected override void CreateSpecificCommand(Action<IStopCommand> onCreated)
        {
            onCreated?.Invoke(new StopCommand());
        }

        public override void CancelCommand()
        {
        }
    }
}