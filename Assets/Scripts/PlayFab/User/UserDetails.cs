using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using PlayFab.Json;
using Mercenary.User;
using static UnityEditor.Progress;

namespace Mercenary.UI
{
    public class UserDetails : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI usernameTextField;

        [SerializeField]
        private TextMeshProUGUI goldTextField;

        [SerializeField]
        private TextMeshProUGUI coinsTextField;


        private void Start()
        {
            RefreshCurrencyValues();
            RefreshPlayerUsername();

        }

        public void RefreshCurrencyValues()
        {
            GetUserInventoryRequest request = new GetUserInventoryRequest();

            PlayFabClientAPI.GetUserInventory(
                request,
                result =>
                {
                    if (result.VirtualCurrency != null && result.VirtualCurrency.ContainsKey(UserHandler.COIN_CURRENCY_VALUE) && result.VirtualCurrency.ContainsKey(UserHandler.GOLD_CURRENCY_VALUE)) // Replace "VC" with your virtual currency code
                    {
                        coinsTextField.text = "Gold: " + result.VirtualCurrency[UserHandler.COIN_CURRENCY_VALUE];
                        goldTextField.text = "Silver: " + result.VirtualCurrency[UserHandler.GOLD_CURRENCY_VALUE];
                        
                    }
                },
                error =>
                {

                });
        }

        public void RefreshPlayerUsername()
        {
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
