using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    public GameObject ghost;
    public GameObject CopyGhost;

    [HideInInspector]
    public int ghostColor;

    [HideInInspector]
    public bool ghostIsDead = false;


    static public Ghost instance;
    private void Awake()
    {
        instance = this;
        ghostColor = 7;
    }
    private void Update()
    {
        if(ghostIsDead == true)
        {
            Debug.Log("유령이 죽었습니다.");
            //ghost = Instantiate(CopyGhost, Vector3.zero, Quaternion.identity);
            ghost.transform.position = Vector3.zero;
            Debug.Log("유령이 재생성합니다.");

            SetRandomGhostColor();
            ghostIsDead = false;
            Skill.instance.ghostIsCaptured = false;
        }
    }

    //유령 랜덤색깔 정하기
    void SetRandomGhostColor()
    {
        int randGhostColor;
        randGhostColor = Random.Range(1, 10); //1~9
        //빨 주 노 초 파 남 보 흰 검
        ChangeGhostColor(randGhostColor);
    }


    void ChangeGhostColor(int randGhostColor)
    {
        ghostColor = randGhostColor;
        switch (randGhostColor)
        {
            case 0:
                ghost.GetComponent<SpriteRenderer>().color = Skill.instance.colorRed;
                break;
            case 1:
                ghost.GetComponent<SpriteRenderer>().color = Skill.instance.colorOrange;
                break;
            case 2:
                ghost.GetComponent<SpriteRenderer>().color = Skill.instance.colorYellow;
                break;
            case 3:
                ghost.GetComponent<SpriteRenderer>().color = Skill.instance.colorGreen;
                break;
            case 4:
                ghost.GetComponent<SpriteRenderer>().color = Skill.instance.colorBlue;
                break;
            case 5:
                ghost.GetComponent<SpriteRenderer>().color = Skill.instance.colorIndigo;
                break;
            case 6:
                ghost.GetComponent<SpriteRenderer>().color = Skill.instance.colorPurple;
                break;
            case 7:
                ghost.GetComponent<SpriteRenderer>().color = Skill.instance.colorWhite;
                break;
            case 8:
                ghost.GetComponent<SpriteRenderer>().color = Skill.instance.colorBlack;
                break;
        }
    }

}//End Class
