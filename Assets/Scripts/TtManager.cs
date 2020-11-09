using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TtManager : MonoBehaviour
{
    static int count = 0;
    private Image script_jobcardIMG;
    //private Text txt;
    public GameObject txtObject;

    // Start is called before the first frame update
    void Start()
    {
        txtObject = transform.Find("Text");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void text_flow()
    {

        Text txt = txtObject.getComponent<Text>();

        switch (count)
        {
            case 0:
                txt.text = "튜토리얼을 시작합니다";
                break;
            case 1:
                txt.text = "직업카드를 인식시켜 주세요";
                script_jobcardIMG = GameObject.Find("Image_Jobcard").GetComponent<Image>(); // Image_Jobcard 오브젝트에 연결된 Image 스크립트를 가져온다
                script_jobcardIMG.enable();  // 스크립트의 enable()함수를 호출한다.
                break;
        };
    }

    public void IncCount() //"다음"버튼 눌렀을때
    {
        if (count >= 0)
        {
            count += 1;
            text_flow();
        }
    }

    public void DecCount() //"이전"버튼 눌렀을때
    {
        if (count > 0)
        {
            count -= 1;
            text_flow();
        }
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
