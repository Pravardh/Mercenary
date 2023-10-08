using Mercenary.HealthSystem;
using Mercenary.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mercenary.StateMachine
{
    public class PlayerMoveState : BasePlayerState
    {
        public PlayerMoveState(GameObject characterReference, Transform characterEyes, Animator characterAnimator, IHealthSystem characterHealthSystem, PlayerInputReader inputReader, Rigidbody2D rigidbody, Transform groundCheck) : base(characterReference, characterEyes, characterAnimator, characterHealthSystem, rigidbody, inputReader, groundCheck)
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
                SwitchState(new PlayerJumpState(characterReference, characterEyes, characterAnimator, characterHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck));
            }

            if (playerIsAttacking)
            {
                SwitchState(new PlayerAttackState(characterReference, characterEyes, characterAnimator, characterHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck));
            }
            if (characterHealthSystem.IsDead())
            {
                SwitchState(new PlayerDeathState(characterReference, characterEyes, characterAnimator, characterHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck));
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
                SwitchState(new PlayerIdleState(characterReference, characterEyes, characterAnimator, characterHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck));
                return;
            }


            playerRigidbody.velocity = new Vector2(movementValue * 6.0f, playerRigidbody.velocity.y); // Replace with character speed

            SetCharacterOrientation(movementValue == -1 ? Utilities.CharacterOrientation.LEFT : Utilities.CharacterOrientation.RIGHT);
        }





    }

}
