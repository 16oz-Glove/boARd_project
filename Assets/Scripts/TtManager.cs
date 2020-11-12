using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TtManager : MonoBehaviour
{
    static int count = -1;
    private Image script_jobcardIMG, script_charcardIMG;
    private Text txt;
    private GameObject txtObject_panel, txtObject_UI;

    void Start()
    {
        txtObject_panel = transform.Find("Text").gameObject;
        txt = txtObject_panel.GetComponent<Text>();
        txt.text = "튜토리얼을 시작합니다!";
        script_jobcardIMG = GameObject.Find("Canvas").transform.Find("Image_Jobcard").GetComponent<Image>(); // Image_Jobcard 오브젝트에 연결된 Image 스크립트를 가져온다
        script_charcardIMG = GameObject.Find("Canvas").transform.Find("Image_Charcard").GetComponent<Image>(); // Image_Charcard 오브젝트에 연결된 Image 스크립트를 가져온다
        txtObject_UI = GameObject.Find("Canvas").transform.Find("UIText").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void text_flow()     // 이전&다음 버튼 터치에 따른 흐름
    {

        switch (count)
        {
            case 0:
                txt.text = "’뱅!’은 보안관과 부관이 한 팀이 되고 무법자, " +
                    "그리고 배신자와 서로 총격전을 벌이는 게임입니다. \n" +
                    "이 게임은 직업카드 7장, 캐릭터카드 16장, 그리고 행동카드 87장으로 구성되어 있습니다.";
                script_jobcardIMG.disable();  // Image_Jobcard 스크립트의 disable()함수를 호출한다.
                break;
            case 1:
                txt.text = "직업카드는 플레이어들의 포지션을 의미합니다. 바닥에 4장의 직업카드를 놓아 인식시켜 주세요.\n" +
                    "카드가 인식되면 카드 주변에 효과가 나타납니다. 인식을 완료하면 '다음' 버튼을 눌러 주세요.";
                script_jobcardIMG.enable_temp();  // Image_Jobcard 스크립트의 enable_temp()함수를 호출한다.
                break;
            case 2:
                txt.text = "인식된 카드를 터치해 보세요.";
                script_jobcardIMG.disable();  // Image_Jobcard 스크립트의 disable()함수를 호출한다.
                txtObject_UI.SetActive(false);   //txtObject_UI 오브젝트 비활성화
                break;
            case 3:
                txt.text = "플레이어의 수에 맞게 직업카드를 섞은 뒤, 1장씩 무작위로 나누어 가지고 보안관만 자신의 직업을 공개하게 됩니다.";
                txtObject_UI.SetActive(true);   //txtObject_UI 오브젝트 활성화
                script_charcardIMG.disable();  // Image_Charcard 스크립트의 disable()함수를 호출한다.
                break;
            case 4:
                txt.text = "캐릭터카드는 각 플레이어의 능력 및 생명력을 결정합니다. 이미지에 해당하는 캐릭터카드를 바닥에 놓아 인식시켜 주세요. \n" +
                    "카드가 인식되면 카드 주변에 효과가 나타납니다. 인식을 완료하면 '다음' 버튼을 눌러 주세요.";
                script_charcardIMG.enable();  // Image_Charcard 스크립트의 enable()함수를 호출한다.
                txtObject_UI.SetActive(false);   //txtObject_UI 오브젝트 비활성화
                break;
            case 5:
                script_charcardIMG.disable();  // Image_Charcard 스크립트의 disable()함수를 호출한다.
                break;

        };
    }

    public void IncCount() //"다음"버튼 눌렀을때
    {
        if (count >= -1 && count < 5)
        {
            count += 1;
            text_flow();
        }
    }

    public void DecCount() //"이전"버튼 눌렀을때
    {
        if (count > 0 && count <= 5)
        {
            count -= 1;
            text_flow();
        }
    }
}