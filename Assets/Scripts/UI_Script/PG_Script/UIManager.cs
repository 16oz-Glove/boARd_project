using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{ 
    //UIManager 타입의 오브젝트를 다른 스크립트에서 즉시 접근할 수 있또록 정적 프로퍼티 instance와 정적 변수 m_instance로 싱글턴으로 구현
    public static UIManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }
    private static UIManager m_instance;

    [Header("각 플레이어까지의 거리를 나타내는 텍스트")]
    public Text[] Player_far_text = new Text[5];

    [Header("생명력 이미지 저장 변수")]
    public GameObject[] life_img = new GameObject[5];

    [Header("플레이어 직업과 캐릭터 텍스트 변수")]
    public Text jobName;       //플레이어 직업 
    public Text CharacterName;  //플레이어 캐릭터 카드
    public Text ability;   //플레이어 캐릭터카드의 효과


    public void Update_Life(int life)
    {
        for (int i = 0; i <= life - 1; i++)//생명력 만큼 총알UI 띄워주기
        {
            life_img[i].SetActive(true);
        }
        for (int i = life; i < 5; i++)//생명력 깍인만큼 총알UI 띄워주기
        {
            life_img[i].SetActive(false);
        }
    }

    public void Update_Far(int[] far)
    {
        for (int i = 0; i < 5; i++)
        {
            Player_far_text[i].text = far[i].ToString();
        }
    }

    public void Update_PlayerInfo(string job, string character,string character_ab)
    {
        jobName.text = job;     //직업
        CharacterName.text = character; //캐릭터
        ability.text = character_ab;    //캐릭터의 능력
    }


}
