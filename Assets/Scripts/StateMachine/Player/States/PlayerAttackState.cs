using Mercenary.HealthSystem;
using Mercenary.Input;
using Mercenary.Audio;
using UnityEngine;

namespace Mercenary.StateMachine
{
    public class PlayerAttackState : BasePlayerState
    {
        private IHealthSystem enemyToAttack;
        public PlayerAttackState(GameObject characterReference, Transform characterEyes, Animator characterAnimator, IHealthSystem characterHealthSystem, PlayerInputReader inputReader, Rigidbody2D rigidbody, Transform groundCheck, AudioHandler audioHandler) : base(characterReference, characterEyes, characterAnimator, characterHealthSystem, rigidbody, inputReader, groundCheck, audioHandler)
        {
            characterAttackRange = 2.5f;
        }

        public override void OnBegin()
        {
            if (!CanAttack()) return;

            playerAudioHandler.PlayAudio("KnifeStab", true);

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
               SwitchState(new PlayerIdleState(characterReference, characterEyes, characterAnimator, characterHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck, playerAudioHandler));
            }
        }
        public override void OnEnd()
        {
            characterAnimator.ResetTrigger("isAttacking");

            base.OnEnd();
        }
    }

}
