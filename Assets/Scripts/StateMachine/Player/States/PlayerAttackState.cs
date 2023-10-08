using Mercenary.HealthSystem;
using Mercenary.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.StateMachine
{
    public class PlayerAttackState : BasePlayerState
    {
        private IHealthSystem enemyToAttack;
        public PlayerAttackState(GameObject characterReference, Transform characterEyes, Animator characterAnimator, IHealthSystem characterHealthSystem, PlayerInputReader inputReader, Rigidbody2D rigidbody, Transform groundCheck) : base(characterReference, characterEyes, characterAnimator, characterHealthSystem, rigidbody, inputReader, groundCheck)
        {
            characterAttackRange = 2.5f;
        }

        public override void OnBegin()
        {
            if (!CanAttack()) return;

            playerCurrentAttackAmount += 1;
            Debug.Log($"{CanAttack()} : {playerCurrentAttackAmount}");
            characterAnimator.SetTrigger("isAttacking");
            Debug.Log("Is attacking;");
            base.OnBegin();
        }

        public override void OnTick()
        {
            base.OnTick();

            enemyToAttack = TryAttackEnemyInRange();

            if (enemyToAttack != null )
            {
                enemyToAttack.Kill();
                enemyToAttack = null;
            }

            AnimatorStateInfo playerAnimInfo = characterAnimator.GetCurrentAnimatorStateInfo(0);
            if (playerAnimInfo.IsName("Attack") && playerAnimInfo.normalizedTime >= 1.0f)
            {
               Debug.Log("Idling now");
               SwitchState(new PlayerIdleState(characterReference, characterEyes, characterAnimator, characterHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck));
            }
        }
        public override void OnEnd()
        {
            characterAnimator.ResetTrigger("isAttacking");

            base.OnEnd();
        }
    }

}
