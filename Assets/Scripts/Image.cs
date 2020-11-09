using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image : MonoBehaviour
{
    private float fDestroyTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("disable", 10f); //10초 후에 오브젝트 비활성화
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enable()
    {
        gameObject.SetActive(true); //오브젝트 활성화
        Invoke("disable", 5);   //5초 후 비활성화
    }

    void disable() 
    {
        gameObject.SetActive(false);    //오브젝트 비활성화
    }
}
