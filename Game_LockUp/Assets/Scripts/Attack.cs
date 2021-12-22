using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject Compartment;
    GameObject createdCompartment;
    [HideInInspector]
    public bool isClickedAttackBtn = false;
    [HideInInspector]
    public int[] clickedCompartment = new int[5];

    [HideInInspector]
    public bool playerPickAllOfThem = false;

    int randPos;
    bool alreadyWorking = false;

    static public Attack instance;
    private void Awake()
    {
        instance = this;

        for(int i=0; i<5; i++)
        {
            clickedCompartment[i] = 0;
        }
    }

    private void Update()
    {
        if (isClickedAttackBtn == true)
        {
            if (playerPickAllOfThem != true)
            {
                Choose5Compartment();
            }
            if(playerPickAllOfThem == true && alreadyWorking == false) //playerPickAllOfThem == true && 한번만 동작되도록
            {
                UIManager.instance.countText.enabled = true;
                StartCoroutine(CountDown());
                Debug.Log("코루틴 이후");
                Invoke("ChooseGhostRandPos", 3);
                Invoke("CompareGhostAndPlayerCompartment",3);
                Invoke("ResetVariable", 5);
                alreadyWorking = true;
            }
        }
    }

    
    public void MakeCompartment()//9개의 칸 만들기
    {
        createdCompartment =Instantiate(Compartment, new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log("칸 생성완료");
    }

    
    void ChooseGhostRandPos()//유령의 랜덤 칸 선택
    {
        randPos = Random.Range(1, 10); //1~9
        Debug.Log("유령이 선택한 칸:" + randPos);
        Ghost.instance.ghost.transform.position = GameObject.Find(randPos.ToString()).transform.position;
    }

    
    public void Choose5Compartment()//플레이어는 칸 5개 선택
    {
        if (Player.instance.ClickedObj == null) //클릭한 오브젝트가 없을 경우
        {
            return;
        }
           
        Debug.Log("choose 함수 동작 + clickedObj != null");
        for (int i=1; i<=9; i++)
        {
            if (Player.instance.ClickedObj.name.Contains(i.ToString()) == true)
            {
                for(int a=0; a<5; a++)
                {
                    if (clickedCompartment[a] == 0) //비워져있어야한다.
                    {
                        for(int b=0;b<5; b++)
                        {
                            if(clickedCompartment[b] == i) //이전칸에 이미 저장되어있는 칸 번호일경우
                            {
                                return;
                            }
                        }

                        clickedCompartment[a] = i;
                        Player.instance.ClickedObj.GetComponent<SpriteRenderer>().material.color = Color.red;
                        Debug.Log("선택한 칸 :"+ i);
                        if(a == 4)
                        {
                            Debug.Log("전부 찼습니다.");
                            playerPickAllOfThem = true;
                        }
                        return;
                    }
                }       
            }
        }   
    }

    void CompareGhostAndPlayerCompartment() //유령과 플레이어의 선택한 칸 비교
    {
        for(int i=0; i<5; i++)
        {
            if (randPos == clickedCompartment[i])
            {
                Debug.Log("플레이어가 선택한 칸과 유령이 선택한 칸이 같습니다.");
                break;
            }
            else
            {
                if(i==4)
                {
                    Debug.Log("플레이어가 선택한 칸과 유령이 선택한 칸이 다릅니다.");
                }   
            }
        }
    }

    void ResetVariable()
    {    
        for(int i=0; i<5; i++)
        {
            clickedCompartment[i] = 0;
        }

        isClickedAttackBtn = false;
        playerPickAllOfThem = false;
        alreadyWorking = false;

        for(int i=1; i<=9; i++)
        {
            GameObject.Find(i.ToString()).GetComponent<SpriteRenderer>().material.color = Color.white;
        }
        Ghost.instance.ghost.transform.position = new Vector3(0, 0, 0);
        Player.instance.ClickedObj = null;
        Destroy(createdCompartment);
        Debug.Log("리셋되었습니다.");
    }

    IEnumerator CountDown()
    {
        for(int i=0; i<3; i++)
        {
            UIManager.instance.CountDownAtAttak();

            yield return new WaitForSeconds(1.0f);
        }
        UIManager.instance.countAtAttack = 3;
        UIManager.instance.countText.enabled = false;
    }
}//End Class
