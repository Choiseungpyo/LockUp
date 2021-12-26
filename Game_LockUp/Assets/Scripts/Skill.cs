using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    //프리팹
    public GameObject VariousColorCompartment;
    public GameObject SelectedBorder; 
    public GameObject CompartmentForLockUp;
    public GameObject Selected4ScaleBoder;
    public GameObject RestrictedSpace9AndSelected4Space;

    //실제 사용 오브젝트
    GameObject selectedBorder; 
    GameObject compartmentForLockUp; 
    GameObject variousColorCompartment;
    GameObject selected4ScaleBorder;
    GameObject restrictedSpace9AndSelected4Space;

    bool colorIsSelected = false;
    bool moveSelectedBorder = false;
    [HideInInspector]
    public bool useSkill1 = false;
    int selectedColorIndex = 0;
    bool lockUpFirstTime = false;

    //스킬2
    bool spaceIsSelected = false;
    [HideInInspector]
    public bool useSkill2 = false;
    int ghostRandPos;
    int selectedSpaceNum;
    [HideInInspector]
    public bool ghostIsCaptured = false;

    float[] variousColorCompartmentPos = new float[9];

    //색깔
    [HideInInspector]
    public Vector4 colorRed = new Vector4(1, 0, 0, 1); //빨
    [HideInInspector]
    public Vector4 colorOrange = new Vector4(0, 140/255f,0,1); //주
    [HideInInspector]
    public Vector4 colorYellow = new Vector4(1, 225/255f, 0,1); //노
    [HideInInspector]
    public Vector4 colorGreen = new Vector4(35/255f, 170/255f, 20/255f, 1); //초
    [HideInInspector]
    public Vector4 colorBlue = new Vector4(0, 190/255f, 1, 1); //파
    [HideInInspector]
    public Vector4 colorIndigo = new Vector4(20/255f, 75/255f, 165/255f, 1); //남
    [HideInInspector]
    public Vector4 colorPurple = new Vector4(120/255f, 25/255f, 165/255f, 1); //보
    [HideInInspector]
    public Vector4 colorWhite = new Vector4(1,1,1,1); //흰
    [HideInInspector]
    public Vector4 colorBlack = new Vector4(0,0,0,1); //검


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
            //모바일 터치로 변환해야할 부분
            if (Input.GetKeyDown(KeyCode.Space))
            {
                colorIsSelected = true;
                moveSelectedBorder = false;
            }

            //if (Input.touchCount > 0)
            //{
            //    Touch touch = Input.GetTouch(0);

            //    if(touch.phase == TouchPhase.Began)
            //    {
            //        colorIsSelected = true;
            //        moveSelectedBorder = false;
            //    }
            //}
        }

        if(useSkill2 == true)
        {
            Choose4ScaleCompartment();
        }

        if(ghostIsCaptured == true)
        {
            Destroy(compartmentForLockUp, 1);
            lockUpFirstTime = false;
        }
    }







    //스킬 2개 중 선택하게 하기
    //스킬 1 - 가두기
    //1. 색깔선택 : 왼->오른쪽으로 선택칸 이동
    //2. 가두기 : 유령을 해당 공간안에 집어넣는다.

    //스킬 2 - 포획
    //1. 색깔 선택:
    //2. 포획 : 2x2 정사각형 4군데 중 한 정사각형 선택


    //스킬1
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
        if(lockUpFirstTime == false)
        {
            compartmentForLockUp = Instantiate(CompartmentForLockUp, Vector3.zero, Quaternion.identity);
            lockUpFirstTime = true;
        }  
        compartmentForLockUp.GetComponent<SpriteRenderer>().material.color = ChangeColorIndexToColorString();
        Debug.Log(ChangeColorIndexToColorString());
    }



    //스킬 2 - 포획
    public void MakeCompartment9()
    {
        restrictedSpace9AndSelected4Space = Instantiate(RestrictedSpace9AndSelected4Space, Vector3.zero, Quaternion.identity);

        Choose4ScaleCompartment();
    }

    void Choose4ScaleCompartment()
    {
        if (Player.instance.ClickedObj == null)
        {
            return;
        }
        //1 2 3
        //4 5 6
        //7 8 9

        //1,2,4,5 -> 정사각형 1
        //2,3,5,6 -> 정사각형 2
        //4,5,7,8 -> 정사각형 3
        //5,6,8,9 -> 정사각형 4

        while (spaceIsSelected == false)
        {      
            for (int i = 1; i <= 4; i++)
            {
                if (Player.instance.ClickedObj.name.Contains(i.ToString()))
                {
                    selectedSpaceNum = i;
                    switch (i)
                    {
                        case 1:
                            selected4ScaleBorder = Instantiate(Selected4ScaleBoder, new Vector3(-0.7f, 0.6f, 0), Quaternion.identity);
                            selected4ScaleBorder.GetComponent<SpriteRenderer>().color = ChangeColorIndexToColorString();
                            spaceIsSelected = true;
                            Debug.Log("1");
                            Invoke("SetGhostRandPos", 1);
                            break; ;
                        case 2:
                            selected4ScaleBorder = Instantiate(Selected4ScaleBoder, new Vector3(0.7f, 0.6f, 0), Quaternion.identity);
                            selected4ScaleBorder.GetComponent<SpriteRenderer>().color = ChangeColorIndexToColorString();
                            spaceIsSelected = true;
                            Debug.Log("2");
                            Invoke("SetGhostRandPos", 1);
                            break; ;
                        case 3:
                            selected4ScaleBorder = Instantiate(Selected4ScaleBoder, new Vector3(-0.7f, -0.7f, 0), Quaternion.identity);
                            selected4ScaleBorder.GetComponent<SpriteRenderer>().color = ChangeColorIndexToColorString();
                            spaceIsSelected = true;
                            Debug.Log("3");
                            Invoke("SetGhostRandPos", 1);
                            break; ;
                        case 4:
                            selected4ScaleBorder = Instantiate(Selected4ScaleBoder, new Vector3(0.7f, -0.7f, 0), Quaternion.identity);
                            selected4ScaleBorder.GetComponent<SpriteRenderer>().color = ChangeColorIndexToColorString();
                            spaceIsSelected = true;
                            Debug.Log("4");
                            Invoke("SetGhostRandPos", 1);
                            break; ;
                    }
                }
            }
        }   
    }

    void SetGhostRandPos()
    {
        ghostRandPos = Random.Range(1, 10); //1~9
        Debug.Log("유령이 선택한 칸:" + ghostRandPos);
        Ghost.instance.ghost.transform.position = GameObject.Find(ghostRandPos.ToString()).transform.position;
        CompareSelectedSpaceToGhostPos();
    }

    void CompareSelectedSpaceToGhostPos()
    {
        switch(selectedSpaceNum)
        {
            case 1:
                if (ghostRandPos == 1 || ghostRandPos == 2 || ghostRandPos == 4 || ghostRandPos == 5)
                {
                    Debug.Log("유령과 선택한 공간의 위치가 일치합니다.");
                    ghostIsCaptured = true;
                }
                else
                    Debug.Log("유령과 선택한 공간의 위치가 일치하지 않습니다.");

                Invoke("ClearScreenRegardingSkill2", 1);
                break;
            case 2:
                if (ghostRandPos == 2 || ghostRandPos == 3 || ghostRandPos == 5 || ghostRandPos == 6)
                {
                    Debug.Log("유령과 선택한 공간의 위치가 일치합니다.");
                    ghostIsCaptured = true;
                }
                else
                    Debug.Log("유령과 선택한 공간의 위치가 일치하지 않습니다.");

                Invoke("ClearScreenRegardingSkill2", 1);
                break;
            case 3:
                if (ghostRandPos == 4 || ghostRandPos == 5 || ghostRandPos == 7 || ghostRandPos == 8)
                {
                    Debug.Log("유령과 선택한 공간의 위치가 일치합니다.");
                    ghostIsCaptured = true;
                }
                else
                    Debug.Log("유령과 선택한 공간의 위치가 일치하지 않습니다.");

                Invoke("ClearScreenRegardingSkill2", 1);
                break;
            case 4:
                if (ghostRandPos == 5 || ghostRandPos == 6 || ghostRandPos == 8 || ghostRandPos == 9)
                {
                    Debug.Log("유령과 선택한 공간의 위치가 일치합니다.");
                    ghostIsCaptured = true;
                }
                else
                    Debug.Log("유령과 선택한 공간의 위치가 일치하지 않습니다.");

                Invoke("ClearScreenRegardingSkill2", 1);
                break;
        }
    }




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
                if(useSkill1 == true)
                {
                    selectedColorIndex = i;
                    //Debug.Log("색깔 인덱스:" + i);
                    Invoke("LockUpGhost", 1.5f);
                    Invoke("ClearScreenRegardingSkill1", 1.5f);
                    break;
                }
                
                if(useSkill2 == true)
                {
                    selectedColorIndex = i;
                    Invoke("MakeCompartment9", 1.5f);
                    Destroy(variousColorCompartment,1);
                    Destroy(selectedBorder,1);
                    
                    break;
                }
                
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
        useSkill1 = false;
    }

    void ClearScreenRegardingSkill2()
    {
        //9칸짜리
        Destroy(restrictedSpace9AndSelected4Space);
        //선택칸
        Destroy(selected4ScaleBorder);
        //유령
        if(ghostIsCaptured == true)
        {
            if(Ghost.instance.ghostColor == selectedColorIndex) //유령과 포획 칸의 색깔이 같을때만 유령 포획
            {
                //Destroy(Ghost.instance.ghost);
                Ghost.instance.ghost.transform.position = new Vector3(-100, -100, 0);
                Ghost.instance.ghostIsDead = true;
            }
            else
            {
                ghostIsCaptured = false;
                Debug.Log("유령과 포획 칸의 색깔이 같지 않습니다.");
                Ghost.instance.ghost.transform.position = Vector3.zero;
            }
        }
        else
        {
            Ghost.instance.ghost.transform.position = Vector3.zero;
        }

        UIManager.instance.ActivateFirstBtn();
        colorIsSelected = false;
        useSkill2 = false;
        Player.instance.ClickedObj = null;
        selectedSpaceNum = 0;
        spaceIsSelected = false;
    }




}//End Class
