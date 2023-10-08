using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;

namespace Mercenary.Authentication
{
    public class Authenticator
    {
        private string emailID;
        private string password;

        private event Action<LoginResult> OnSignInSucceedEvent;
        private event Action<PlayFabError> OnSignInFailedEvent;
        private event Action<RegisterPlayFabUserResult> OnSignUpSuccessEvent;
        private event Action<PlayFabError> OnSignUpFailedEvent;

        public Authenticator(string emailID, string password, Action<LoginResult> onSignInSucessMethod, Action<PlayFabError> onSignInFailureMethod, Action<RegisterPlayFabUserResult> onSignUpSuccessMethod, Action<PlayFabError> onSignUpFailureMethod)
        {
            this.emailID = emailID;
            this.password = password;
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
                RequireBothUsernameAndEmail = false
            };

            PlayFabClientAPI.RegisterPlayFabUser(request, OnSignUpSuccess, OnSignUpFailed);

        }
        
        private void OnSignUpSuccess(RegisterPlayFabUserResult userResult)
        {
            OnSignUpSuccessEvent?.Invoke(userResult);
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

