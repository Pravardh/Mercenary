using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using Mercenary.User;
using System;
using PlayFab.Json;
using TMPro;

namespace Mercenary.UI
{
    public class ShopHandler : MonoBehaviour
    {
        [SerializeField]
        private UserDetails playerDetailsHandler;

        [SerializeField]
        private Button buyInvisibilityWithCoinsButton;
        [SerializeField]
        private Button buyInvisibilityWithGoldButton;

        [SerializeField]
        private TextMeshProUGUI shopStateText;


        [SerializeField]
        private MainMenuHandler mainMenuHandler;

        private long currentTime;
        private long endTime;

        private void Awake()
        {

            //Checks to see if player has already bought Invisibility. If it has been revoked
            //On the server or has been consumed, buttons are disabled.

            if (playerDetailsHandler.CurrentCoins < 50)
            {
                buyInvisibilityWithCoinsButton.gameObject.SetActive(false);
                buyInvisibilityWithGoldButton.gameObject.SetActive(false);
                SetStatusText("Insufficient coins! Play to earn");
            }


            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), 
                result =>
                {
                    foreach (ItemInstance item in result.Inventory)
                    {
                        if (item.DisplayName == "Invisibility")
                        {
                            SetStatusText("You have already bought this upgrade!");
                            buyInvisibilityWithCoinsButton.gameObject.SetActive(false);
                            buyInvisibilityWithGoldButton.gameObject.SetActive(false);
                            return;
                        }
                        else
                        {
                            buyInvisibilityWithCoinsButton.gameObject.SetActive(true);
                            buyInvisibilityWithGoldButton.gameObject.SetActive(true);
                        }
                    }
                },
                error =>
                {
                    Debug.LogError("Could not get player inventory");
                });


            //If buttons are not null add the on click listeners.
            if (buyInvisibilityWithCoinsButton != null)
            {
                buyInvisibilityWithCoinsButton.onClick.AddListener(OnBuyInvisibilityWithCoinsClicked);
            }
            if(buyInvisibilityWithGoldButton != null)
            {
                buyInvisibilityWithGoldButton.onClick.AddListener(OnBuyInvisibilityWithGoldClicked);
            }
        }

        private void OnDestroy()
        {

            //Clean up 

            buyInvisibilityWithCoinsButton.onClick.RemoveListener(OnBuyInvisibilityWithCoinsClicked);
            buyInvisibilityWithGoldButton.onClick.RemoveListener(OnBuyInvisibilityWithGoldClicked);
        }

        private void Start()
        {
            //If and upgrade is not ongoing, disable the speed up with gold button

            if (!PlayerPrefs.HasKey("EndTime"))
            {
                buyInvisibilityWithGoldButton.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            //Check to see if upgrade is going on, if it is, check and compare times.
            //End time is set in server for cheat protection. 

            if (PlayerPrefs.HasKey("EndTime"))
            {
                buyInvisibilityWithGoldButton.gameObject.SetActive(true);

                string endTimeString = PlayerPrefs.GetString("EndTime");
                if (DateTime.TryParseExact(endTimeString, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime savedDateTime))
                {

                    if (savedDateTime < DateTime.Now)
                    {
                        PurchaseWithCoins();

                        PlayerPrefs.DeleteKey("EndTime");
                    }
                }
            }
        }

        private void SetStatusText(string newText)
        {
            //Helper function to set status text (this provides info about the process) 
            //LeanTween simple animation 

            shopStateText.transform.position = new Vector3(-1036, 0);
            LeanTween.moveX(shopStateText.gameObject, 0, 1);
            shopStateText.text = newText;
        }

        private void OnBuyInvisibilityWithCoinsClicked()
        {
            //Upgrade with soft currency if coins button is clicked

            UpgradeWithSoftCurrency();
        }

        private void PurchaseWithCoins()
        {
            var request = new PurchaseItemRequest
            {
                ItemId = "Invisibility",
                Price = 50,
                VirtualCurrency = UserHandler.COIN_CURRENCY_VALUE,

            };
            PlayFabClientAPI.PurchaseItem(request, resultCallback =>
            {
                playerDetailsHandler.RefreshCurrencyValues();
                buyInvisibilityWithGoldButton.gameObject.SetActive(false);
                buyInvisibilityWithCoinsButton.gameObject.SetActive(false);
                SetStatusText("Purchased Item!");


            }, error =>
            {
                SetStatusText("Could not purchase item because: " + error.ErrorMessage);
            });

        }

        private void UpgradeWithSoftCurrency()
        {
            //StartUpgradeWithSoftCurrency is a cloud script that decides what time the 
            //update is supposed to be released. This provides a level of protection

            var request = new ExecuteCloudScriptRequest
            {
                FunctionName = "StartUpgradeWithSoftCurrency"
            };

            PlayFabClientAPI.ExecuteCloudScript(request, result =>
            {
                if (result == null)
                {
                    Debug.LogError("Error executing script. Check javascript code");
                    return;
                }

                JsonObject jsonResult = (JsonObject)result.FunctionResult;
                Debug.Log(jsonResult == null);

                if (jsonResult.TryGetValue("current", out object upgradeStartTime))
                {
                    currentTime = Convert.ToInt64(upgradeStartTime);
                }
                if (jsonResult.TryGetValue("finish", out object upgradeFinishTime))
                {
                    endTime = Convert.ToInt64(upgradeFinishTime);
                }

                float timeToComplete = endTime - currentTime;

                DateTime timeAtCompletion = DateTime.Now.AddSeconds(timeToComplete);

                string dateTimeString = timeAtCompletion.ToString("yyyy-MM-dd HH:mm:ss");

                SetStatusText("Purchase Requested! Invisibility will be available in 2 minutes. You can speed up using gold!");
                buyInvisibilityWithCoinsButton.gameObject.SetActive(false);

                PlayerPrefs.SetString("EndTime", dateTimeString);
                PlayerPrefs.Save();

            }, error =>
            {
                Debug.LogError("Error starting the upgrade: " + error.ErrorMessage);
            });
        }

        private void OnBuyInvisibilityWithGoldClicked()
        {
            //If gold button is clicked, try subtracting virtual currency. If player has enough funds, it will subtract the 
            //currency. If not, it will display a message that says you don't have enough funds.

            if (playerDetailsHandler.CurrentGold < 2)
            {
                SetStatusText("Insufficient gold! Please wait for upgrade to finish");
                return;
            }

            ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest
            {
                FunctionName = "SubtractVirtualCurrency", 
                FunctionParameter = new { virtualCurrency = UserHandler.GOLD_CURRENCY_VALUE, amountToSubtract = 2} 
                
            };


            PlayFabClientAPI.ExecuteCloudScript(request, 
            result => 
            { 
                if (PlayerPrefs.HasKey("EndTime"))
                {
                    DateTime completionTime = DateTime.Now;
                    string completedTimeString = completionTime.ToString("yyyy-MM-dd HH:mm:ss");

                    PlayerPrefs.SetString("EndTime", completedTimeString);

                    buyInvisibilityWithGoldButton.gameObject.SetActive(false);
                    playerDetailsHandler.RefreshCurrencyValues();

                    //If upgrade is on going then set EndTime to the current time so that the upgrade can be granted immedietely.
                }
            }, 

            error => 
            {
                if (error.Error == PlayFabErrorCode.InsufficientFunds)
                {
                    SetStatusText("Insufficient gold!");

                }
            });

  

        }
    }
}

