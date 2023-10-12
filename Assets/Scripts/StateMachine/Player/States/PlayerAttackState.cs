using Mercenary.HealthSystem;
using Mercenary.Input;
using Mercenary.Audio;
using UnityEngine;
using Mercenary.Animations;
using System;

namespace Mercenary.StateMachine
{
    public class PlayerAttackState : BasePlayerState
    {
        private IHealthSystem enemyToAttack;
        private PlayerAnimationEvents playerAnimationEvents;

        public PlayerAttackState(GameObject characterReference, Transform characterEyes, Animator characterAnimator, IHealthSystem characterHealthSystem, PlayerInputReader inputReader, Rigidbody2D rigidbody, Transform groundCheck, AudioHandler audioHandler) : base(characterReference, characterEyes, characterAnimator, characterHealthSystem, rigidbody, inputReader, groundCheck, audioHandler)
        {
            characterAttackRange = 2.5f;
        }

        public override void OnBegin()
        {
            //Play respective audio 
            playerAnimationEvents = characterReference.GetComponent<PlayerAnimationEvents>();

            playerAnimationEvents.OnPlayerAttack += AttackEnemy;
            playerAnimationEvents.OnPlayerAttackEnd += StopAttacking;

            characterAnimator.SetTrigger("isAttacking");
            playerAudioHandler.PlayAudio("KnifeStab", true);

            base.OnBegin();
        }


        public override void OnTick()
        {
            base.OnTick();

        }
        public override void OnEnd()
        {
            characterAnimator.ResetTrigger("isAttacking");

            base.OnEnd();
        }

        private void AttackEnemy()
        {
            enemyToAttack = TryAttackEnemyInRange();

            Debug.Log(enemyToAttack == null);

            if (enemyToAttack != null)
            {
                //If I can attack an enemy, kill that enemy. 

                enemyToAttack.Kill();
                Debug.Log("Kill enemy");
                enemyToAttack = null;
            }
        }


        private void StopAttacking()
        {
               SwitchState(new PlayerIdleState(characterReference, characterEyes, characterAnimator, characterHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck, playerAudioHandler));

        }
    }

}
