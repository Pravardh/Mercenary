using Mercenary.Audio;
using Mercenary.HealthSystem;
using Mercenary.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mercenary.StateMachine
{
    public class PlayerMoveState : BasePlayerState
    {
        public PlayerMoveState(GameObject characterReference, Transform characterEyes, Animator characterAnimator, IHealthSystem characterHealthSystem, PlayerInputReader inputReader, Rigidbody2D rigidbody, Transform groundCheck, AudioHandler audioHandler) : base(characterReference, characterEyes, characterAnimator, characterHealthSystem, rigidbody, inputReader, groundCheck, audioHandler)
        {

        }

        public override void OnBegin()
        {
            characterAnimator.SetTrigger("isMoving");

            base.OnBegin();
        }

        public override void OnTick()
        {
            base.OnTick();



            MovePlayer(playerMovementValue);

            if(playerIsJumping && IsGrounded())
            {
                SwitchState(new PlayerJumpState(characterReference, characterEyes, characterAnimator, characterHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck, playerAudioHandler));
            }

            if (playerIsAttacking)
            {
                SwitchState(new PlayerAttackState(characterReference, characterEyes, characterAnimator, characterHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck, playerAudioHandler));
            }
            if (characterHealthSystem.IsDead())
            {
                SwitchState(new PlayerDeathState(characterReference, characterEyes, characterAnimator, characterHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck, playerAudioHandler));
            }

        }

        public override void OnEnd()
        {
            characterAnimator.ResetTrigger("isMoving");

            base.OnEnd();
        }

        private void MovePlayer(float movementValue)
        {
            if (playerMovementValue == 0)
            {
                SwitchState(new PlayerIdleState(characterReference, characterEyes, characterAnimator, characterHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck, playerAudioHandler));
                return;
            }

            playerRigidbody.velocity = new Vector2(movementValue * 7f, playerRigidbody.velocity.y); // Replace with character speed

            SetCharacterOrientation(movementValue == -1 ? Utilities.CharacterOrientation.LEFT : Utilities.CharacterOrientation.RIGHT);
        }
    }

}
