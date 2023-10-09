using Mercenary.Input;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Mercenary.Abilities
{
    public class ConsumeInvisibility : MonoBehaviour
    {
        private PlayerInputReader playerInputReader;

        [SerializeField]
        private string abilityToConsume = "Invisibility";

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
                if (item.DisplayName == abilityToConsume)
                {
                    Consume(item.ItemInstanceId);
                    break;
                }
            }
        }

        private void OnPlayerConsumed()
        {
            if(!PlayerPrefs.HasKey(abilityToConsume))
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
                    StartCoroutine("EnableInvisibility");
                },
                error =>
                {
                    Debug.Log("Consumed item fail" + error.ErrorMessage);

                });
        }

        IEnumerator EnableInvisibility()
        {
            PlayerPrefs.SetString(abilityToConsume, "True");

            yield return new WaitForSeconds(15);
            
            PlayerPrefs.DeleteKey(abilityToConsume);
            PlayerPrefs.Save();
        }

        //GameManager.OnLevelEnded += FUNCTION TO REMOVE PLAYER PREFS INVISIBILITY
        //THIS IS SO THAT ONCE THE LEVEL IS OVER THE PLAYER PREFS DOES NOT PERSIST TO THE NEXT LEVEL
    }

}
