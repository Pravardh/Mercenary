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

        public void PlayGame()
        {
            SceneManager.LoadScene(2);
        }
    }

}
