using Mercenary.Audio;
using Mercenary.HealthSystem;
using Mercenary.Input;
using System.Security.Cryptography;
using UnityEngine;
namespace Mercenary.StateMachine
{
    public class BasePlayerState : BaseState
    {
        protected const int GROUND_LAYER_MASK = 8;

        protected Rigidbody2D playerRigidbody;

        protected PlayerInputReader playerInputReader;

        protected Transform playerGroundCheck;

        protected AudioHandler playerAudioHandler;


        protected float playerJumpMagnitude = 20f;

        protected float playerMovementValue;
        protected bool playerIsAttacking;
        protected bool playerIsJumping;
        protected bool playerCanJump = true;

        protected int playerTotalAttackAmount = 8;
        protected int playerCurrentAttackAmount;



        //Data container

        public BasePlayerState(GameObject characterReference, Transform characterEyes, Animator characterAnimator, IHealthSystem characterHealthSystem, Rigidbody2D rigidbody, PlayerInputReader inputReader, Transform playerGroundCheck, AudioHandler playerAudioHandler) : base(characterReference, characterEyes, characterAnimator, characterHealthSystem)
        {
            playerRigidbody = rigidbody;
            playerInputReader = inputReader;
            this.playerGroundCheck = playerGroundCheck;
            this.playerAudioHandler = playerAudioHandler;
        }

        public override void OnBegin()
        {


            base.OnBegin();
        }

        public override void OnTick()
        {
            base.OnTick();

            playerMovementValue = playerInputReader.PlayerMovementValue;
            playerIsJumping = playerInputReader.PlayerJumpValue;
            playerIsAttacking = playerInputReader.PlayerAttackValue;


        }

        public override void OnEnd()
        {
            
            base.OnEnd();
        }

        public bool CanAttack()
        {
            return playerCurrentAttackAmount < playerTotalAttackAmount;
        }

        protected bool IsGrounded()
        {
            bool isGrounded = Physics2D.OverlapCircle(playerGroundCheck.position, 0.2f, GROUND_LAYER_MASK);

            if (isGrounded)
            {
                return true;
            }

            return false;
        }
    }

}
