using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mercenary.UI
{
    public class MainMenuHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject parentUIElement;

        //Main menu helper functions

        private void Start()
        {
            LeanTween.scale(parentUIElement, Vector3.one, 1.0f);
        }

        public void QuitApplication()
        {
            Application.Quit();
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(2);
        }
    }

}
