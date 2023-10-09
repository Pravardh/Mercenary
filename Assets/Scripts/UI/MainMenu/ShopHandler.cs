using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using Mercenary.User;
using System;
using PlayFab.Json;
using TMPro;
using System.Data.SqlTypes;

namespace Mercenary.UI
{
    public class ShopHandler : MonoBehaviour
    {
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
            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), 
                result =>
                {
                    foreach (ItemInstance item in result.Inventory)
                    {
                        if (item.DisplayName == "Invisibility")
                        {
                            SetStatusText("You have already bought this upgrade!");
                            Debug.Log("Already bought");
                            return;
                        }
                        else
                        {
                            Debug.Log("Nothing is there");
                        }
                    }
                },
                error =>
                {
                    Debug.LogError("Could not get player inventory");
                });

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
            buyInvisibilityWithCoinsButton.onClick.RemoveListener(OnBuyInvisibilityWithCoinsClicked);
            buyInvisibilityWithCoinsButton.onClick.RemoveListener(OnBuyInvisibilityWithGoldClicked);
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey("EndTime"))
            {
                buyInvisibilityWithGoldButton.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (PlayerPrefs.HasKey("EndTime"))
            {
                buyInvisibilityWithGoldButton.gameObject.SetActive(true);

                string endTimeString = PlayerPrefs.GetString("EndTime");
                if (DateTime.TryParseExact(endTimeString, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime savedDateTime))
                {
                    Debug.Log("DateTime retrieved from PlayerPrefs: " + savedDateTime);

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
            shopStateText.text = newText;
        }

        private void OnBuyInvisibilityWithCoinsClicked()
        {
            UpgradeWithSoftCurrency();
        }

        private void PurchaseWithCoins()
        {
            var request = new PurchaseItemRequest
            {
                ItemId = "Invisibility",
                Price = 1, // Price should be changed to something scalable. Preferably getting it from the catalog
                VirtualCurrency = UserHandler.COIN_CURRENCY_VALUE,

            };
            PlayFabClientAPI.PurchaseItem(request, resultCallback =>
            {
                Debug.Log("Purchased item.." + request.Price);

            }, error =>
            {
                Debug.Log("Could not purchase item" + error.ErrorMessage);

            });

        }

        private void UpgradeWithSoftCurrency()
        {
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

                SetStatusText("Purchase Successful! Training mercenary. You can speed up using gold!");

                PlayerPrefs.SetString("EndTime", dateTimeString);
                PlayerPrefs.Save();

            }, error =>
            {
                Debug.LogError("Error starting the upgrade: " + error.ErrorMessage);
            });
        }

        private void OnBuyInvisibilityWithGoldClicked()
        {
 
            ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest
            {
                FunctionName = "SubtractVirtualCurrency", // Replace with your CloudScript function name
                FunctionParameter = new { virtualCurrency = UserHandler.GOLD_CURRENCY_VALUE, amountToSubtract = 2} // Pass necessary parameters
            };


            PlayFabClientAPI.ExecuteCloudScript(request, 
            result => 
            { 
                if (PlayerPrefs.HasKey("EndTime"))
                {
                    DateTime completionTime = DateTime.Now;
                    string completedTimeString = completionTime.ToString("yyyy-MM-dd HH:mm:ss");

                    PlayerPrefs.SetString("EndTime", completedTimeString);
                    Debug.Log("Sped up process with hard currency! ");
                }
            }, 

            error => 
            {
                Debug.Log("Could not subtract " + error.Error);
            });

  

        }

        private void GetUserInventory()
        {
            var request = new GetUserInventoryRequest();
            PlayFabClientAPI.GetUserInventory(request, OnGetInventorySuccess, OnGetInventoryFailure);
        }

        private void OnGetInventorySuccess(GetUserInventoryResult result)
        {


        }

        private void OnGetInventoryFailure(PlayFabError error)
        {
            Debug.LogError("Failed to get user inventory: " + error.ErrorDetails);
        }
    }
}

