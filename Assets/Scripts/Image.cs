using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Image : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        Thread.Sleep(15000);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
