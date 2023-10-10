using PlayFab;
using PlayFab.ClientModels;
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
            GrantMoneyToPlayer();

            SceneManager.LoadScene(1);
        }

        private void GrantMoneyToPlayer()
        {
            ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest
            {
                FunctionName = "OnPlayerWin",

            };


            PlayFabClientAPI.ExecuteCloudScript(request,
            result =>
            {
                Debug.Log("Granted money to player");

            },
            error =>
            {
            });
        }


    }

}
