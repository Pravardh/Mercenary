using Mercenary.Input;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
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
        void TryGetInventory()
        {
            //Try getting the user inventory

            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetInventorySuccess, OnGetInventoryFailed);
        }

        private void OnGetInventoryFailed(PlayFabError error)
        {

        }
        private void OnGetInventorySuccess(GetUserInventoryResult result)
        {
            //Simple get inventory

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
            //If player does not have the ability already activated, try to get the inventory, which in turn
            //consumes invisibility.

            if(!PlayerPrefs.HasKey(abilityToConsume))
                TryGetInventory();
        }

        private void Consume(string itemID)
        {
            //Consume invisibility when requested. 

            ConsumeItemRequest request = new ConsumeItemRequest
            {
                ItemInstanceId = itemID,
                ConsumeCount = 1 
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
            //Coroutine to enable and disable invisibility. It turns the sprite rendered translucent, waits for 15 seconds then
            //turns invisibility back off.

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
