using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    public GameObject ghost;
    public GameObject CopyGhost;

    [HideInInspector]
    public bool ghostIsDead = false;


    static public Ghost instance;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if(ghostIsDead == true)
        {
            Debug.Log("유령이 죽었습니다.");
            ghost = Instantiate(CopyGhost, Vector3.zero, Quaternion.identity);
            Debug.Log("유령이 재생성합니다.");

            ghostIsDead = false;
        }
    }

}//End Class
