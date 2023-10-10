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
            //Start Game

            ChangeGameState(GameState.Playing);
        }

        public void ChangeGameState(GameState newState)
        {
            //Change game state and invoke any events that are bound to OnGameStateChanged

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
            //When enemy spawns add enemy count

            if (enemyHealthSystemList.Contains(healthSystem)) return;

            enemyHealthSystemList.Add(healthSystem);
            totalEnemyCount++;
        }

        public void RemoveEnemyCount(IHealthSystem healthSystem)
        {
            //When enemy dies remove enemy count

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
            //Change gamestate to lost which brings up the UI menu for losing
            ChangeGameState(GameState.Lost);
        }

        public void OnApplicationQuit()
        {
            PlayerPrefs.DeleteKey("Invisibility");
            //When application is closed suddenly, delete the key
        }
    }
    
    //GameState enum
    public enum GameState
    {
        Playing,
        Lost,
        Won
    }
}
