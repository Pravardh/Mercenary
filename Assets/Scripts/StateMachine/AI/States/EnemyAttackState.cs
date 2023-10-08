using Mercenary.HealthSystem;
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
            base.OnBegin();
        }
        public override void OnTick()
        {
            base.OnTick();

            IHealthSystem attackedEnemy = TryAttackEnemyInRange();
            
            if (attackedEnemy != null && !isAttacking)
            {
                //Time.timeScale = .25f;
                
                attackedEnemy.Kill();
            }
        }

        public override void OnEnd()
        {
            Debug.Log("Has finished attacking");
            characterAnimator.ResetTrigger("isAttacking");
            isAttacking = false;
            base.OnEnd();
        }


        protected override void OnTargetReached()
        {
            base.OnTargetReached();

        }
    }
}
