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


	// 프로파일 닫을 때 프리팹 삭제(content오브젝트에 직접 넣어 자식오브젝트로 접근해야할 듯)
	public void OnClickCloseProfile()
    {
		// 인스턴스 모두 삭제!
    }

	// 마이로그 버튼 눌렀을 때
	public void OnClickLogs()
    {
		friendsScroll.SetActive(false);
		logsScroll.SetActive(true);
		
		foreach (DataSnapshot data in snapshot.Children)
        {
			GameObject go = Instantiate(log_prefab, transform.position, transform.rotation);
			go.transform.SetParent(log_parent.transform);
			
			IDictionary logs = (IDictionary)data.Value;
			string date = logs["date"].ToString().Substring(4, 2) + "/" + logs["date"].ToString().Substring(6, 2) + " ";
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
