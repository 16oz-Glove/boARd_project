using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class UserRegDate : MonoBehaviour
{
    public Text RegDate;
    private DataSnapshot snapshot;

    // 마이페이지 열릴 때 DB에서 읽어오기
    void Start()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        string key = AuthManager.User.UserId;
        FirebaseDatabase.DefaultInstance.GetReference("users/" + key + "/regDate").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Cannot read DB.");
            }
            else if (task.IsCompleted)
            {
                snapshot = task.Result;
            }
        });
    }

    // 현재 로그인한 유저의 가입날짜 가져와 세팅
    public void OnClickEditPanel()
    {
        string data = snapshot.Value.ToString();
        RegDate.GetComponent<Text>().text = data.Substring(0, 4) + "/" + data.Substring(4, 2) + "/" + data.Substring(6);
    }
}
