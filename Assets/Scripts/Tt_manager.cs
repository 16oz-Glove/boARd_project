using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


public class Tt_manager : MonoBehaviour
{
    static int count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public GameObject[] tutorialPage;


    //[SerializeField] GameObject buttonLeft;
    //[SerializeField] GameObject buttonRight;

    //int currentIndex = 0;

    //public int CurrentIndex
    //{

    //    get
    //    {
    //        return currentIndex;
    //    }
    //    set
    //    {
    //        if (tutorialPage[currentIndex] != null)
    //        {
    //            //set the current active object to inactive, before replacing it
    //            GameObject aktivesObj = tutorialPage[currentIndex];
    //            aktivesObj.SetActive(false);
    //        }

    //        if (value < 0)
    //            currentIndex = tutorialPage.Length - 1;
    //        else if (value > tutorialPage.Length - 1)
    //            currentIndex = 0;
    //        else
    //            currentIndex = value;
    //        if (tutorialPage[currentIndex] != null)
    //        {
    //            GameObject aktivesObj = tutorialPage[currentIndex];
    //            aktivesObj.SetActive(true);
    //        }
    //    }
    //}

    //public void Previous(int direction)
    //{
    //    if (direction == 0)
    //        CurrentIndex--;

    //    if (CurrentIndex <= 3)
    //    {
    //        buttonRight.SetActive(true);

    //        Debug.Log("4");
    //    }

    //    if (CurrentIndex <= 0)
    //    {
    //        buttonLeft.SetActive(false);
    //        Debug.Log("3");
    //    }
    //}



    //public void Next(int direction)
    //{
    //    if (direction >= 1)
    //        CurrentIndex++;

    //    if (CurrentIndex >= 4)
    //    {
    //        buttonRight.SetActive(false);

    //        Debug.Log("2");
    //    }

    //    if (CurrentIndex >= 1)
    //    {
    //        buttonLeft.SetActive(true);
    //        Debug.Log("3");
    //    }

    //}


    //public void OnClickTutorialPanel()
    //{

    //    if (CurrentIndex <= 0)
    //    {
    //        buttonLeft.SetActive(false);
    //        buttonRight.SetActive(true);
    //        Debug.Log("1");

    //    }

    //    tutorialPage[0].SetActive(true);
    //    Tutorial.SetActive(true);
    //}

    //public void OnClickBackToSettings()
    //{
    //    CurrentIndex = 0;

    //    tutorialPage[currentIndex].SetActive(false);
    //    Tutorial.SetActive(false);

    //}

}
