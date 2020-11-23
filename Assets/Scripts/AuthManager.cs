using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Facebook.Unity;

public class AuthManager : MonoBehaviour
{
    public InputField emailField;
    public InputField passwordField;
    public Button signInButton;
    public GameObject errorPanel;

    // static으로 선언한 이유는, AuthManager.xxx를 통해 이 변수에 즉시 접근하기 위함
    public static FirebaseApp firebaseApp;
    public static FirebaseAuth firebaseAuth;
    public static FirebaseUser User;
    public static bool IsFirebaseReady { get; private set; }
    public static bool IsSignInOnProgress { get; set; }

    // 초기화
    public void Awake()
    {
        // 상태 비활성화
        signInButton.interactable = false;
        errorPanel.SetActive(false);
        IsSignInOnProgress = false;

        // Firebase 사용 가능한지 확인(Async메서드가 아니기 때문에 callback, chain 필요 --> ContinueWith로 chain 걸어주기)
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var result = task.Result;
            
            if (result != DependencyStatus.Available)
            {
                Debug.LogError("Firebase is not available: " + result.ToString());
                IsFirebaseReady = false;
            }
            else
            {
                Debug.Log("Firebase is ready now.");
                IsFirebaseReady = true;

                firebaseApp = FirebaseApp.DefaultInstance;
                firebaseAuth = FirebaseAuth.DefaultInstance;
            }

            signInButton.interactable = IsFirebaseReady;
        }
        );
    }

    // 로그인 버튼 클릭 시
    public void OnClickSignIn()
    {
        
        // 파이어베이스가 준비 안된 상태 || 이미 유저가 할당된 경우 || 이미 로그인 서비스가 실행 중인 경우
        if (!IsFirebaseReady || User != null || IsSignInOnProgress == true)
        {
            return;
        }

        IsSignInOnProgress = true;
        signInButton.interactable = false;

        // 이메일 입력을 통한 로그인
        firebaseAuth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWithOnMainThread(task =>
        {
            Debug.Log($"Sign in status : {task.Status}");
            IsSignInOnProgress = false;
            signInButton.interactable = true;

            if (task.IsFaulted)
            {
                Debug.LogError("Fail to Sign-in: " + task.Exception);
                errorPanel.SetActive(true);
            }
            else if (task.IsCanceled)
            {
                Debug.LogError("SignIn was canceled");
            }
            else // 로그인 성공의 경우
            {
                User = task.Result;
                Debug.Log("Succeed to Sign-in: " + User.Email);
                SceneManager.LoadScene("Mainmenu");
            }
        });
    }
}

