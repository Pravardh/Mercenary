using Mercenary.Input;
using PlayFab;
using PlayFab.ClientModels;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Mercenary.Abilities
{
    public class ConsumeInvisibility : MonoBehaviour
    {
        private PlayerInputReader playerInputReader;

        private void Awake()
        {
            playerInputReader = GetComponent<PlayerInputReader>();

            playerInputReader.PlayerConsumeEvent += OnPlayerConsumed;
        }
        void TryConsumeInvisibility()
        {
            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetInventorySuccess, OnGetInventoryFailed);
        }

        private void OnGetInventoryFailed(PlayFabError error)
        {

        }
        private void OnGetInventorySuccess(GetUserInventoryResult result)
        {
            foreach (ItemInstance item in result.Inventory)
            {
                if (item.DisplayName == "Invisibility")
                {
                    Consume(item.ItemInstanceId);
                    break;
                }
            }
        }

        private void OnPlayerConsumed()
        {
            TryConsumeInvisibility();
        }

        private void Consume(string itemID)
        {

            ConsumeItemRequest request = new ConsumeItemRequest
            {
                ItemInstanceId = itemID, // Set to null if you want to consume the first item with the specified item ID
                ConsumeCount = 1 // Specify the quantity to consume (typically 1 for consumable items)
            };

            PlayFabClientAPI.ConsumeItem(request,
                result =>
                {
                    Debug.Log("Consumed item successfully");
                    PlayerPrefs.SetString("Invisibility", "True");
                },
                error =>
                {
                    Debug.Log("Consumed item fail" + error.ErrorMessage);

                });
        }

        //GameManager.OnLevelEnded += FUNCTION TO REMOVE PLAYER PREFS INVISIBILITY
        //THIS IS SO THAT ONCE THE LEVEL IS OVER THE PLAYER PREFS DOES NOT PERSIST TO THE NEXT LEVEL
    }

}
