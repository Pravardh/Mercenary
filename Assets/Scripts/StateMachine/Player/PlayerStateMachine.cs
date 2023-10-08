using UnityEngine;
using Mercenary.Input;
using Mercenary.HealthSystem;

namespace Mercenary.StateMachine
{
    public class PlayerStateMachine : BaseStateMachine
    {
        [SerializeField]
        private Transform playerGroundCheck;

        private Rigidbody2D playerRigidbody;
        private PlayerInputReader playerInputReader;
        private BasePlayerState playerState;

        private IHealthSystem playerHealthSystem;
        // Start is called before the first frame update
        void Start()
        {
            InitBase();

            playerRigidbody = GetComponent<Rigidbody2D>();
            playerInputReader = GetComponent<PlayerInputReader>();
            playerHealthSystem = GetComponent<IHealthSystem>();


            playerState = new PlayerIdleState(gameObject, characterEyes, characterAnimator, playerHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck);
            
        }

        // Update is called once per frame
        void Update()
        {
            playerState = (BasePlayerState)playerState?.Execute();

        }
    }

}
