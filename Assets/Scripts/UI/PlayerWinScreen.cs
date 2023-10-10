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
        private Button backButton;

        void Start()
        {
            backButton.onClick.AddListener(LoadMainMenu);
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene(1);
        }

        
    }

}
