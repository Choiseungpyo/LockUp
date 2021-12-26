using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public GameObject ClickedObj = null;


    RaycastHit2D hit;

    static public Player instance;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        CheckClickedObj();
    }

    void CheckClickedObj()
    {
        //모바일 터치로 변환해야할 부분
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (UIManager.instance.attackBtnIsClicked == true)
            {
                //공격 버튼 클릭시 - compartment만 감지되게
                hit = Physics2D.Raycast(touchPos, Vector2.zero, 0f, LayerMask.GetMask("Compartment"));
            }
            else if (UIManager.instance.skillBtnIsClicked == true)
            {
                //스클 버튼 클릭시 - 번호 선택하는 곳만 감지되게
                hit = Physics2D.Raycast(touchPos, Vector2.zero, 0f, LayerMask.GetMask("SelectedSpace"));
                //Debug.Log("Layer : selectedSpace");
            }
            else
            {
                //나머지 경우는 일단 compartment만 감지되게 설정
                hit = Physics2D.Raycast(touchPos, Vector2.zero, 0f, LayerMask.GetMask("Compartment"));
            }

            if (hit.collider != null)
            {
                ClickedObj = hit.transform.gameObject;
            }
        }


        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        if (UIManager.instance.attackBtnIsClicked == true)
        //        {
        //            //공격 버튼 클릭시 - compartment만 감지되게
        //            hit = Physics2D.Raycast(touchPos, Vector2.zero, 0f, LayerMask.GetMask("Compartment"));
        //        }
        //        else if (UIManager.instance.skillBtnIsClicked == true)
        //        {
        //            //스클 버튼 클릭시 - 번호 선택하는 곳만 감지되게
        //            hit = Physics2D.Raycast(touchPos, Vector2.zero, 0f, LayerMask.GetMask("SelectedSpace"));
        //            //Debug.Log("Layer : selectedSpace");
        //        }
        //        else
        //        {
        //            //나머지 경우는 일단 compartment만 감지되게 설정
        //            hit = Physics2D.Raycast(touchPos, Vector2.zero, 0f, LayerMask.GetMask("Compartment"));
        //        }

        //        if (hit.collider != null)
        //        {
        //            ClickedObj = hit.transform.gameObject;
        //        }
        //    }
        //}
    }
}//End Class
