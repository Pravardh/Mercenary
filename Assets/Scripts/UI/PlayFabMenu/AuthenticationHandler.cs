using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mercenary.Authentication
{
    public class AuthenticationHandler : MonoBehaviour, IOnAuthenticate
    {
        private Authenticator userAuthenticator;

        [SerializeField]
        private TMP_InputField userEmailAddress;

        [SerializeField]
        private TMP_InputField userPassword;

        [SerializeField]
        private Button userSignInButton;

        private void Awake()
        {
            if (!(userSignInButton == null))
            {
                userSignInButton.onClick.AddListener(OnSignInClicked);
            }


        }

        private void OnDisable()
        {
            if (!(userSignInButton == null))
            {
                userSignInButton.onClick.RemoveListener(OnSignInClicked);
            }
        }
        private void OnSignInClicked()
        {
            if(!IsValidEmail() ||  !IsValidPassword())
            {
                Debug.Log("Please recheck email or password properties");
                return;
            }

            userAuthenticator = new Authenticator(GetEmailAddress(), GetPassword(), OnSignInSuccess, OnSignInFailed, OnSignUpSuccess, OnSignUpFailed);
         
            userAuthenticator.SignIn();
        }



        private string GetEmailAddress()
        {
            return userEmailAddress.text;
        }


        private string GetPassword()
        {
            return userPassword.text;

        }

        private bool IsValidEmail()
        {
            string emailAddress = GetEmailAddress();

            if(!emailAddress.Contains("@"))
            {
                return false;
            }

            return true;
        }

        private bool IsValidPassword()
        {
            string password = GetPassword();

            if (!(password.Length > 4))
            {
                return false;
            }

            return true;
        }

        public void OnSignInSuccess(LoginResult loginResult)
        {
            SceneManager.LoadScene(1);
        }

        public void OnSignInFailed(PlayFabError loginError)
        {
            Debug.Log(loginError.Error);
            if (loginError.Error == PlayFabErrorCode.InvalidParams)
            {
                Debug.Log("Please recheck your username or password");
            }
            else if(loginError.Error == PlayFabErrorCode.AccountNotFound)
            {
                userAuthenticator.SignUp();
            }
        }

        public void OnSignUpSuccess(RegisterPlayFabUserResult userResult)
        {
            SceneManager.LoadScene(1);
        }

        public void OnSignUpFailed(PlayFabError signUpError)
        {
            Debug.Log("Error " +  signUpError.ErrorMessage);
            Debug.Log("Error Message" +  signUpError.Error);
        }
    }
}

