using UnityEngine;
using UnityEngine.UI;

public class PlayerNameText : MonoBehaviour
{
    private Text nameText;  //UI에 보이고 있는 Text 부분

    private void Start()
    {
        nameText = GetComponent<Text>();

        //AuthManager class에서 static으로 선언한 User를 바로 쓸 수 있음.
        if (AuthManager.User != null)   //User가 잘 할당되었다면
        {
            nameText.text = $"Hi! {AuthManager.User.Email}";
            // 만약, User 정보에 다른 정보를 출력하고 싶다면, 따로 구현하기.
        }
        else            // User가 할당되지 않았다면,
        {
            nameText.text = "ERROR : AuthManager.User == null";
        }

    }
}
