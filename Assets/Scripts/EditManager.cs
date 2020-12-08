using System;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class EditManager : MonoBehaviour
{
    public GameObject editPanel;
    public GameObject completePanel;
    public InputField nickNameField;
    public InputField current_pwField;
    public InputField new_pwField;
    public InputField new_pwCheckField;

    [SerializeField] private string currentPW;
    [SerializeField] private string newPW;

    private bool IsSuccessOnReAuth;
    private FirebaseUser user;

    void Start()
    {
		editPanel.SetActive(false);
        IsSuccessOnReAuth = false;
    }

    public void OnClickSave()
    {
        // 변경 내용 없을 경우
        if (new_pwCheckField.text.Length == 0 && nickNameField.text.Length == 0)
        {
            return;
        }

        // 변경 내용 있다면, 사용자 재인증부터
        currentPW = current_pwField.text;
        ReAuth();

        if (new_pwCheckField.text.Length != 0) // 비밀번호 변경
        {
            Debug.Log("new_pwCheckField.text is not null: "+ new_pwCheckField.text);
            if (new_pwField.text == new_pwCheckField.text)
            {
                newPW = new_pwCheckField.text;
                Invoke("ChangePW",1f);
            }
            else
            {
                Debug.Log("Check the new password you input again.");
            }
        }
        if (nickNameField.text.Length != 0) // 닉네임 변경
        {
            Invoke("ChangeNickName",1f);
        }

        Invoke("CompleteMessage", 1f);
    }

    // 사용자 재인증
    private void ReAuth()
    {
        user = AuthManager.firebaseAuth.CurrentUser;
        Credential credential = EmailAuthProvider.GetCredential(user.Email, currentPW);
        if (user != null)
        {
            user.ReauthenticateAsync(credential).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("ReauthenticateAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("ReauthenticateAsync encountered an error: " + task.Exception);
                    return;
                }
                Debug.Log("Successfully reauthenticated.");
                IsSuccessOnReAuth = true;
            });
        }
    }

    // 비밀번호 변경하기
    private void ChangePW()
    {
        Debug.Log("ChangePW() is started");
        if (user != null && IsSuccessOnReAuth == true)
        {
            AuthManager.User.UpdatePasswordAsync(newPW).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdatePasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdatePasswordAsync encountered an error: " + task.Exception);
                    return;
                }
                Debug.Log("Password updated successfully.");
            });
        }
    }

    // 닉네임 변경하기
    private void ChangeNickName()
    {
        Debug.Log("ChangeNickName() is started");
        if (user != null && IsSuccessOnReAuth == true)
        {
            // Auth의 DisplayName 바꾸기
            UserProfile profile = new UserProfile { DisplayName = nickNameField.text };
            AuthManager.User.UpdateUserProfileAsync(profile).ContinueWith(task => {
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
                Debug.Log("Successfully changed at Auth: " + AuthManager.User.DisplayName);
            });

            // DB의 닉네임 바꾸기
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

            string key = AuthManager.User.UserId;
            reference.Child("users").Child(key).Child("nickName").SetValueAsync(nickNameField.text);
            Debug.Log("Succesfully changed at DB.");
        }
    }

    // 완료 패널 활성화
    private void CompleteMessage()
    {
        editPanel.SetActive(false);
        completePanel.SetActive(true);

        ResetForm();
    }

    public void ResetForm()
    {
        nickNameField.text = null;
        current_pwField.text = null;
        new_pwField.text = null;
        new_pwCheckField.text = null;

        currentPW = null;
        newPW = null;

        user = null;
    }

    // 변경 완료 시, 재접속 요청(로그아웃)
    public void OnClickOkay()
    {
        AuthManager.firebaseAuth.SignOut();
        AuthManager.User = null;
        SceneManager.LoadScene("SignIn");
        Debug.Log("Log-out");
    }
}
