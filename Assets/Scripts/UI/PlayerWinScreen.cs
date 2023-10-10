using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mercenary.UI
{
    public class PlayerWinScreen : MonoBehaviour
    {
        [SerializeField]
        private Button nextLevelButton;

        [SerializeField]
        private Button backButton;

        private int maxBuildIndex = 2;
        void Start()
        {
            if(SceneManager.GetActiveScene().buildIndex == maxBuildIndex)
            {
                nextLevelButton.gameObject.SetActive(false);
            }

            nextLevelButton.onClick.AddListener(LoadNextLevel);
            backButton.onClick.AddListener(LoadMainMenu);
        }

        private void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene(1);
        }

        
    }

}
