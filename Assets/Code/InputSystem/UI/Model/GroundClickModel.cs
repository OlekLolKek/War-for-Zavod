using System;
using Abstractions;
using UnityEngine;


namespace InputSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(GroundClickModel), menuName = "Strategy/" + nameof(GroundClickModel))]
    public class GroundClickModel : BaseDataModel<Vector3>
    {
    }
}