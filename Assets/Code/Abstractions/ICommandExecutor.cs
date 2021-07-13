using UnityEngine;


namespace Abstractions
{
    public interface ICommandExecutor
    {
        void Execute(ICommand command);
    }
    
    public abstract class BaseCommandExecutor<T> : MonoBehaviour, ICommandExecutor where T : ICommand
    {
        public void Execute(ICommand command)
        {
            ExecuteSpecificCommand((T)command);
        }

        protected abstract void ExecuteSpecificCommand(T command);
    }
}