using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class AddFriend : MonoBehaviour
{
    public GameObject addFriendPanel;
    public InputField nickNameField;
    public GameObject prefab;
    public GameObject parent;

    private DataSnapshot snapshot;

    private void Start()
    {
        addFriendPanel.SetActive(false);
    }

    // 마이페이지에서 add 버튼 눌렀을 때 패널 활성화
    public void OnClickAddOfMypage()
    {
        addFriendPanel.SetActive(true);
        // 활성화되면 이전에 생성해놓은 프리팹 있다면 삭제하는 거 구현해야 함
    }

    // 검색하기 버튼 눌렀을 경우
    public void OnClickSearch()
    {
        SearchDB();
        Invoke("InstantiatePrefab", 1);
    }

    // 닉네임으로 DB에서 이메일을 검색해라
    private void SearchDB()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("nickName").EqualTo(nickNameField.text).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Cannot read DB.");
            }
            else if (task.IsCompleted)
            {
                snapshot = task.Result;
                Debug.Log("Succesfully searched.");
                InstantiatePrefab();
            }
        });
    }

    // 프리팹으로 결과 노출하기
    private void InstantiatePrefab()
    {
        if (snapshot == null)
        {
            Debug.Log("There's no such nickName.");
        }
        else
        {
            foreach (DataSnapshot data in snapshot.Children)
            {
                GameObject go = Instantiate(prefab, transform.position, transform.rotation);
                go.transform.SetParent(parent.transform);

                IDictionary item = (IDictionary)data.Value;
                Debug.Log(item);
                go.transform.Find("text").GetComponent<Text>().text = item["email"].ToString();
                Debug.Log("Instantiated.");
            }
        }
    }

    // 친구 추가 패널에서 add버튼 눌렀을 경우
    public void OnClickAddOfAddFriendPanel()
    {
        // 이거 프리팹에다 스크립트 추가해서 같이 구현해야 할 듯. 고민해보기.
    }

}
