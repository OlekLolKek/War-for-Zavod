using Abstractions;
using UnityEngine;


namespace DefaultNamespace.CommandExecutors
{
    public class AttackCommandExecutor : BaseCommandExecutor<IAttackCommand>
    {
        protected override void ExecuteSpecificCommand(IAttackCommand command)
        {
            if (command.Target != null)
            {
                Debug.Log($"{command.Target} attacked.");
            }
            else
            {
                Debug.Log("You didn't select an enemy.");
            }
        }
    }
}