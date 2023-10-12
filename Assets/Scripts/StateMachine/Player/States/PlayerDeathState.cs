using Mercenary.HealthSystem;
using Mercenary.Input;
using System;
using System.Threading.Tasks;
using Mercenary.Audio;
using UnityEngine;


namespace Mercenary.StateMachine
{
    public class PlayerDeathState : BasePlayerState
    {
        public PlayerDeathState(GameObject characterReference, Transform characterEyes, Animator characterAnimator, IHealthSystem characterHealthSystem, PlayerInputReader inputReader, Rigidbody2D rigidbody, Transform groundCheck, AudioHandler audioHandler) : base(characterReference, characterEyes, characterAnimator, characterHealthSystem, rigidbody, inputReader, groundCheck, audioHandler)
        {

        }

        public override void OnBegin()
        {
            playerInputReader.enabled = false;

            PlayDeath();

            base.OnBegin();
        }

        public override void OnTick()
        {
            base.OnTick();
                
        }

        public override void OnEnd()
        {
            characterAnimator.ResetTrigger("isDead");

            base.OnEnd();
        }


        private void PlayDeath()
        {
            characterAnimator.SetTrigger("isDead");
        }

    }

}
