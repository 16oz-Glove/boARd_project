using Firebase;     //파이어배이스
using Firebase.Auth;     //파이어배이스
using Firebase.Extensions;       //파이어배이스
using UnityEngine;
using UnityEngine.SceneManagement;     //유니티 Scene과 관련된 pakage
using UnityEngine.UI;                  //UI요소와 관련된 pakage

public class AuthManager : MonoBehaviour
{
    public bool IsFirebaseReady { get; private set; }       //firebase를 지원할 수 있는 환경인지를 판별하는 변수
    public bool IsSignInOnProgress { get; private set; }    //더블탭으로 로그인을 시도하지않도록, 즉 현재 로그인이 진행중인지 아닌지 판별하기 위한 bool 함수

    public InputField emailField; // Email Field
    public InputField passwordField;    //Password Field
    public Button signInButton;         //Sign-in Button

    //static으로 선언한 이유는, AuthManager.xxx 를 통해 이 변수에 즉시 접근하기 위해!!
    public static FirebaseApp firebaseApp;      //파이어배이스 전체 어플리케이션을 관리하는 object
    public static FirebaseAuth firebaseAuth;        //파이어배이스 Authentication을 관리하는 object

    public static FirebaseUser User;    //파이어베이스 어스를 통해서 emailField와 passwordField에 해당하는 유저정보를 가져와서 할당하는 부분

    public void Start()
    {
        signInButton.interactable = false; // signIn 버튼을 클릭할 수 없게 만들어줌. (아직 준비가 안됐는데 곧장 클릭할 수 있게 만들면 안되기때문에)

        //파이어배이스를 사용 가능한지 검사하는 부분. 파이어배이스가 실행될 수 있는 환경인지 이 앱에서 체크하고
        //Dependency 를 fix해줌. 반대로 그렇지 않은 환경이면 아래 CheckAndFixDependenciesAsync method 가 알려줌.
        //그리고 Async 메서드가아니기 때문에 callback 이나 chain을 실행시켜준다.
        //CheckAndFixDependenciesAsync method가 끝나는 타이밍에 맞춰서 다른 metod를 실행시켜준다.
        //ContinueWith로 chain걸어주기.
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var result = task.Result;   //task의 결과를 받아오고

            if (result != DependencyStatus.Available)    //이용 가능한 상태가 아니라면
            {
                Debug.LogError(result.ToString());      //에러 로그 띄우기
                IsFirebaseReady = false;
            }
            else                                    //이용 가능한 상태면
            {
                IsFirebaseReady = true;

                firebaseApp = FirebaseApp.DefaultInstance;
                firebaseAuth = FirebaseAuth.DefaultInstance;
            }

            signInButton.interactable = IsFirebaseReady;    //Firebase를 사용할 준비가 되었다면 버튼이 활성화 될 것이고, 아니면 그대로 비활성화
        }
        );
    }

    public void SignIn()
    {
        if (!IsFirebaseReady || IsSignInOnProgress || User != null)    //파이어베이스가 준비 안된 상태 || 이미 로그인 서비스가 실행 중인 경우 || 이미 유저가 할당된 경우
        {
            return;             //곧장 리턴
        }

        IsSignInOnProgress = true;          //로그인 과정 진행중임.
        signInButton.interactable = false;  //로그인 버튼 비 활성화

        //SigninWith xxxxx 메서드는 다양한 로그인 기능을 지원한다. 구글, 깃허브 등등 같은 아이디로 로그인 할 수있도록 지원함.
        //아래 코드는 이메일입력을 통한 로그인을 하는 부분을 구현한 코드임.
        firebaseAuth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWithOnMainThread(task =>
        {
            Debug.Log($"Sign in status : {task.Status}");   //현재 로그인의 정보를 

            IsSignInOnProgress = false;
            signInButton.interactable = true;

            if (task.IsFaulted) //로그인에 실패했을 경우
            {
                Debug.LogError(task.Exception);
            }
            else if (task.IsCanceled)   //로그인이 최고되었을 경우
            {
                Debug.LogError("SignIn-indexer canceled");
            }
            else
            {
                User = task.Result; //로그인이 성공적으로일어나서 사용자 정보 저장.
                Debug.Log(User.Email);      //해당 유저의 이메일 주소 띄우기
                SceneManager.LoadScene("Lobby");    //로비Secene으로이동
            }

        });
    }
}