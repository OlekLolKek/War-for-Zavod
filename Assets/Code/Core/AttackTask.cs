using Abstractions;
using UnityEngine.AI;


namespace Core
{
    public class AttackTask : IAttackTask
    {
        public ISelectableItem Target { get; }

        private readonly NavMeshAgent _navMeshAgent;
        private readonly float _remainingDistanceThreshold;

        public AttackTask(ISelectableItem target, 
            NavMeshAgent navMeshAgent, float remainingDistanceThreshold)
        {
            Target = target;
            _navMeshAgent = navMeshAgent;
            _remainingDistanceThreshold = remainingDistanceThreshold;
        }

        public bool IsEnded()
        {
            return _navMeshAgent.remainingDistance <= _remainingDistanceThreshold;
        }
    }
}