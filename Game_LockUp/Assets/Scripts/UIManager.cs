using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text countText;

    [HideInInspector]
    public int countAtAttack = 3;

    static public UIManager instance;
    private void Awake()
    {
        instance = this;
        countText.enabled = false;
    }

    //유령과 만나게 될 시 누르는 버튼 동작
    public void attack()
    { 
        if (Attack.instance.isClickedAttackBtn == false)
        {
            Attack.instance.MakeCompartment();
            Attack.instance.isClickedAttackBtn = true;
        }
      
    }

    void Skill()
    {

    }

    void Item()
    {

    }

    void Run()
    {

    }

    public void CountDownAtAttak()
    {
        countText.text = countAtAttack.ToString();
        Debug.Log("카운트 다운 함수 동작");
        --countAtAttack;
    }
}//End Class
