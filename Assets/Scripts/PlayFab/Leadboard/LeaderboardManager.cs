using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

namespace Mercenary.Leaderboard
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField]
        private string leaderboardStatisticName = "Level1Score";

        [SerializeField]
        private GameObject leaderboardItemPrefab;

        [SerializeField]
        private Transform leaderboardLayoutGroup;

        [SerializeField]
        private Button refreshLeaderboardButton;

        private void Start()
        {
            //Set refresh button listeners and then get leaderboard

            refreshLeaderboardButton.onClick.AddListener(TryGetLeaderboard);
            TryGetLeaderboard();
        }

        private void OnDestroy()
        {
            refreshLeaderboardButton.onClick.RemoveListener(TryGetLeaderboard);

        }
        private void ClearLeaderboard()
        {
            //Clearing children when refreshing or starting the leaderboard menu

            if (leaderboardLayoutGroup.childCount > 0)
            {
                for (int i = 0; i < leaderboardLayoutGroup.childCount; i++)
                {
                    Destroy(leaderboardLayoutGroup.GetChild(i).gameObject);
                }
            }
        }
        public void TryGetLeaderboard()
        {
            ClearLeaderboard();

            GetLeaderboardRequest request = new GetLeaderboardRequest()
            {
                StartPosition = 0,
                MaxResultsCount = 10,
                StatisticName = leaderboardStatisticName
            };

            //Get leaderboard request.

            PlayFabClientAPI.GetLeaderboard(
                request,
                result =>
                {
                    result.Leaderboard.Reverse(); //Reversing because here lowest time taken is the highest score...

                    foreach (PlayerLeaderboardEntry entry in result.Leaderboard)
                    {
                        //Instantiate PlayerLeaderboardEntry and set the username and score.

                        GameObject go = Instantiate(leaderboardItemPrefab, leaderboardLayoutGroup);
                        LeaderboardObject leaderboardObject= go.GetComponent<LeaderboardObject>();

                        leaderboardObject.SetUsernameAndScore(entry.DisplayName, entry.StatValue.ToString());
                        
                        
                    }
                },
                error =>
                {

                });
        }
    }

}
