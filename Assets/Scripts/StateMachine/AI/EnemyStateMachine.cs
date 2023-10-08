using Mercenary.HealthSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Mercenary.StateMachine
{
    public class EnemyStateMachine : BaseStateMachine
    {

        //DataContainer as well

        private BaseEnemyState currentState;

        [SerializeField]
        private Transform waypoint1;

        [SerializeField]
        private Transform waypoint2;
        void Start()
        {
            //Sets basic references like Animator, IHealthSystem etc
            InitBase();

            //Adding components unique to this state machine

            currentState = new EnemyPatrolState(gameObject, characterEyes, characterAnimator, characterHealthSystem, waypoint1, waypoint2);
        }

        void Update()
        {
            currentState = (BaseEnemyState)currentState?.Execute();
        }
    }
}
