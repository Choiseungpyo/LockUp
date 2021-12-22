using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    public GameObject ghost;

    static public Ghost instance;
    private void Awake()
    {
        instance = this;
    }


}//End Class
