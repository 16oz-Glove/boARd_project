using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guideUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // guideCanvas가 활성화되어있고 터치 입력이 들어올 경우
        if (Input.GetMouseButtonDown(0) && gameObject.activeSelf)
        {
            gameObject.SetActive(false);                 //guideCanvas 비활성화
        }

    }
}
