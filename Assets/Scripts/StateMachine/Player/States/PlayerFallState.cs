using Mercenary.HealthSystem;
using Mercenary.Input;
using Mercenary.Audio;
using UnityEngine;

namespace Mercenary.StateMachine
{
    public class PlayerFallState : BasePlayerState
    {
        public PlayerFallState(GameObject characterReference, Transform characterEyes, Animator characterAnimator, IHealthSystem characterHealthSystem, PlayerInputReader inputReader, Rigidbody2D rigidbody, Transform groundCheck, AudioHandler audioHandler) : base(characterReference, characterEyes, characterAnimator, characterHealthSystem, rigidbody, inputReader, groundCheck, audioHandler)
        {

        }

        public override void OnBegin()
        {
            characterAnimator.SetTrigger("isFalling");
            base.OnBegin();
        }

        public override void OnTick()
        {
            base.OnTick();
            playerRigidbody.velocity = new Vector2(playerMovementValue * 7f, playerRigidbody.velocity.y);

            if (IsGrounded())
            {
                SwitchState(new PlayerIdleState(characterReference, characterEyes, characterAnimator, characterHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck, playerAudioHandler));
            }


        }
        public override void OnEnd()
        {
            characterAnimator.ResetTrigger("isFalling");
            base.OnEnd();
        }

        private void TryJump()
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerJumpMagnitude);
        }
    }

}
