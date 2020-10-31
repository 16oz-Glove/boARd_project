using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class AuthFacebook : MonoBehaviour
{
    void Start()
    {
        // 초기화 확인
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallBack, OnHideUnity); // 초기화 진행(콜백)
        }
        else // 초기화되었다면 개발자 페이지의 앱 활성화
        {

            // Signal an app activattion App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
    }

    private void InitCallBack()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK"); // 초기화 실패
        }
    }

    // 로그인 과정에서 시간을 멈추는 역할
    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0.0f;
        }
        else
        {
            // Resume the game - we're gettin focus again
            Time.timeScale = 1.0f;
        }
    }

    // 페이스북 로그인 버튼 클릭 시
    public void OnClickFacebookLogin()
    {
        var perms = new List<string>() { "public_profile", "email" };

        // 로그인하라
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    // 콜백
    private void AuthCallback(ILoginResult result)
    {

        if (result.Error != null) // 에러의 경우
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn) // 로그인이 된 경우
            {
                Debug.Log("Facebook login successed!");

                var aToken = AccessToken.CurrentAccessToken;

                Debug.Log(aToken.UserId);
                foreach (string perm in aToken.Permissions)
                {
                    Debug.Log(perm);
                }

                // for firebase
                StartCoroutine(coLogin(aToken));
            }
            else // 로그인이 취소된 경우
            {
                Debug.Log("User cancelled login");
            }
        }
    }

    // 페이스북 토큰을 firebase로 전달하여 계정 생성
    IEnumerator coLogin(AccessToken aToken)
    {
        Debug.Log(string.Format("\nTry to get Token...{0}", aToken));
        while (System.String.IsNullOrEmpty(aToken.TokenString)) yield return null;

        Debug.Log(string.Format("\nTry Auth for Facebook...{0}", aToken.UserId));

        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Firebase.Auth.Credential credential = Firebase.Auth.FacebookAuthProvider.GetCredential(aToken.TokenString);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
        });
    }
}