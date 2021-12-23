using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public GameObject VariousColorCompartment;//프리팹
    public GameObject SelectedBorder; //프리팹
    public GameObject CompartmentForLockUp; //프리팹

    GameObject selectedBorder; //실제 사용 오브젝트
    GameObject compartmentForLockUp; //실제 사용 오브젝트
    GameObject variousColorCompartment; //실제 사용 오브젝트

    bool colorIsSelected = false;
    bool moveSelectedBorder = false;
    bool useSkill1 = false;
    int selectedColorIndex = 0;

    float[] variousColorCompartmentPos = new float[9];

    //색깔
    Vector4 colorRed = new Vector4(1, 0, 0, 1); //빨
    Vector4 colorOrange = new Vector4(0, 140/255f,0,1); //주
    Vector4 colorYellow = new Vector4(1, 225/255f, 0,1); //노
    Vector4 colorGreen = new Vector4(35/255f, 170/255f, 20/255f, 1); //초
    Vector4 colorBlue = new Vector4(0, 190/255f, 1, 1); //파
    Vector4 colorIndigo = new Vector4(20/255f, 75/255f, 165/255f, 1); //남
    Vector4 colorPurple = new Vector4(120/255f, 25/255f, 165/255f, 1); //보
    Vector4 colorWhite = new Vector4(1,1,1,1); //흰
    Vector4 colorBlack = new Vector4(0,0,0,1); //검


    static public Skill instance;
    private void Awake()
    {
        instance = this;

        //-1.35 -> -0.45 -> 0.45 -> 1.35
        //-1.8 -> -0.9 -> 0 -> 0.9 -> 1.8
        variousColorCompartmentPos[0] = -1.35f;
        variousColorCompartmentPos[1] = -0.45f;
        variousColorCompartmentPos[2] =  0.45f;
        variousColorCompartmentPos[3] =  1.35f;
        variousColorCompartmentPos[4] = -1.8f;
        variousColorCompartmentPos[5] = -0.9f;
        variousColorCompartmentPos[6] =  0f;
        variousColorCompartmentPos[7] =  0.9f;
        variousColorCompartmentPos[8] =  1.8f;

    }

    private void Update()
    {
        if (moveSelectedBorder == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                colorIsSelected = true;
                moveSelectedBorder = false;
            }
        }

    }







        //스킬 2개 중 선택하게 하기
        //스킬 1 - 가두기
        //1. 색깔선택 : 왼->오른쪽으로 선택칸 이동
        //2. 가두기 : 유령을 해당 공간안에 집어넣는다.

    public void MakeVariousColorCompartment() //여러색깔칸 만들기
    {
        variousColorCompartment = Instantiate(VariousColorCompartment, new Vector3(0, -3.5f, 0), Quaternion.identity);
        MakeSelectedBorder();
    }

    
    void MakeSelectedBorder() //칸을 선택하는 테두리를 좌에서우로 계속 이동시킨다.
    {
        selectedBorder = Instantiate(SelectedBorder, new Vector3(variousColorCompartmentPos[0], -2.5f, 0), Quaternion.identity);
        moveSelectedBorder = true;
        StartCoroutine(MoveSelectedBorder());
        //-1.35 -> -0.45 -> 0.45 -> 1.35 (0.9씩 더해진다.)
        //-1.8 -> -0.9 -> 0 -> 0.9 -> 1.8
    }

    void LockUpGhost()
    {
        if(useSkill1 == false)
        {
            compartmentForLockUp = Instantiate(CompartmentForLockUp, Vector3.zero, Quaternion.identity);
            useSkill1 = true;
        }  
        compartmentForLockUp.GetComponent<SpriteRenderer>().material.color = ChangeColorIndexToColorString();
        Debug.Log(ChangeColorIndexToColorString());
    }



    //스킬 2 - 포획



    IEnumerator MoveSelectedBorder()
    {
        for(int i=0; i<9; i++)
        {
            if(i<4)
            {
                selectedBorder.transform.position = new Vector3(variousColorCompartmentPos[i], -2.5f, 0);         
            }
            else
            {
                selectedBorder.transform.position = new Vector3(variousColorCompartmentPos[i], -3.5f, 0);
            }

            if (colorIsSelected == true) //색깔을 선택했다면 코루틴 종료
            {
                selectedColorIndex = i;
                Debug.Log("색깔 인덱스:" + i);
                Invoke("LockUpGhost", 1.5f);
                Invoke("ClearScreenRegardingSkill1", 1.5f);
                break;
            }

            yield return new WaitForSeconds(0.25f);

            if(i==8) //무한 반복
            {
                i = -1;
            }
        }
    }


    Vector4 ChangeColorIndexToColorString()
    {
        switch(selectedColorIndex)
        {
            case 0: //빨
                return colorRed;
            case 1: //주
                return colorOrange;
            case 2: //노
                return colorYellow;
            case 3: //초
                return colorGreen;
            case 4: //파
                return colorBlue;
            case 5: //남
                return colorIndigo;
            case 6: //보
                return colorPurple;
            case 7: //흰
                return colorWhite;
            case 8: //검
                return colorBlack;
            default:
                return colorWhite;
        }
    }

    void ClearScreenRegardingSkill1() //스킬1에 대해여 필요없는 오브젝트 지우기
    {
        Destroy(variousColorCompartment);
        Destroy(selectedBorder);
        UIManager.instance.ActivateFirstBtn();
        colorIsSelected = false;
    }

    





}//End Class
