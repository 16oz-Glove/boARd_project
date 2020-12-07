using UnityEngine;
using UnityEngine.UI;

public class ToggleControl : MonoBehaviour
{
    public Toggle emailToggle;

    // true일 경우 email 전달
    public void sendCheckedEmail()
    {
        if (emailToggle.isOn)
        {
            AddFriend.friendEmail = emailToggle.transform.Find("Label").GetComponent<Text>().text;
            Debug.Log(AddFriend.friendEmail + " is saved from prefab");
        }
        else
        {
            AddFriend.friendEmail = null;
            Debug.Log("Toogle is false");
        }
    }
}
