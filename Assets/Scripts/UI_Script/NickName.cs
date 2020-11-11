using UnityEngine;
using UnityEngine.UI;

public class NickName : MonoBehaviour
{
    public Text userName;

    void Start()
    {
        userName.GetComponent<Text>().text = AuthManager.User.DisplayName;
    }
}
