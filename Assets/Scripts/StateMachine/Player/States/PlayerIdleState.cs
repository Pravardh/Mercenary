using Mercenary.Audio;
using Mercenary.HealthSystem;
using Mercenary.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.StateMachine
{
    public class PlayerIdleState : BasePlayerState
    {
        public PlayerIdleState(GameObject characterReference, Transform characterEyes, Animator characterAnimator, IHealthSystem characterHealthSystem, PlayerInputReader inputReader, Rigidbody2D rigidbody, Transform groundCheck, AudioHandler audioHandler) : base(characterReference, characterEyes, characterAnimator, characterHealthSystem, rigidbody, inputReader, groundCheck, audioHandler)
        {

        }

        public override void OnBegin()
        {
            characterAnimator.SetTrigger("isIdling");

            base.OnBegin();
        }

        public override void OnTick()
        {
            base.OnTick();

            if (playerMovementValue != 0)
            {
                SwitchState(new PlayerMoveState(characterReference, characterEyes, characterAnimator, characterHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck, playerAudioHandler));
            }

            if (playerIsJumping && IsGrounded())
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
            characterAnimator.ResetTrigger("isIdling");

            base.OnEnd();
        }

    }

}
