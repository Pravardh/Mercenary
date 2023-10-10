using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mercenary.UI
{
    public class PlayerLoseScreen : MonoBehaviour
    {
        [SerializeField]
        private Button retryButton;


        private void Start()
        {
            retryButton.onClick.AddListener(ReloadLevel);
        }

        private void OnDestroy()
        {
            retryButton.onClick.RemoveListener(ReloadLevel);
        }

        private void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


}
