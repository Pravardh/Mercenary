using Mercenary.HealthSystem;
using Mercenary.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.StateMachine
{
    public class PlayerJumpState : BasePlayerState
    {
        public PlayerJumpState(GameObject characterReference, Transform characterEyes, Animator characterAnimator, IHealthSystem characterHealthSystem, PlayerInputReader inputReader, Rigidbody2D rigidbody, Transform groundCheck) : base(characterReference, characterEyes, characterAnimator, characterHealthSystem, rigidbody, inputReader, groundCheck)
        {

        }

        public override void OnBegin()
        {
            characterAnimator.SetTrigger("isJumping");
            TryJump();
            base.OnBegin();
        }

        public override void OnTick()
        {
            base.OnTick();

            if (playerRigidbody.velocity.y < 0)
            {
                SwitchState(new PlayerFallState(characterReference, characterEyes, characterAnimator, characterHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck));
            }

        }
        public override void OnEnd()
        {
            characterAnimator.ResetTrigger("isJumping");
            base.OnEnd();
        }

        private void TryJump()
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerJumpMagnitude);
        }
    }

}
