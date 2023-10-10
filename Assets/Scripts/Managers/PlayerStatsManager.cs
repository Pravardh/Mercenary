using Mercenary.Utilities;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mercenary.Managers
{

    //This script calculates the totalTimeTaken for the particular level and sets the statistics on PlayFab using cloudscript.
    public class PlayerStatsManager : MonoBehaviour, IGameState
    {
        
        [SerializeField]
        private GameManager gameManager;

        [SerializeField]
        private TextMeshProUGUI totalTakenTimeText;

        private DateTime startTime;
        private DateTime endTime;
        private void Awake()
        {
            gameManager.OnGameStateChanged += OnGameStateChanged;

            startTime = DateTime.Now;
        }

        private void OnDestroy()
        {
            gameManager.OnGameStateChanged -= OnGameStateChanged;    
        }

        public void OnGameStateChanged(GameState newState)
        {
            //If player wins the game, set total time taken in cloud. Statistics name is automatically calculated for seamlesness in
            //different levels.
            if (newState == GameState.Won)
            {
                endTime = DateTime.Now;

                int timeTaken = (int)(endTime - startTime).TotalSeconds;
                totalTakenTimeText.text += timeTaken + " Seconds";

                //Server side statistics update
                UpdatePlayerStatistics(timeTaken);
            }
        }
        public void UpdatePlayerStatistics(int score)
        {
            //Using cloud scripts because you cannot change statistics in clients. So calling it from the server

            string statisticsName = SceneManager.GetActiveScene().name + "Score";
            Debug.Log(statisticsName + $" {score}");

            var playerStatisticsUpdate = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = statisticsName,
                    Value = score
                }
            };

            var playerStatisticsUpdateRequest = new ExecuteCloudScriptRequest()
            {
                FunctionName = "UpdatePlayerLevelScore",
                FunctionParameter = new
                {
                    statisticName = statisticsName,
                    playerScore = score
                }
            };

            // Send the request to PlayFab
            PlayFabClientAPI.ExecuteCloudScript(playerStatisticsUpdateRequest, OnUpdatePlayerStatisticsSuccess, OnUpdatePlayerStatisticsError);
        }


        private void OnUpdatePlayerStatisticsSuccess(ExecuteCloudScriptResult result)
        {
            Debug.Log(result.FunctionResult.ToString());
        }

        private void OnUpdatePlayerStatisticsError(PlayFabError error)
        {
            Debug.LogError("Error updating player statistics: " + error.ErrorMessage);
        }


    }

}
