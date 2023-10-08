using Mercenary.HealthSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Mercenary.StateMachine
{
    public class EnemyIdleState : BaseEnemyState
    {
        public EnemyIdleState(GameObject _characterReference, Transform _characterEyes, Animator _characterAnimator, IHealthSystem _characterHealthSystem, NavMeshAgent _characterNavMeshAgent, Transform _waypoint1, Transform _waypoint2) : base(_characterReference, _characterEyes, _characterAnimator, _characterHealthSystem, _waypoint1, _waypoint2)
        {

        }

    }

}
