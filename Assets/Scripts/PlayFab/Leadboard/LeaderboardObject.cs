using TMPro;
using UnityEngine;

namespace Mercenary.Leaderboard
{
    public class LeaderboardObject : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI usernameTextField;

        [SerializeField]
        private TextMeshProUGUI scoreTextField;

        public void SetUsernameAndScore(string usernameText, string scoreText)
        {
            //This function is called when the Leaderboard is updating the UI. 

            usernameTextField.text = usernameText;
            scoreTextField.text = scoreText;
        }
    }

}
