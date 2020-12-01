using System;
using System.Text;
using UnityEngine;
using Firebase.Database;
using Photon.Pun;

public class LogRecord : MonoBehaviour
{
    // 뱅 튜토리얼 입장 시
    public void OnClick_tt()
    {
        AddLog_tt();
    }

    // 튜토리얼 로그 저장
    private void AddLog_tt()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        string date = DateTime.Now.ToString("yyyyMMdd HH:mm");
        Logs log = new Logs(date, BoardName.Name_Scene, "tt");

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
                Debug.Log("Succesfully added log_tt to DB.");
            }
        });
    }

    // 연습게임 로그 저장: UpdateRoomPlayer.cs에서 사용
    public static void AddLog_pg()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        
        //log setting
        string date = DateTime.Now.ToString("yyyyMMdd HH:mm");
        Logs log = new Logs(date, BoardName.Name_Scene, "pg");
        
        // player setting
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            sb.Append(PhotonNetwork.PlayerList[i].NickName);
            sb.Append(", ");
        }
        string players = sb.ToString().Substring(0, sb.Length - 2);

        // 로그 개수 for 키 값
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
                reference.Child("logs").Child(key).Child(num).Child("players").SetValueAsync(players); // push players to log
                Debug.Log("Succesfully added log_pg to DB.");
            }
        });
    }
}
