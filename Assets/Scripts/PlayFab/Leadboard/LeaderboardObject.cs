using System.Collections;
using System.Collections.Generic;
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
            usernameTextField.text = usernameText;
            scoreTextField.text = scoreText;
        }
    }

}
