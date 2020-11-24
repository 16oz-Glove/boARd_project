using System;
using UnityEngine;
using Firebase.Database;

public class LogRecord : MonoBehaviour
{
    // 뱅 튜토리얼 입장 시
    public void OnClick_tt()
    {
        AddLog("tt");
    }

    // 뱅 연습게임 입장 시
    public void OnClick_pg()
    {
        AddLog("pg");
    }

    private void AddLog(string type)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        string date = DateTime.Now.ToString("yyyyMMdd HH:mm");
        Logs log = new Logs(date, BoardName.Name_Scene, type); // log setting

        // 로그 개수 읽어오기 --> 키 값
        string key = AuthManager.User.UserId;
        FirebaseDatabase.DefaultInstance.GetReference("logs/" + key).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Cannot read DB.");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                string num = Convert.ToInt32(snapshot.ChildrenCount).ToString();
                string json = JsonUtility.ToJson(log);
                reference.Child("logs").Child(key).Child(num).SetRawJsonValueAsync(json); // push log
                Debug.Log("Succesfully added log to DB.");
            }
        });
    }
}
