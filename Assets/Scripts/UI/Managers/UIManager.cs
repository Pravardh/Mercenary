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
            //playerWinScreen.SetActive(false);
            //playerLoseScreen.SetActive(false);
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
            playerWinScreen.SetActive(true);

            Time.timeScale = 0.8f;
            LeanTween.moveLocalY(playerWinScreen, 0.0f, 2.0f);
            LeanTween.moveLocalY(playerControls, -100.0f, 2.0f);
        }

        public void DisplayLoseScreen()
        {
            Time.timeScale = 0.8f;
            playerLoseScreen.SetActive(true);
            LeanTween.scale(playerLoseScreen, Vector2.one, 5.0f);

        }
    }
}
