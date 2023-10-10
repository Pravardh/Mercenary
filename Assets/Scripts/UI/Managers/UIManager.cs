using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Mercenary.Utilities;
using System.Collections;
using System.Security.Cryptography;

namespace Mercenary.Managers
{
    public class UIManager : MonoBehaviour, IGameState
    {
        [SerializeField]
        private GameObject playerWinScreen;

        [SerializeField]
        private GameObject playerLoseScreen;

        [SerializeField]
        private GameObject playerControls;

        [SerializeField]
        private GameManager gameManager;

    

        private void Awake()
        {
            gameManager.OnGameStateChanged += OnGameStateChanged;
            LeanTween.init(10000);
        }

        public void OnGameStateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.Won:
                    DisplayWinScreen();
                    break;

                case GameState.Lost:
                    DisplayLoseScreen();
                    break;
            }
        }

        private void OnDestroy()
        {
            gameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        public void DisplayWinScreen()
        {
            LeanTween.moveLocalY(playerWinScreen, 0.0f, 2.0f);
            playerControls.SetActive(false);
        }

        public void DisplayLoseScreen()
        {
            LeanTween.scale(playerLoseScreen, Vector3.one, 2.0f);
            playerControls.SetActive(false);
        }
    }
}
