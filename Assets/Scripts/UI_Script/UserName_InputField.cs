using UnityEngine;
using UnityEngine.UI;

public class UserName_InputField : MonoBehaviour
{
    public InputField userName;

    // 현재 로그인한 유저 이름 가져와 세팅
    void Start()
    {
        userName.GetComponent<InputField>().text = AuthManager.User.DisplayName;
    }
}
