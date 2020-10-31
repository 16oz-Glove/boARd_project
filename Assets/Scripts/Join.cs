using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class Join : MonoBehaviour
{
    public bool IsCreateOnProgress { get; private set; }

    public Button joinButton;
    
    public GameObject joinPanel;
    public InputField emailField;
    public InputField passwordField;
    public InputField passwordCheckField;
    public InputField nickNameField;
    public GameObject pwCheck_sad;
    public GameObject pwCheck_good;
    public Button createButton;

    [SerializeField] private string email;
    [SerializeField] private string password;

    FirebaseAuth auth;

    // Initialization
    void Awake()
    {
        joinPanel.SetActive(false);
    }

    // join 버튼 클릭 시 join 패널 활성화 및 초기화 내용
    public void OnClickJoin()
    {
        joinPanel.SetActive(true);

        passwordCheckField.interactable = false;
        nickNameField.interactable = false;
        createButton.interactable = false;
        pwCheck_sad.SetActive(false);
        pwCheck_good.SetActive(false);

        emailField.text = "";
        passwordField.text = "";
        passwordCheckField.text = "";
        nickNameField.text = "";

        auth = FirebaseAuth.DefaultInstance;
    }

    // 패스워드 한 번 더 입력 허용
    public void OneMorePassword()
    {
        if (passwordField.text.Length < 6)
        {
            passwordCheckField.interactable = false;
        }
        else
        {
            passwordCheckField.interactable = true;
        }
    }

    // 올바른 패스워드인지 확인 
    public void PasswordCheck()
    {
        if (passwordField.text != passwordCheckField.text)
        {
            pwCheck_sad.SetActive(true);
            pwCheck_good.SetActive(false);
        }
        else
        {
            pwCheck_sad.SetActive(false);
            pwCheck_good.SetActive(true);

            nickNameField.interactable = true;
        }
    }

    // 마지막
    public void CheckAll()
    {
        if ((emailField && passwordField && nickNameField != null) && pwCheck_good.activeSelf == true)
        {
            createButton.interactable = true;
        }
    }

    // create 버튼 클릭 시
    public void OnClickCreate()
    {
        createButton.interactable = false;

        email = emailField.text;
        password = passwordField.text;

        CreateUser();

        joinPanel.SetActive(false);
        Debug.LogFormat("11111111");
    }

    void CreateUser()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("Sign-up was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to Sign-up: " + task.Exception);
                return;
            }

            // FirebaseAuth에 사용자 이름 등록
            FirebaseUser newUser = task.Result;
            UpdateUserName(newUser);
        });
    }

    // FirebaseAuth에 사용자 이름 등록
    public void UpdateUserName(FirebaseUser newUser)
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
}
