using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject FirstBtn;
    public GameObject RelatedToAttack;
    public GameObject RelatedToSkill;

    //공격 관련
    [HideInInspector]
    public bool attackBtnIsClicked = false;
    [HideInInspector]
    public int countAtAttack;
    public Text countText;

    //스킬 관련
    [HideInInspector]
    public bool skillBtnIsClicked = false;


    bool[] btnIsWorking = new bool[4];




    static public UIManager instance;
    private void Awake()
    {
        instance = this;
        RelatedToAttack.SetActive(false);
        RelatedToSkill.SetActive(false);
        countAtAttack = 3;
        
        for(int i=0; i<4; i++)
        {
            btnIsWorking[i] = false;
        }
        btnIsWorking[0] = attackBtnIsClicked;
        btnIsWorking[1] = skillBtnIsClicked;
    }

    private void Update()
    {
        btnIsWorking[0] = attackBtnIsClicked;
        btnIsWorking[1] = skillBtnIsClicked;
    }

    //유령과 만나게 될 시 누르는 버튼 동작
    public void attackBtn()
    { 
        if (checkBtnIsWorking() == false)
        {
            if (attackBtnIsClicked == false)
            {
                Attack.instance.MakeCompartment();
                attackBtnIsClicked = true;
            }
        }   
    }

    public void SkillBtn()
    {
        if (checkBtnIsWorking() == false)
        {
            if (skillBtnIsClicked == false)
            {
                FirstBtn.SetActive(false);
                RelatedToSkill.SetActive(true);

                skillBtnIsClicked = true;
            }
        }    
    }

    void ItemBtn()
    {

    }

    void RunBtn()
    {

    }

    //공격 관련 
    public void CountDownAtAttak()
    {
        countText.text = countAtAttack.ToString();
        Debug.Log("카운트 다운 함수 동작");
        --countAtAttack;
    }

    //스킬 관련
    public void LockUp()
    {
        //LockUp하는 함수 동작시키기
        Skill.instance.MakeVariousColorCompartment();

        //가두기와 포획 UI 없애기
        RelatedToSkill.SetActive(false);
        Skill.instance.useSkill1 = true;
    }

    public void Capture()
    {
        //Capture하는 함수 동작시키기
        //가두기와 포획 UI 없애기
        RelatedToSkill.SetActive(false);
        Skill.instance.useSkill2 = true;
        Skill.instance.MakeVariousColorCompartment();
    }





    //다른 버튼을 이미 눌러 동작중인지 체크한다.
    bool checkBtnIsWorking()
    {
        for(int i=0; i<4; i++)
        {
            if(btnIsWorking[i] == true)
            {
                return true; //동작중
            }
        }
        return false; //동작x
    }


    public void ActivateFirstBtn()
    {
        skillBtnIsClicked = false;
        FirstBtn.SetActive(true);
    }
}//End Class
