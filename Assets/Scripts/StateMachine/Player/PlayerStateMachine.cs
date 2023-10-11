using UnityEngine;
using Mercenary.Input;
using Mercenary.HealthSystem;
using Mercenary.Audio;
using Mercenary.Managers;
using Mercenary.Utilities;

namespace Mercenary.StateMachine
{
    public class PlayerStateMachine : BaseStateMachine, IGameState
    {
        [SerializeField]
        private Transform playerGroundCheck;

        [SerializeField]
        private GameManager gameManager;

        private Rigidbody2D playerRigidbody;
        private PlayerInputReader playerInputReader;
        private BasePlayerState playerState;
        private AudioHandler playerAudioHandler;

        private IHealthSystem playerHealthSystem;

        void Start()
        {
            InitBase();
            //Init player values

            playerRigidbody = GetComponent<Rigidbody2D>();
            playerInputReader = GetComponent<PlayerInputReader>();
            playerHealthSystem = GetComponent<IHealthSystem>();
            playerAudioHandler = GetComponent<AudioHandler>();

            //Start with idle state

            playerState = new PlayerIdleState(gameObject, characterEyes, characterAnimator, playerHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck, playerAudioHandler);

            gameManager.OnGameStateChanged += OnGameStateChanged;
        }

        // Update is called once per frame
        void Update()
        {
            //Execute whatever player state player is in.

            playerState = (BasePlayerState)playerState?.Execute();
        }

        public void OnGameStateChanged(GameState newState)
        {

            //If you're not playing disable input, else enable input

            if (newState != GameState.Playing)
            {
                playerInputReader.DisableInput();
            }
            else
            {
                playerInputReader.EnableInput();
            }
        }

    }

}
