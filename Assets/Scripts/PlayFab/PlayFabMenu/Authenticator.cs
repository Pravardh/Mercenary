using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;

namespace Mercenary.Authentication
{
    public class Authenticator
    {
        //This class provides a layer of abstraction. It takes in an email, password and success/failure events and subscribes them.
        //These methods will be invoked accordingly whenever required.

        private string emailID;
        private string password;
        private string username;

        private event Action<LoginResult> OnSignInSucceedEvent;
        private event Action<PlayFabError> OnSignInFailedEvent;
        private event Action<RegisterPlayFabUserResult> OnSignUpSuccessEvent;
        private event Action<PlayFabError> OnSignUpFailedEvent;

        public Authenticator(string emailID, string password, Action<LoginResult> onSignInSucessMethod, Action<PlayFabError> onSignInFailureMethod, Action<RegisterPlayFabUserResult> onSignUpSuccessMethod, Action<PlayFabError> onSignUpFailureMethod)
        {
            this.emailID = emailID;
            this.password = password;
            this.username = "Player" + UnityEngine.Random.Range(0, 1000);
            this.OnSignInSucceedEvent = onSignInSucessMethod;
            this.OnSignInFailedEvent = onSignInFailureMethod;
            this.OnSignUpSuccessEvent = onSignUpSuccessMethod;
            this.OnSignUpFailedEvent = onSignUpFailureMethod;
        }

        public void SignIn()
        {
            var request = new LoginWithEmailAddressRequest
            {
                Email = emailID,
                Password = password,
            };

            PlayFabClientAPI.LoginWithEmailAddress(request, OnSignInSuccess, OnSignInFailure);
        }

        public void SignUp()
        {
            var request = new RegisterPlayFabUserRequest
            {
                Email = emailID,
                Password = password,
                Username = username,
                DisplayName = username
            };

            PlayFabClientAPI.RegisterPlayFabUser(request, OnSignUpSuccess, OnSignUpFailed);

        }
        
        private void OnSignUpSuccess(RegisterPlayFabUserResult userResult)
        {
            OnSignUpSuccessEvent?.Invoke(userResult);
            Debug.Log("Your username is: " + userResult.Username);
        }

        private void OnSignUpFailed(PlayFabError error)
        {
            OnSignUpFailedEvent?.Invoke(error); 
        }

        private void OnSignInSuccess(LoginResult result)
        {
            OnSignInSucceedEvent?.Invoke(result);    
        }

        private void OnSignInFailure(PlayFabError error)
        {
            OnSignInFailedEvent?.Invoke(error);
        }
    }
}

