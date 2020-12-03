using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class InviteManager : MonoBehaviour
{
    public GameObject lobbyPanel;
    public InputField roomName;
    public Button inviteButton;
    public GameObject friendsScroll;
    public GameObject prefab_invite;
    public GameObject prefab_parent;

    public static string roomName_Text;
    private DataSnapshot friends = null;

    // Start is called before the first frame update
    void Start()
    {
        lobbyPanel.SetActive(false);
        inviteButton.interactable = false;
        friendsScroll.SetActive(false);
    }

    // 연습게임 버튼 클릭 시, 로비 패널 열리면서 DB 읽어오기
    public void OnClickPGbutton()
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
                friends = task.Result;
                Debug.Log("Success to read friends from DB.");
            }
        });
    }

    // 방 이름 작성되면 초대 버튼 활성화
    public void OnWritingRoomName()
    {
        if (roomName.text != null)
        {
            roomName_Text = roomName.text;
            inviteButton.interactable = true;
        }
    }

    // 친구 초대하기 버튼 클릭 시, 스크롤 오픈 & 프리팹 생성
    public void OnClickInviteFriend()
    {
        friendsScroll.SetActive(true);

        if (prefab_parent.transform.childCount == 0)
        {
            foreach (DataSnapshot data in friends.Children)
            {
                GameObject go = Instantiate(prefab_invite, transform.position, transform.rotation);
                go.transform.SetParent(prefab_parent.transform);

                IDictionary friend = (IDictionary)data.Value;
                go.transform.Find("friendName").GetComponent<Text>().text = friend["name"].ToString();
            }
        }
    }
}
