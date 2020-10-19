using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//버튼 누를시, 등록된 오브젝트 활성화 및 비활성화 시키는 스크립트.
public class bActive : MonoBehaviour
{
    public GameObject uiPanel;

    //버튼 누를시, 오브젝트 비활성화
    public void OnClickButton_unActive()
    {
        uiPanel.SetActive(false);
    }

    //버튼 누를시, 오브젝트 활성화
    public void OnClickButton_Active()
    {
        uiPanel.SetActive(true);
    }

}
