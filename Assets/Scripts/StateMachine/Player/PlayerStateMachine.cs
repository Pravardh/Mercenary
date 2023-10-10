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
        // Start is called before the first frame update
        void Start()
        {
            InitBase();

            playerRigidbody = GetComponent<Rigidbody2D>();
            playerInputReader = GetComponent<PlayerInputReader>();
            playerHealthSystem = GetComponent<IHealthSystem>();
            playerAudioHandler = GetComponent<AudioHandler>();

            playerState = new PlayerIdleState(gameObject, characterEyes, characterAnimator, playerHealthSystem, playerInputReader, playerRigidbody, playerGroundCheck, playerAudioHandler);

            gameManager.OnGameStateChanged += OnGameStateChanged;
        }

        // Update is called once per frame
        void Update()
        {
            playerState = (BasePlayerState)playerState?.Execute();
        }

        public void OnGameStateChanged(GameState newState)
        {
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
