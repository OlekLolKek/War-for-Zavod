using Abstractions;
using UnityEngine;


namespace DefaultNamespace.CommandExecutors
{
    public class AttackCommandExecutor : BaseCommandExecutor<IAttackCommand>
    {
        protected override void ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log("Unit attacked");
        }
    }
}