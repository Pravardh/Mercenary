using Mercenary.HealthSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.StateMachine
{
    public class EnemyChaseState : BaseEnemyState
    {
        public EnemyChaseState(GameObject _characterReference, Transform _characterEyes, Animator _characterAnimator, IHealthSystem _characterHealthSystem, Transform _waypoint1, Transform _waypoint2) : base(_characterReference, _characterEyes, _characterAnimator, _characterHealthSystem, _waypoint1, _waypoint2)
        {
            characterState = EnemyStates.CHASE;
        }

        public override void OnBegin()
        {
            Debug.Log("Chasing begin");
            characterAnimator.SetTrigger("isChasing");


            base.OnBegin();
        }

        public override void OnTick()
        {
            base.OnTick();

            if (TryDetectEnemyInRange() == null)
            {
                nextState = new EnemyPatrolState(characterReference, characterEyes, characterAnimator, characterHealthSystem, waypoint1, waypoint2);
                currentStage = Stage.END;
            }
            else
            {
                Transform detectedEnemy = TryDetectEnemyInRange().transform;

                if (detectedEnemy != null)
                {
                    SetMoveToTarget(detectedEnemy);
                }

                TryMoveToTarget(3);
            }
        }

        public override void OnEnd()
        {
            characterAnimator.ResetTrigger("isChasing");

            base.OnEnd();
        }

        protected override void OnTargetReached()
        {
            SwitchState(new EnemyAttackState(characterReference, characterEyes, characterAnimator, characterHealthSystem, waypoint1, waypoint2));
        }
    }

}
