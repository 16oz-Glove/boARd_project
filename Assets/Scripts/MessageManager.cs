using System;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class MessageManager : MonoBehaviour
{
    public GameObject icon_message;
    public GameObject icon_messageSent;
    public Text FriendName;

    private void Start()
    {
        icon_message.SetActive(true);
        icon_messageSent.SetActive(false);
    }

    // 메세지 아이콘 클릭 시,
    public void OnClickMessage()
    {
        icon_message.SetActive(false);
        icon_messageSent.SetActive(true);
        PushMessage();
    }

    private void PushMessage()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        // setting
        string date = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
        Message message = new Message(AuthManager.User.DisplayName, FriendName.text, date, InviteManager.roomName_Text);
        
        string json = JsonUtility.ToJson(message);
        string key = reference.Child("messages").Push().Key;
        reference.Child("messages").Child(key).SetRawJsonValueAsync(json);
    }
}
