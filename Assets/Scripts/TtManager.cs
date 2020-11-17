 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TtManager : MonoBehaviour
{
    static int count = -1;
    private Image script_jobcardIMG, script_charcardIMG, script_playingcardIMG, script_minimap, script_minimapCard;
    private GameObject txtObject_UI;
    public AudioSource[] audioSources = new AudioSource[8];
    public TypingEffect talk;

    void Start()
    {
        talk.SetMsg("튜토리얼을 시작합니다!");

        audioSources[0].Play(); //오디오 실행

        script_jobcardIMG = GameObject.Find("Canvas").transform.Find("Image_Jobcard").GetComponent<Image>(); // Image_Jobcard 오브젝트에 연결된 Image 스크립트를 가져온다
        script_charcardIMG = GameObject.Find("Canvas").transform.Find("Image_Charcard").GetComponent<Image>(); // Image_Charcard 오브젝트에 연결된 Image 스크립트를 가져온다
        script_playingcardIMG = GameObject.Find("Canvas").transform.Find("Image_Playingcard").GetComponent<Image>(); // Image_Playingcard 오브젝트에 연결된 Image 스크립트를 가져온다
        script_minimap = GameObject.Find("Canvas").transform.Find("Image_Minimap").GetComponent<Image>(); // Image_Minimap 오브젝트에 연결된 Image 스크립트를 가져온다
        script_minimapCard = GameObject.Find("Canvas").transform.Find("Image_MinimapCard").GetComponent<Image>(); // Image_MinimapCard 오브젝트에 연결된 Image 스크립트를 가져온다
        
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
                talk.SetMsg("’뱅!’은 무법자 집단과 보안관이 펼치는 서부시대 풍의 총격전 카드게임입니다. \n" +
                    "부관은 신분을 감춘 채로 보안관을 돕지만, 배신자 또한 자신만의 목표를 위해 호시탐탐 기회를 노립니다!\n" +
                    "각각의 플레이어는 이들 중 한 명이 되어 게임에 참가하게 됩니다.");
                audioSources[0].Stop(); //오디오 멈춤
                audioSources[2].Stop(); //오디오 멈춤
                audioSources[1].Play(); //오디오 실행
                break;
            case 1:
                talk.SetMsg("뱅 게임은 7장의 직업카드, 16장의 캐릭터카드, 80장의 행동카드로 구성되어 있습니다.\n" +
                    "세 종류의 카드들은 저마다 뚜렷한 특징이 존재합니다.");
                audioSources[1].Stop(); //오디오 멈춤
                audioSources[3].Stop(); //오디오 멈춤
                audioSources[2].Play(); //오디오 실행
                script_jobcardIMG.disable();  // 직업카드 Image 스크립트의 disable()함수를 호출한다.
                break;
            case 2:
                talk.SetMsg("직업카드는 플레이어들의 포지션을 의미합니다.바닥에 4장의 직업카드를 놓아 인식시켜 주세요.\n" +
                    "카드가 인식되면 카드 위에 3D 오브젝트가 나타납니다. 오브젝트를 터치해 보세요.");
                audioSources[2].Stop(); //오디오 멈춤
                audioSources[3].Play(); //오디오 실행
                script_jobcardIMG.enable_temp();  // 직업카드 Image 스크립트의 enable_temp()함수를 호출한다.
                txtObject_UI.SetActive(false);   //txtObject_UI 오브젝트 비활성화
                break;
            case 3:
                talk.SetMsg("화면에 뜬 텍스트는 플레이어 수에 따라 분배하는 직업카드 종류와 장수입니다.\n" +
                    "플레이어의 수에 맞게 직업카드를 섞은 뒤, 1장씩 무작위로 나누어 가지고 보안관만 자신의 직업을 공개하게 됩니다.");
                audioSources[3].Stop(); //오디오 멈춤
                audioSources[4].Stop(); //오디오 멈춤
                txtObject_UI.SetActive(true);   //txtObject_UI 오브젝트 활성화
                script_jobcardIMG.disable();  // 직업카드 Image 스크립트의 disable()함수를 호출한다.
                script_charcardIMG.disable();  // 캐릭터카드 Image 스크립트의 disable()함수를 호출한다.
                break;
            case 4:
                talk.SetMsg("캐릭터카드는 각 플레이어의 능력 및 생명력을 결정합니다. 이미지에 해당하는 캐릭터카드를 바닥에 놓아 인식시켜 주세요. \n" +
                    "카드가 인식되면 카드 위에 3D 오브젝트가 나타납니다. 오브젝트를 터치해 보세요.");
                audioSources[5].Stop(); //오디오 멈춤
                audioSources[4].Play(); //오디오 실행
                script_charcardIMG.enable_temp();  // 캐릭터카드 Image 스크립트의 enable_temp()함수를 호출한다.
                txtObject_UI.SetActive(false);   //txtObject_UI 오브젝트 비활성화
                break;
            case 5:
                talk.SetMsg("캐릭터카드에 표시된 총알 개수는 캐릭터의 최대 생명력을 의미하며, 플레이어는 그만큼의 총알 토큰을 가지게 됩니다. \n" +
                    "또한 플레이어는 행동카드를 생명력보다 많이 보유할 수 없습니다. \n" +
                    "(단, 직업이 보안관일 경우, 생명력이 +1 됩니다.)");
                audioSources[4].Stop(); //오디오 멈춤
                audioSources[6].Stop(); //오디오 멈춤
                audioSources[5].Play(); //오디오 실행
                script_charcardIMG.disable();  // 캐릭터카드 Image 스크립트의 disable()함수를 호출한다.
                script_playingcardIMG.disable();  // 행동카드 Image 스크립트의 disable()함수를 호출한다.
                break;
            case 6:
                talk.SetMsg("행동카드는 실질적인 게임 진행에 사용되며, 각각의 행동카드는 저마다의 기능을 가지고 있습니다. \n" +
                    "이미지에 해당하는 캐릭터카드를 바닥에 놓아 인식시켜 주세요. \n" +
                    "카드가 인식되면 카드 위에 3D 오브젝트가 나타납니다. 오브젝트를 터치해 보세요.");
                audioSources[5].Stop(); //오디오 멈춤
                audioSources[7].Stop(); //오디오 멈춤
                audioSources[6].Play(); //오디오 실행
                script_playingcardIMG.enable_temp();  // 행동카드 Image 스크립트의 enable_temp()함수를 호출한다.
                break;
            case 7:
                talk.SetMsg("갈색의 행동카드는 일회성으로, 자신의 차례에서 원하는 만큼 사용할 수 있습니다. \n" +
                    "무기 카드가 포함된 파란색의 장착카드는 최대 1개까지만 장착할 수 있고 지속적인 효과를 부여합니다.");
                audioSources[6].Stop(); //오디오 멈춤
                audioSources[7].Play(); //오디오 실행
                script_playingcardIMG.disable();  // 행동카드 Image 스크립트의 disable()함수를 호출한다.
                script_minimap.disable();  // 미니맵 Image 스크립트의 disable()함수를 호출한다.
                break;
            case 8:
                talk.SetMsg("뱅 게임 플레이를 위해 꼭 알아 두어야 하는 것이 '거리' 개념입니다.\n" +
                    "거리는 말 그대로 '플레이어 간의 거리'입니다. \n" +
                    "그림에서 나와 A,D 사이의 거리는 1이고, 나와 B,C 사이의 거리는 2가 됩니다.");
                audioSources[7].Stop(); //오디오 멈춤
                script_minimap.enable();  // 미니맵 Image 스크립트의 enable()함수를 호출한다.
                script_minimapCard.disable();  // 미니맵카드 Image 스크립트의 disable()함수를 호출한다.
                break;
            case 9:
                talk.SetMsg("예를 들어 내가 '볼캐닉' 무기 카드를 장착 중이고 '뱅' 행동카드를 사용하려 한다면, 나와 거리가 1 이하인 플레이어, 즉 A,D에게 사용 가능합니다.\n" +
                    "이처럼 거리 개념은 일부 행동 카드(특히 무기 카드)의 사용과 직결되므로 꼭 숙지한 후 게임을 플레이해 주세요.");
                script_minimap.enable();  // 미니맵 Image 스크립트의 enable()함수를 호출한다.
                script_minimapCard.enable();  // 미니맵카드 Image 스크립트의 enable()함수를 호출한다.
                break;
            case 10:
                talk.SetMsg("'뱅!' 게임 플레이를 위한 기본 개념 설명은 여기까지입니다. \n" +
                    "메인 화면에서 '보드게임 AR 설명' -> '연습플레이' 기능을 실행해 보세요. \n" +
                    "다른 유저들과 직접 맛보기 게임을 플레이해보며 AR과 함께 게임 룰을 직접 익힐 수 있습니다!");
                script_minimap.disable();  // 미니맵 Image 스크립트의 disable()함수를 호출한다.
                script_minimapCard.disable();  // 미니맵카드 Image 스크립트의 disable()함수를 호출한다.
                break;
        };
    }

    public void IncCount() //"다음"버튼 눌렀을때
    {
        if (count >= -1 && count < 10)
        {
            count += 1;
            text_flow();
        }
    }

    public void DecCount() //"이전"버튼 눌렀을때
    {
        if (count > 0 && count <= 10)
        {
            count -= 1;
            text_flow();
        }
    }
}