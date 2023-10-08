using Mercenary.HealthSystem;
using Mercenary.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


namespace Mercenary.StateMachine
{
    public class PlayerDeathState : BasePlayerState
    {
        public PlayerDeathState(GameObject characterReference, Transform characterEyes, Animator characterAnimator, IHealthSystem characterHealthSystem, PlayerInputReader inputReader, Rigidbody2D rigidbody, Transform groundCheck) : base(characterReference, characterEyes, characterAnimator, characterHealthSystem, rigidbody, inputReader, groundCheck)
        {

        }

        public override async void OnBegin()
        {
            playerInputReader.enabled = false;

            await PlayDeath();

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


        private async Task PlayDeath()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.4f));

            characterAnimator.SetTrigger("isDead");
        }

    }

}
