using System;
using Abstractions;
using UnityEngine;


namespace InputSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(SelectedItemModel), menuName = "Strategy/Models")]
    public class SelectedItemModel : BaseDataModel<ISelectableItem>
    {
    }
}