using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Linq;
using UnityEngine;


namespace Mercenary.User
{
    public class UserHandler : MonoBehaviour
    {
        //constants that define the gold and coin currency itemId on playfab

        public const string GOLD_CURRENCY_VALUE = "GG";
        public const string COIN_CURRENCY_VALUE = "CC";

        private static UserHandler instance;
        public static UserHandler Instance;

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
            //Needs to be changed : If user is not logged in then load back main menu. Should not be done in tick.

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
