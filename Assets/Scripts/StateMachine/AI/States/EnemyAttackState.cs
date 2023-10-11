using Mercenary.HealthSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.StateMachine
{
    public class EnemyAttackState : BaseEnemyState
    {
        private bool isAttacking = false;
        private bool hasSubscribed = false;
        public EnemyAttackState(GameObject _characterReference, Transform _characterEyes, Animator _characterAnimator, IHealthSystem _characterHealthSystem, Transform _waypoint1, Transform _waypoint2) : base(_characterReference, _characterEyes, _characterAnimator, _characterHealthSystem, _waypoint1, _waypoint2)
        {
            characterState = EnemyStates.ATTACK;   
        }

        public override void OnBegin()
        {
<<<<<<< HEAD
<<<<<<< HEAD
            EnemyAnimationEvents.OnEnemyAttack += AttackPlayer;

            characterAnimator.SetTrigger("isAttacking");
            AttackPlayer();
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

=======
            characterAnimator.SetTrigger("isAttacking");
            base.OnBegin();
        }
>>>>>>> parent of ebb1ef1 (Bug fixes and animation event handlers)
=======
            characterAnimator.SetTrigger("isAttacking");
            base.OnBegin();
        }
>>>>>>> parent of ebb1ef1 (Bug fixes and animation event handlers)
        public override void OnTick()
        {
            base.OnTick();

<<<<<<< HEAD
<<<<<<< HEAD
=======
=======
>>>>>>> parent of ebb1ef1 (Bug fixes and animation event handlers)
            IHealthSystem attackedEnemy = TryAttackEnemyInRange();
            
            if (attackedEnemy != null && !isAttacking)
            {
                //Time.timeScale = .25f;
                
                attackedEnemy.Kill();
            }
>>>>>>> parent of ebb1ef1 (Bug fixes and animation event handlers)
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
