using UnityEngine;
using UnityEngine.UI;

public class UserEmail : MonoBehaviour
{
    public Text userEmail;

    // 현재 로그인한 유저 이메일 가져와 세팅
    void Start()
    {
        userEmail.GetComponent<Text>().text = AuthManager.User.Email;
    }
}
