using System;
using UnityEngine;


namespace Abstractions
{
    public interface IMoveTask
    {
        Vector3 MovementPoint { get; }
    }
}