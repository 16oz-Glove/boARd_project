using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAction : MonoBehaviourPun
{
    public float Speed;
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    float h;
    float v;
    bool isHorizonMove;

    //Mobile Key Var
    int up_Value;
    int down_Value;
    int left_Value;
    int right_Value;
    bool up_Down;
    bool down_Down;
    bool left_Down;
    bool right_Down;
    bool up_Up;
    bool down_Up;
    bool left_Up;
    bool right_Up;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //사용자가 로컬이라면
        if (photonView.IsMine)
        {
            spriteRenderer.color = Color.green;
        }
    }

    //매 프레임 사용자 입력을 감지
    void Update()
    {
        //로컬 플레이어가 아닌 경우 입력을 받지 않음
        if (!photonView.IsMine)
        {
            return;
        }
        //PC+ Movile Move Value
        h = Input.GetAxisRaw("Horizontal") + right_Value + left_Value;
        v = Input.GetAxisRaw("Vertical") + up_Value + down_Value;

        /*
        var input = InputButton.VerticalInput;
        var distance = input * Speed * Time.deltaTime;
        var targetPosition = transform.position + Vector3.up * distance;
        */

        //Check Button Down & Up
        bool hDown = Input.GetButtonDown("Horizontal") || right_Down || left_Down;
        bool vDown = Input.GetButtonDown("Vertical") || up_Down || down_Down;
        bool hUp = Input.GetButtonUp("Horizontal") || right_Up || left_Up;
        bool vUp = Input.GetButtonUp("Vertical") || up_Up || down_Up;

        //Check horizontal Move
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        //Animation
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else anim.SetBool("isChange", false);

        //Mobile Var Init
        up_Down = false;
        down_Down = false;
        left_Down = false;
        right_Down = false;
        up_Up = false;
        down_Up = false;
        left_Up = false;
        right_Up = false;


    }

    void FixedUpdate()
    {
        //Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec* Speed;
    }

    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 1;
                up_Down = true;
                break;
            case "D":
                down_Value = -1;
                down_Down = true;
                break;
            case "L":
                left_Value = -1;
                left_Down = true;
                break;
            case "R":
                right_Value = 1;
                right_Down = true;
                break;
        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 0;
                up_Up = true;
                break;
            case "D":
                down_Value = 0;
                down_Up = true;
                break;
            case "L":
                left_Value = 0;
                left_Up = true;
                break;
            case "R":
                right_Value = 0;
                right_Up = true;
                break;
        }
    }

}
