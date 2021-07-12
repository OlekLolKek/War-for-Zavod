using System;
using Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.AI;


namespace Core
{
    public class MoveTask : IMoveTask
    {
        public Vector3 MovementPoint { get; }

        private readonly NavMeshAgent _navMeshAgent;
        private readonly float _remainingDistanceThreshold;

        public MoveTask(Vector3 movementPoint, NavMeshAgent navMeshAgent,
            float remainingDistanceThreshold)
        {
            MovementPoint = movementPoint;
            _navMeshAgent = navMeshAgent;
            _remainingDistanceThreshold = remainingDistanceThreshold;
            Debug.Log("Move task created");
        }

        public bool IsEnded()
        {
            Debug.Log($"{_navMeshAgent.remainingDistance <= _remainingDistanceThreshold}");
            return _navMeshAgent.remainingDistance <= _remainingDistanceThreshold;
        }
    }
}