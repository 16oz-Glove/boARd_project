using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCard : MonoBehaviour  //게임카드들의 부모 클래스 (공통기능)
{

    private GameObject use_Button;  // '사용' 버튼
    public static GameObject game_obj;     //카드 인식하면 나타나는 3D오브젝트
    public static GameObject game_obj2;
    protected GameObject minimap;     //미니맵 버튼

    PlayerSet playerSet;
    UIManager instance;
    private int[] far = new int[5];

    void Awake()
    {
        use_Button = GameObject.Find("Canvas").transform.Find("Panel").transform.Find("use_Button").gameObject;
        minimap = GameObject.Find("Canvas").transform.Find("Panel").transform.Find("Minimap").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Update_Object();
    }

    void Update()
    {
        // 터치 입력이 들어올 경우
        if (Input.GetMouseButtonDown(0))
        {
            touchClick();
        }

    }
    
    // 터치 시 오브젝트 확인 함수
    void touchClick()
    {
         
        // 오브젝트 정보를 담을 변수 생성
        RaycastHit hit;

        // 터치 좌표를 담는 변수
        Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 터치한 곳에 ray를 보냄
        Physics.Raycast(touchray, out hit);

        // ray가 오브젝트에 부딪힐 경우
        if (hit.collider != null)
        {
            Update_Object();
            if (hit.collider.name == game_obj.name)
            {
                Debug.Log(game_obj.name + "오브젝트 터치");
                game_obj2 = game_obj;
                use_Button.SetActive(true);   //버튼 활성화
            }
        }

    }

    protected void Update_Object()
    {
        game_obj = gameObject;
    }

    public virtual void Effect()
    {
        Debug.Log(game_obj2.name + "이(가) 사용되었습니다");
    }
}
