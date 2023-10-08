using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SignInWithEmail("test@test.com", "12345678");
    }

    public void SignUpWithEmail(string email, string password)
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = email,
            Password = password,
            RequireBothUsernameAndEmail = false // Set to true if you want both username and email
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnSignUpSuccess, OnSignUpFailure);
    }

    private void OnSignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Player signed up successfully!");
    }

    private void OnSignUpFailure(PlayFabError error)
    {
        Debug.LogError("Sign-up failed: " + error.GenerateErrorReport());
        // Handle sign-up failure (e.g., show an error message to the player).
    }
    public void SignInWithEmail(string email, string password)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnSignInSuccess, OnSignInFailure);
    }

    private void OnSignInSuccess(LoginResult result)
    {
        Debug.Log("Player signed in successfully!");
        // Handle post-sign-in logic (e.g., load player data)
    }

    private void OnSignInFailure(PlayFabError error)
    {
        Debug.LogError("Sign-in failed: " + error.GenerateErrorReport());
        SignUpWithEmail("test@test.com", "12345678");
        // Handle sign-in failure (e.g., show error message to the player)
    }

    public void TryReduceCurrency()
    {
        PurchaseItem();
    }

    public void PurchaseItem()
    {
        var request = new PurchaseItemRequest
        {
            ItemId = "Invisibility",
            Price = 2,
            VirtualCurrency = "CC"
        };
        PlayFabClientAPI.PurchaseItem(request, resultCallback =>
        {
            Debug.Log("Purchased item.." + request.Price);

        }, error =>
        {
            Debug.Log("Could not purchase item" + error.ErrorMessage);
        });

        var r = request.ToJson();
        print(r);
    }
}
