using Abstractions;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;


namespace InputSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(AttackableTargetModel), menuName = "Strategy/" + nameof(AttackableTargetModel))]
    public class AttackableTargetModel : BaseDataModel<IAttackable>
    {
        
    }
}