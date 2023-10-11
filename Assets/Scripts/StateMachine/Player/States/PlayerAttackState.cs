using Mercenary.HealthSystem;
using Mercenary.Input;
using Mercenary.Audio;
using UnityEngine;
using Mercenary.Animations;

namespace Mercenary.StateMachine
{
    public class PlayerAttackState : BasePlayerState
    {
        private IHealthSystem enemyToAttack;

        private bool hasSetAnimationEvent = false;
        public PlayerAttackState(GameObject characterReference, Transform characterEyes, Animator characterAnimator, IHealthSystem characterHealthSystem, PlayerInputReader inputReader, Rigidbody2D rigidbody, Transform groundCheck, AudioHandler audioHandler) : base(characterReference, characterEyes, characterAnimator, characterHealthSystem, rigidbody, inputReader, groundCheck, audioHandler)
        {
            characterAttackRange = 2.5f;
        }

        public override void OnBegin()
        {
            //Play respective audio 

            playerAudioHandler.PlayAudio("KnifeStab", true);

            characterAnimator.SetTrigger("isAttacking");

            PlayerAnimationEvents.OnPlayerAttack += AttackEnemy;

            base.OnBegin();
        }

        public override void OnTick()
        {
            base.OnTick();


            AnimatorStateInfo playerAnimInfo = characterAnimator.GetCurrentAnimatorStateInfo(0);
            if (playerAnimInfo.IsName("Attack") && playerAnimInfo.normalizedTime >= 1.0f)
            {
                //If animation switched then change to idle state.

               SwitchState(new PlayerIdleState(characterReference, characterEyes, characterAnimator, characterHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck, playerAudioHandler));
            }
        }

        public void AttackEnemy()
        {
            enemyToAttack = TryAttackEnemyInRange();
            if (enemyToAttack != null)
            {
                //If I can attack an enemy, kill that enemy. 

                enemyToAttack.Kill();
                enemyToAttack = null;
            }

            Debug.Log("Attacking ");
        }

        public override void OnEnd()
        {
            characterAnimator.ResetTrigger("isAttacking");
            PlayerAnimationEvents.OnPlayerAttack -= AttackEnemy;

            base.OnEnd();
        }
    }

}
