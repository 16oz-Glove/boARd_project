using UnityEngine;
/*
 * 이 스크립트는 나중에
 * 조이스틱으로 사용자가 움직일 수 있도록 수정한다.
 * 
 */
public class InputButton : MonoBehaviour
{
    //Input값을눌렀냐 누르지않았냐(화면을 터치했냐 터치하지 않았냐)에 따라 적절한 값 설정.
    public static float VerticalInput;

    //현재 어떤 버튼을 클릭하고 있냐를 표현
    public enum State
    {
        None,
        Down,
        Up
    }

    private State state = State.None;

    private void Update()
    {
        if (state == State.None)
        {
            VerticalInput = 0f;
        }
        else if (state == State.Up)
        {
            VerticalInput = 1f;
        }
        else if (state == State.Down)
        {
            VerticalInput = -1f;
        }
    }

    //손을 누르는 순간에
    public void OnMoveUpButtonPressed()
    {
        state = State.Up;
    }

    //손을 떼는 순간에
    public void OnMoveUpButtonUp()
    {
        //아래쪽으로 가는 버튼과 위쪽으로가는 버튼을 동시에 누르고 있는데, 위쪽으로 가는 버튼을 때는 경우 방지
        if (state == State.Up)
        {
            state = State.None;
        }
    }

    //손을 누르는 순간에
    public void OnMoveDownButtonPressed()
    {
        state = State.Down;
    }

    //손을 떼는 순간에
    public void OnMoveDownButtonUp()
    {
        //아래쪽으로 가는 버튼과 위쪽으로가는 버튼을 동시에 누르고 있는데, 위쪽으로 가는 버튼을 때는 경우 방지
        if (state == State.Down)
        {
            state = State.None;
        }
    }
}