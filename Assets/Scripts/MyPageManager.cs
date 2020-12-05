using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class MyPageManager : MonoBehaviour
{
	// 메인화면 오브젝트
	public GameObject messageButton;
	public GameObject messagePanel;
	public Text contentText;
	public InputField roomInput, nickNameInput;
	public GameObject waitingPanel;

	// 마이페이지 오브젝트
	public GameObject myPagePanel;
	public Button myLogButton;
	public Button myFriendButton;
	public GameObject friendsScroll;
	public GameObject logsScroll;

	public GameObject friend_prefab;
	public GameObject friend_parent;
	public GameObject log_prefab;
	public GameObject log_parent;

	private string key = null;
	private string room = null;
	private DataSnapshot message_snapshot = null;
	private DataSnapshot log_snapshot;
	private DataSnapshot friend_snapshot;

	// 메인화면 초기화
	void Start()
	{
		messageButton.SetActive(false);
		messagePanel.SetActive(false);
		waitingPanel.SetActive(false);
		myPagePanel.SetActive(false);

		log_snapshot = null;
		friend_snapshot = null;

	FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://test-board-1158b.firebaseio.com/");
		CheckMessage(); // 초대 메세지 온 거 있는지 확인
	}

	// 메인화면에서,
	// 프로파일 눌렀을 때 마이페이지 오픈하면서 초기화
	public void OnClickProfile()
	{
		friendsScroll.SetActive(false);
		logsScroll.SetActive(false);

		myFriendButton.interactable = false;
		myLogButton.interactable = false;

		// DB 열람
		Read_Friends();
		Read_Logs();
	}

	//----------------------------------------------------------------------
	// 초대 메세지 확인
	private void CheckMessage()
	{
		FirebaseDatabase.DefaultInstance.GetReference("messages").OrderByChild("receiver").EqualTo(AuthManager.User.DisplayName).GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted)
			{
				Debug.LogError("Cannot read DB.");
			}
			else if (task.IsCompleted)
			{
				if (task.Result.Value != null) // DataSnapshot은 null이 없다!!!
				{
					message_snapshot = task.Result;
					messageButton.SetActive(true);
				}
			}
		});
		// 이벤트 리스너(지속적으로 확인하라)
		FirebaseDatabase.DefaultInstance.GetReference("messages").OrderByChild("receiver").EqualTo(AuthManager.User.DisplayName).ChildAdded += HandleChildAdded;
	}

	// 이벤트 핸들러
	private void HandleChildAdded(object sender, ChildChangedEventArgs args)
	{
		if (args.DatabaseError != null)
		{
			Debug.LogError(args.DatabaseError.Message);
			return;
		}
		if (args.Snapshot != null)
		{
			message_snapshot = args.Snapshot;
			messageButton.SetActive(true);
			Debug.Log("New messages arrived.");
		}
	}

	// 메세지 버튼을 누르면 메세지 패널 팝업
	public void OnClickMessage()
	{
		foreach (DataSnapshot data in message_snapshot.Children) // 우선 초대 메세지는 하나만 온다고 가정
		{
			IDictionary item = (IDictionary)data.Value;
			key = data.Key;
			room = item["content"].ToString();
			Debug.Log(key + ": Invitation to room name, " + room);
			contentText.GetComponent<Text>().text = string.Format("To. {0}\r\n\r\n우리 연습게임하자.\r\n방(\"{1}\")으로 들어와!\r\n\r\nFrom. {2}", item["receiver"].ToString(), room, item["sender"].ToString());
		}
		messageButton.SetActive(false);
		messagePanel.SetActive(true);
	}

	// 해당 초대메세지 수락 시
	public void OnClickYesOnMessage()
	{
		roomInput.GetComponent<InputField>().text = room;
		nickNameInput.GetComponent<InputField>().text = AuthManager.User.DisplayName;
		deleteMessage();
		waitingPanel.SetActive(true);
	}

	// 해당 초대메세지DB 삭제
	public void deleteMessage()
	{
		if (key != null)
		{
			FirebaseDatabase.DefaultInstance.GetReference("messages/" + key).RemoveValueAsync();
		}
	}

	//----------------------------------------------------------------------
	// 마이페이지에서,
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

				IDictionary friend = (IDictionary)data.Value;
				go.transform.Find("friendName").GetComponent<Text>().text = friend["name"].ToString();
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

	// DB에서 친구 목록 읽어오기
	private void Read_Friends()
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
				Debug.Log("Success to read friends from DB.");
				friend_snapshot = task.Result;
				myFriendButton.interactable = true;
			}
		});
	}

	// DB에서 로그 기록 읽어오기
	private void Read_Logs()
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
				Debug.Log("Success to read logs from DB.");
				log_snapshot = task.Result;
				myLogButton.interactable = true;
			}
		});
	}

	//----------------------------------------------
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
}
