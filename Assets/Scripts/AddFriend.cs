using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class AddFriend : MonoBehaviour
{
    public GameObject myPagePanel;
    public GameObject addFriendPanel;
    public GameObject completePanel;
    public InputField nickNameField;
    public GameObject prefab;
    public GameObject parent;

    //private DataSnapshot snapshot;
    private List<IDictionary> snapshotList = new List<IDictionary>();
    public static string friendEmail;

    private void Start()
    {
        addFriendPanel.SetActive(false);
        completePanel.SetActive(false);
    }

    // 검색하기 버튼 눌렀을 경우
    public void OnClickSearch()
    {
        SearchDB();
        Invoke("InstantiatePrefab", 1f); // 함수 지연
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
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary item = (IDictionary)data.Value;
                    item.Add("uid", data.Key);
                    snapshotList.Add(item);
                }
                Debug.Log("Succesfully searched.");
            }
        });
    }

    // 프리팹으로 결과 노출하기
    private void InstantiatePrefab()
    {
        Debug.Log("Instantiating Started.");
        if (snapshotList == null)
        {
            Debug.Log("There's no such nickName.");
        }
        else
        {
            for(int i = 0; i < snapshotList.Count; i++)
            {
                GameObject go = Instantiate(prefab, transform.position, transform.rotation);
                go.transform.SetParent(parent.transform);
                go.transform.Find("Label").GetComponent<Text>().text = snapshotList[i]["email"].ToString();
                Debug.Log("Instantiated.");
            }
        }
    }

    // 친구 추가 패널에서 add버튼 눌렀을 경우
    public void OnClickAddOfAddFriendPanel()
    {
        Debug.Log("Checked Email is " + friendEmail);
        if (friendEmail == null)
        {
            return;
        }
        
        // 선택한 이메일의 친구를 추가하라
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        for (int i = 0; i < snapshotList.Count; i++)
        {
            if (snapshotList[i]["email"].ToString() == friendEmail)
            {
                // 세팅
                Friend friend = new Friend(snapshotList[i]["uid"].ToString(), snapshotList[i]["nickName"].ToString());

                // 친구 수 읽어오기 --> 키 값
                string key = AuthManager.User.UserId;
                FirebaseDatabase.DefaultInstance.GetReference("users/" + key + "/friends").GetValueAsync().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.LogError("Cannot read DB.");
                    }
                    else if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        string num = Convert.ToInt32(snapshot.ChildrenCount).ToString(); // 친구 수
                        string json = JsonUtility.ToJson(friend);
                        reference.Child("users").Child(key).Child("friends").Child(num).SetRawJsonValueAsync(json); // push friend
                        Debug.Log("Succesfully added friend to DB.");
                    }
                });
            }
        }
        Debug.Log("Let pages be non-active");
        completePanel.SetActive(true);
        addFriendPanel.SetActive(false);
        myPagePanel.SetActive(false);
    }

    public void OnClosePage()
    {
        nickNameField.text = null;

        // 내부 프리팹 삭제
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
    }
}


