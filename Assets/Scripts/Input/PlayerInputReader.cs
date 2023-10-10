using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Mercenary.Input
{
    public class PlayerInputReader : MonoBehaviour, Controls.IPlayerActionsActions
    {

        //Getters
        public float PlayerMovementValue { get; private set; }
        public bool PlayerJumpValue { get; private set; }
        public bool PlayerAttackValue { get; private set; }


        public event Action PlayerConsumeEvent;

        private Controls playerControls;

        void Start()
        {
            //Enable and init controls.

            playerControls = new Controls();
            playerControls.PlayerActions.AddCallbacks(this);
            playerControls.Enable();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            //Set PlayerAttackValue

            PlayerAttackValue = context.performed;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            //Set PlayerJumpValue

            PlayerJumpValue = context.performed;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            //If moved the set the playermovementvalue if not reset.

            if (context.performed)
                PlayerMovementValue = context.ReadValue<float>();
            else
                PlayerMovementValue = 0f;
        }

        public void OnConsume(InputAction.CallbackContext context)
        {

            if (context.performed)
                PlayerConsumeEvent?.Invoke();
        }

        public void OnReset(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        //Helper functions to enable and disable input.

        public void EnableInput()
        {
            playerControls.Enable();
        }

        public void DisableInput()
        {
            playerControls.Disable();

        }
    }
}
