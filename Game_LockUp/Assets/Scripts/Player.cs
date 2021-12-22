using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public GameObject ClickedObj = null; 

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
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero, 0f,LayerMask.GetMask("Compartment"));
            if (hit.collider != null)
            {
                ClickedObj = hit.transform.gameObject;
            }
        }
    }
}//End Class
