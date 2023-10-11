using Mercenary.Animations;
using Mercenary.HealthSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.StateMachine
{
    public class EnemyAttackState : BaseEnemyState
    {
        private bool isAttacking = false;
        public EnemyAttackState(GameObject _characterReference, Transform _characterEyes, Animator _characterAnimator, IHealthSystem _characterHealthSystem, Transform _waypoint1, Transform _waypoint2) : base(_characterReference, _characterEyes, _characterAnimator, _characterHealthSystem, _waypoint1, _waypoint2)
        {
            characterState = EnemyStates.ATTACK;   
        }

        public override void OnBegin()
        {
            characterAnimator.SetTrigger("isAttacking");

            EnemyAnimationEvents.OnEnemyAttack += AttackPlayer;

            base.OnBegin();
        }

        private void AttackPlayer()
        {
            IHealthSystem attackedEnemy = TryAttackEnemyInRange();

            if (attackedEnemy != null && !isAttacking)
            {
                Debug.Log("Attacked Enemy: " + attackedEnemy);
    

                    attackedEnemy.Kill();
            }
        }

        public override void OnTick()
        {
            base.OnTick();

            AnimatorStateInfo enemyAnimInfo = characterAnimator.GetCurrentAnimatorStateInfo(0);
            if (enemyAnimInfo.IsName("AttackSkeleton") && enemyAnimInfo.normalizedTime >= 1.0f)
            {
                //If animation switched then change to idle state.

                SwitchState(new EnemyPatrolState(characterReference, characterEyes, characterAnimator, characterHealthSystem, waypoint1, waypoint2));
            }
        }

        public override void OnEnd()
        {
            Debug.Log("Has finished attacking");
            characterAnimator.ResetTrigger("isAttacking");
            EnemyAnimationEvents.OnEnemyAttack -= AttackPlayer;

            isAttacking = false;
            base.OnEnd();
        }


        protected override void OnTargetReached()
        {
            base.OnTargetReached();

        }
    }
}
