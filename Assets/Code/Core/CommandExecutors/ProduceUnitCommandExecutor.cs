using Abstractions;
using UnityEngine;


namespace DefaultNamespace.CommandExecutors
{
    public class ProduceUnitCommandExecutor : BaseCommandExecutor<IProduceUnitCommand>
    {
        protected override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            Debug.Log("Unit produced");
        }
    }
}