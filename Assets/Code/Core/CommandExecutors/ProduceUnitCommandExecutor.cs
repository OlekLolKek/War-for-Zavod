using Abstractions;
using UnityEngine;


namespace DefaultNamespace.CommandExecutors
{
    public class ProduceUnitCommandExecutor : BaseCommandExecutor<IProduceUnitCommand>
    {
        protected override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            Debug.Log("Unit produced");
            Instantiate(command.UnitPrefab, transform.position + Vector3.forward * 2, Quaternion.identity);
        }
    }
}