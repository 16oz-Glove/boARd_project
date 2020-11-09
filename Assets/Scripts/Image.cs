using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enable_temp()
    {
        gameObject.SetActive(true); //오브젝트 활성화
        Invoke("disable", 10);   //10초 후 비활성화
    }

    public void enable()
    {
        gameObject.SetActive(true); //오브젝트 활성화
    }

    public void disable() 
    {
        gameObject.SetActive(false);    //오브젝트 비활성화
    }
}
