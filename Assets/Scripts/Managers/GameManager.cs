using JetBrains.Annotations;
using Mercenary.HealthSystem;
using Mercenary.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Mercenary.Managers
{
    public class GameManager : MonoBehaviour
    {
        private int totalEnemyCount;
        
        private GameState gameState;
        private List<IHealthSystem> enemyHealthSystemList = new List<IHealthSystem>();

        public event Action<GameState> OnGameStateChanged;


        private void Awake()
        {
            ChangeGameState(GameState.Playing);
        }

        public void ChangeGameState(GameState newState)
        {
            gameState = newState;
            OnGameStateChanged?.Invoke(gameState);

            if(newState != GameState.Playing)
            {
                totalEnemyCount = 0;
                enemyHealthSystemList.Clear();
                PlayerPrefs.DeleteKey("Invisibility");
            }
        }

        public void AddEnemyCount(IHealthSystem healthSystem)
        {
            if (enemyHealthSystemList.Contains(healthSystem)) return;

            enemyHealthSystemList.Add(healthSystem);
            totalEnemyCount++;
            Debug.Log(totalEnemyCount);
        }

        public void RemoveEnemyCount(IHealthSystem healthSystem)
        {
            if (enemyHealthSystemList.Contains(healthSystem))
            {
                enemyHealthSystemList.Remove(healthSystem);
                totalEnemyCount--;
                if (totalEnemyCount <= 0)
                {
                    ChangeGameState(GameState.Won);
                }
            }
        }
        
        public void OnPlayerDead()
        {
            ChangeGameState(GameState.Lost);
        }

        public void OnApplicationQuit()
        {
            PlayerPrefs.DeleteKey("Invisibility");
            //When application is closed suddenly, delete the key
        }
    }
    

    public enum GameState
    {
        Playing,
        Lost,
        Won
    }
}
