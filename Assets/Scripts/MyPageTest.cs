using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class MyPageTest : MonoBehaviour
{
	public GameObject myPagePanel;
	public GameObject friendsScroll;
	public GameObject logsScroll;

	public Button myLogButton;
	public Button myFriendButton;

	public GameObject friend_prefab = null;
	public GameObject friend_parent = null;
	public GameObject log_prefab = null;
	public GameObject log_parent = null;

	private DataSnapshot log_snapshot = null;
	private DataSnapshot friend_snapshot = null;

	void Start()
    {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://test-board-1158b.firebaseio.com/");
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

		myPagePanel.SetActive(false);
		friendsScroll.SetActive(false);
		logsScroll.SetActive(false);

		myFriendButton.interactable = false;
		myLogButton.interactable = false;
	}

	// 프로파일 눌렀을 때 DB 열람
	public void OnClickProfile()
    {
		ReadFriendsData();
		ReadLogsData();
	}

	// 마이페이지 닫을 때
	public void OnClickClosePage()
    {
		// 내부 프리팹 삭제
		for (int i = 0; i < friend_parent.transform.childCount; i++)
		{
			Destroy(friend_parent.transform.GetChild(i).gameObject);
		}
		for (int i = 0; i < log_parent.transform.childCount; i++)
        {
			Destroy(log_parent.transform.GetChild(i).gameObject);
		}

		// 스크롤 비활성화
		friendsScroll.SetActive(false);
		logsScroll.SetActive(false);

		myFriendButton.interactable = false;
		myLogButton.interactable = false;

	}

	// 친구목록 버튼 눌렀을 때
	public void OnClickFriends()
	{
		friendsScroll.SetActive(true);
		logsScroll.SetActive(false);

		if (friend_parent.transform.childCount == 0)
		{
			foreach (DataSnapshot data in friend_snapshot.Children)
			{
				GameObject go = Instantiate(friend_prefab, transform.position, transform.rotation);
				go.transform.SetParent(friend_parent.transform);

				IDictionary logs = (IDictionary)data.Value;
				go.transform.Find("friendName").GetComponent<Text>().text = logs["name"].ToString();
			}
		}
	}

	// 마이로그 버튼 눌렀을 때
	public void OnClickLogs()
    {
		friendsScroll.SetActive(false);
		logsScroll.SetActive(true);
		
		if (log_parent.transform.childCount == 0)
        {
			foreach (DataSnapshot data in log_snapshot.Children)
			{
				GameObject go = Instantiate(log_prefab, transform.position, transform.rotation);
				go.transform.SetParent(log_parent.transform);
			
				IDictionary logs = (IDictionary)data.Value;
				string date = logs["date"].ToString().Substring(4, 2) + "/" + logs["date"].ToString().Substring(6, 8) + " ";
				if (logs["type"].Equals("tt"))
				{
					go.GetComponent<Text>().text = date + logs["game"] + "(튜토리얼)";
				}
				else if (logs["type"].Equals("pg"))
				{
					go.GetComponent<Text>().text = date + logs["game"] + "(연습게임) " + logs["players"];
				}
				else
				{
					go.GetComponent<Text>().text = date + " " + logs["game"];
					Debug.Log("Wrong game type in DB");
				}
			}
        }
	}

	// DB에서 친구정보 읽어오기
	private void ReadFriendsData()
	{
		string key = AuthManager.User.UserId;
		FirebaseDatabase.DefaultInstance.GetReference("users/" + key + "/friends").GetValueAsync().ContinueWith(task =>
		{
			if (task.IsFaulted)
			{
				Debug.LogError("Cannot read friends from DB.");
			}
			else if (task.IsCompleted)
			{
				friend_snapshot = task.Result;
				myFriendButton.interactable = true;
				Debug.Log("Success to read friends from DB.");
			}
		});
	}

	// DB에서 로그 기록 읽어오기
	private void ReadLogsData()
	{
		string key = AuthManager.User.UserId;
		FirebaseDatabase.DefaultInstance.GetReference("logs/" + key).GetValueAsync().ContinueWith(task =>
		{
			if (task.IsFaulted)
			{
				Debug.LogError("Cannot read log from DB.");
			}
			else if (task.IsCompleted)
			{
				log_snapshot = task.Result;
				myLogButton.interactable = true;
				Debug.Log("Success to read logs from DB.");
			}
		});
	}

}
