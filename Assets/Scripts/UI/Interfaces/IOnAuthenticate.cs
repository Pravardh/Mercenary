using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.Authentication
{
    public interface IOnAuthenticate
    {
        public void OnSignInSuccess(LoginResult loginResult);
        public void OnSignInFailed(PlayFabError loginError);
    }

}
