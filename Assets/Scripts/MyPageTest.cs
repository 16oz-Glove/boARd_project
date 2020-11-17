using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class MyPageTest : MonoBehaviour
{
	public GameObject friendsScroll;
	public GameObject logsScroll;
	public GameObject myPagePanel;
	public Button myLogButton;

	private DataSnapshot snapshot = null;
	public GameObject log_prefab = null;
	public GameObject log_parent = null;

	void Start()
    {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://test-board-1158b.firebaseio.com/");
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

		myPagePanel.SetActive(false);
		friendsScroll.SetActive(false);
		logsScroll.SetActive(false);

		myLogButton.interactable = false;
	}

	// 프로파일 눌렀을 때 DB 열람
	public void OnClickProfile()
    {
		ReadLogsData();
	}


	// 마이페이지 닫을 때
	public void OnClickClosePage()
    {
		// 내부 프리팹 삭제
		for (int i = 0; i < log_parent.transform.childCount; i++)
        {
			Destroy(log_parent.transform.GetChild(i).gameObject);
		}

		// 스크롤 비활성화
		friendsScroll.SetActive(false);
		logsScroll.SetActive(false);

		myLogButton.interactable = false;

	}

	// 마이로그 버튼 눌렀을 때
	public void OnClickLogs()
    {
		friendsScroll.SetActive(false);
		logsScroll.SetActive(true);
		
		if (log_parent.transform.childCount == 0)
        {
			foreach (DataSnapshot data in snapshot.Children)
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

	public void ReadLogsData()
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
				snapshot = task.Result;
				myLogButton.interactable = true;
				Debug.Log("Success to read logs from DB.");
			}
		});
	}
}
