﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Facebook.Unity;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

public class AuthFacebook : MonoBehaviour
{
    public InputField nickNameField;
    public GameObject namingPanel;
    
    void Start()
    {
        namingPanel.SetActive(false);

        // 페이스북 초기화 확인
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallBack, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    // 페이스북 초기화 콜백
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
        if (!AuthManager.IsFirebaseReady || AuthManager.IsSignInOnProgress || AuthManager.User != null)
        {
            return;
        }
        AuthManager.IsSignInOnProgress = true;
        
        var perms = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    // 페이스북 로그인 콜백
    private void AuthCallback(ILoginResult result)
    {
        if (result.Error != null)
        {
            Debug.LogError(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn) // 로그인이 된 경우
            {
                Debug.Log("Facebook login successed!");

                var aToken = AccessToken.CurrentAccessToken;
                foreach (string perm in aToken.Permissions)
                {
                    Debug.Log(perm);
                }
                StartCoroutine(coLogin(aToken));
            }
            else
            {
                Debug.Log("User cancelled login");
            }
        }
    }

    // 페이스북 토큰으로 앱 로그인 진행
    IEnumerator coLogin(AccessToken aToken)
    {
        Debug.Log(string.Format("\nTry to get Token...{0}", aToken));
        while (System.String.IsNullOrEmpty(aToken.TokenString)) yield return null;

        Debug.Log(string.Format("\nTry Auth for Facebook...{0}", aToken.UserId));

        Credential credential = FacebookAuthProvider.GetCredential(aToken.TokenString);
        AuthManager.firebaseAuth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            Debug.Log($"Facebook Sign in status : {task.Status}");
            AuthManager.IsSignInOnProgress = false;
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
            }
            else if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
            }
            else
            {
                AuthManager.User = task.Result;
                Debug.Log("Succeed to Sign-in: " + AuthManager.User.Email);
                if(AuthManager.User.DisplayName == null)
                {
                    namingPanel.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene("Mainmenu");
                }
            }
        });
    }

    // naming 패널에서 닉네임 입력받아 DB 등록
    public void OnClickCreate()
    {
        UpdateUserName(AuthManager.User);
        AddToDB(AuthManager.User);
        SceneManager.LoadScene("Mainmenu");
    }

    // Auth에 이름 등록
    private void UpdateUserName(FirebaseUser newUser)
    {
        UserProfile profile = new UserProfile { DisplayName = nickNameField.text };
        newUser.UpdateUserProfileAsync(profile).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("Setting DisplayName was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("Setting DisplayName encountered an error: " + task.Exception);
                return;
            }

            Debug.LogFormat("Successfully created. Welcome, {0}({1})!", newUser.DisplayName, newUser.Email);
        });
    }
    
    // DB에 유저 데이터 등록
    private void AddToDB(FirebaseUser newUser)
    {
        // DB 경로 설정 후 인스턴스 초기화
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://test-board-1158b.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        string time = System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
        NewUser user = new NewUser(newUser.Email, nickNameField.text, time);

        string json = JsonUtility.ToJson(user); // data to json
        string key = newUser.UserId; // take uid as key value
        reference.Child("users").Child(key).SetRawJsonValueAsync(json);

        Debug.Log("Succesfully added new user to DB.");
    }
}