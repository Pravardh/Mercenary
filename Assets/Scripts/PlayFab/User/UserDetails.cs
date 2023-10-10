using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Mercenary.User;

namespace Mercenary.UI
{
    public class UserDetails : MonoBehaviour
    {
        //This retrieves data from playfab, specifically how much currency the player has


        [SerializeField]
        private TextMeshProUGUI usernameTextField;

        [SerializeField]
        private TextMeshProUGUI goldTextField;

        [SerializeField]
        private TextMeshProUGUI coinsTextField;

        public float CurrentCoins { get; private set; }
        public float CurrentGold { get; private set; }


        private void Start()
        {
            RefreshCurrencyValues();
            RefreshPlayerUsername();

        }

        public void RefreshCurrencyValues()
        {
            //Instead of checking in tick, refresh currency whenever a purchase has been made, or when required. This way
            //the function is not constantly running on tick.

            GetUserInventoryRequest request = new GetUserInventoryRequest();

            PlayFabClientAPI.GetUserInventory(
                request,
                result =>
                {
                    if (result.VirtualCurrency != null && result.VirtualCurrency.ContainsKey(UserHandler.COIN_CURRENCY_VALUE) && result.VirtualCurrency.ContainsKey(UserHandler.GOLD_CURRENCY_VALUE)) // Replace "VC" with your virtual currency code
                    {
                        CurrentCoins = result.VirtualCurrency[UserHandler.COIN_CURRENCY_VALUE];
                        CurrentGold = result.VirtualCurrency[UserHandler.GOLD_CURRENCY_VALUE];
                        coinsTextField.text = "Coins: " + CurrentCoins;
                        goldTextField.text = "Gold: " + CurrentGold;
                        
                    }
                },
                error =>
                {

                });
        }

        public void RefreshPlayerUsername()
        {
            // Similar to RefreshCurrency. Gets the player profile, then sets the username

            var request = new GetPlayerProfileRequest
            {
                
            };

            PlayFabClientAPI.GetPlayerProfile(request, result =>
            {

                string username = result.PlayerProfile.DisplayName;
                usernameTextField.text = "Username: " + username;
            },
            error =>
            {
                Debug.LogError("GetPlayerProfile request failed: " + error.GenerateErrorReport());
            });
        }
    }

}
