using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mercenary.Input
{
    public class PlayerInputReader : MonoBehaviour, Controls.IPlayerActionsActions
    {
        public float PlayerMovementValue { get; private set; }
        public bool PlayerJumpValue { get; private set; }
        public bool PlayerAttackValue { get; private set; }

        public event Action PlayerConsumeEvent;


        private Controls playerControls;

        // Start is called before the first frame update
        void Start()
        {
            playerControls = new Controls();
            playerControls.PlayerActions.AddCallbacks(this);
            playerControls.Enable();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            PlayerAttackValue = context.performed;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            PlayerJumpValue = context.performed;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
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
