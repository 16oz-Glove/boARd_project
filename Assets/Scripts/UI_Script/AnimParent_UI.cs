using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimParent_UI : MonoBehaviour
{
    public GameObject uiPanel;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //코루틴함수를 이용해 0.5초 기다렸다가 비활성화
    public void OnClickButton_all_animationDelay()
    {

        //오브젝트가 활성화 되어 있다면
        if (uiPanel.activeSelf)
        {
            StartCoroutine(CloseAfterDelay());
        }
        else
        {
            uiPanel.SetActive(true);    //활성화
        }
    }

    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("close");
    }

}
