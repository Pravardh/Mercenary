using Mercenary.HealthSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Mercenary.StateMachine
{
    public class EnemyPatrolState : BaseEnemyState
    {

        public EnemyPatrolState(GameObject _characterReference, Transform _characterEyes, Animator _characterAnimator, IHealthSystem _characterHealthSystem, Transform _waypoint1, Transform _waypoint2) : base(_characterReference, _characterEyes, _characterAnimator, _characterHealthSystem, _waypoint1, _waypoint2)
        {
            characterState = EnemyStates.PATROL;

        }

        public override void OnBegin()
        {
            SetMoveToTarget(waypoint1);
            characterAnimator.SetTrigger("isPatroling");

            base.OnBegin();
        }

        public override void OnTick()
        {
            base.OnTick();


            if (TryDetectEnemyInRange() == null)
            {
                TryMoveToTarget();
            }
            else
            {
                SwitchState(new EnemyChaseState(characterReference, characterEyes, characterAnimator, characterHealthSystem, waypoint1, waypoint2));

            }
        }

        public override void OnEnd()
        {
            characterAnimator.ResetTrigger("isPatroling");

            base.OnEnd();
        }

        protected override void OnTargetReached()
        {
            if (moveToTarget == waypoint1)
            {
                moveToTarget = waypoint2;
            }
            else
            {
                moveToTarget = waypoint1;
            }
        }
    }
}
