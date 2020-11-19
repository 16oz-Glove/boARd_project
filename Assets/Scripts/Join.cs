using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

// (DB manager 없는 버전)
public class Join : MonoBehaviour
{
    public Button joinButton;

    public GameObject joinPanel;
    public InputField emailField;
    public InputField passwordField;
    public InputField passwordCheckField;
    public InputField nickNameField;
    public GameObject pwCheck_sad;
    public GameObject pwCheck_good;
    public Button createButton;

    [SerializeField] private string PW;

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

        PW = passwordCheckField.text;
        CreateUser();
        joinPanel.SetActive(false);
    }

    void CreateUser()
    {
        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, PW).ContinueWith(task => {
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

            // DB에 사용자 데이터 추가
            AddToDB(newUser);
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