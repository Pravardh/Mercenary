using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Linq;
using UnityEngine;


namespace Mercenary.User
{
    public class UserHandler : MonoBehaviour
    {
        public const string GOLD_CURRENCY_VALUE = "GG";
        public const string COIN_CURRENCY_VALUE = "CC";


        private static UserHandler instance;
        public static UserHandler Instance;

        public static GetUserInventoryResult UserInventory { get; private set; }

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }

        private void Update()
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0) return;

            if(!IsUserLoggedIn())
            {
                Debug.Log("User is not logged in");
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
      
        }


        public static bool IsUserLoggedIn()
        {
            return PlayFabClientAPI.IsClientLoggedIn();
        }

    }

}
