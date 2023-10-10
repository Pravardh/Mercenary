using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using System;
using PlayFab.Json;

namespace Mercenary.UI
{
    public class MainMenuHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject parentUIElement;

        private void Start()
        {
            LeanTween.scale(parentUIElement, Vector3.one, 1.0f);
        }

        public void QuitApplication()
        {
            Application.Quit();
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(2);
        }
    }

}
