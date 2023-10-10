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
        private SpriteRenderer playerSpriteRenderer;

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
            Color color = playerSpriteRenderer.color;
            color.a = .5f;
            LeanTween.color(playerSpriteRenderer.gameObject, color, 2.0f);

            playerSpriteRenderer.color = color;

            yield return new WaitForSeconds(15);

            color.a = 1f;
            LeanTween.color(playerSpriteRenderer.gameObject, color, 2.0f);
            PlayerPrefs.DeleteKey(abilityToConsume);
            PlayerPrefs.Save();
        }
    }

}
