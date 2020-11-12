using UnityEngine;
using UnityEngine.UI;

public class UserName : MonoBehaviour
{
    public Text userName;

    // 현재 로그인한 유저 이름 가져와 세팅
    void Start()
    {
        userName.GetComponent<Text>().text = AuthManager.User.DisplayName;
    }
}
