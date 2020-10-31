using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    public bool IsFirebaseReady { get; private set; } // firebase를 지원할 수 있는 환경인지 판별하는 변수
    public bool IsSignInOnProgress { get; private set; } // 현재 로그인이 진행 중인지 판별하는 변수(더블탭으로 로그인을 시도하지 않도록)

    public InputField emailField; // Email Field
    public InputField passwordField; // Password Field
    public Button signInButton; // Sign-in Button
    public GameObject errorPanel; // LoginError-Panel

    // static으로 선언한 이유는, AuthManager.xxx를 통해 이 변수에 즉시 접근하기 위함
    public static FirebaseApp firebaseApp;
    public static FirebaseAuth firebaseAuth;
    public static FirebaseUser User; // FirebaseAuth를 통해서 emailField와 passwordField에 해당하는 유저 정보를 가져와서 할당하는 부분

    public void Awake()
    {
        // Initialization. 상태 비활성화
        signInButton.interactable = false;
        errorPanel.SetActive(false);

        // Firebase 사용 가능한지 확인
        // 이 앱에서 파이어베이스가 실행될 수 있는 환경인지 체크하고 Dependency를 fix해줌. (그렇지 않은 환경이면 CheckAndFixDependenciesAsync 메서드가 알려줌.)
        // Async메서드가 아니기 때문에 callback, chain 필요 --> ContinueWith로 chain 걸어주기
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var result = task.Result;
            
            // 이용 가능한 상태가 아니라면
            if (result != DependencyStatus.Available)
            {
                Debug.LogError("Firebase is not available: " + result.ToString());
                IsFirebaseReady = false;
            }
            else // 이용 가능한 상태면
            {
                Debug.Log("Firebase is ready now.");
                IsFirebaseReady = true;

                firebaseApp = FirebaseApp.DefaultInstance;
                firebaseAuth = FirebaseAuth.DefaultInstance;
            }

            signInButton.interactable = IsFirebaseReady; // 파이어베이스 상태에 따라 버튼 활성화 여부 결정
        }
        );
    }

    public void OnClickSignIn()
    {
        // 파이어베이스가 준비 안된 상태 || 이미 로그인 서비스가 실행 중인 경우 || 이미 유저가 할당된 경우
        if (!IsFirebaseReady || IsSignInOnProgress || User != null)
        {
            return;
        }

        IsSignInOnProgress = true; //로그인 과정 진행 중
        signInButton.interactable = false; //로그인 버튼 비활성화

        // 이메일 입력을 통한 로그인(SigninWithxxxxx 메서드는 다양한 로그인 기능 지원함 ex. 구글, 깃허브 등)
        firebaseAuth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWithOnMainThread(task =>
        {
            Debug.Log($"Sign in status : {task.Status}");

            IsSignInOnProgress = false;
            signInButton.interactable = true;

            if (task.IsFaulted) // 로그인에 실패했을 경우
            {
                Debug.LogError("Failed to Sign-in: " + task.Exception);
                errorPanel.SetActive(true);
            }
            else if (task.IsCanceled) // 로그인이 취소되었을 경우
            {
                Debug.LogError("SignIn was canceled");
            }
            else // 로그인 성공의 경우
            {
                User = task.Result;
                Debug.Log("Success to Sign-in: " + User.Email);
                SceneManager.LoadScene("Mainmenu"); // 메인메뉴Secene으로 이동
            }
        });
    }
}