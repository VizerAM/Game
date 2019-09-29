using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaPlantControler : MonoBehaviour
{
    public Transform ShellPosition;
    public GameObject Shell;

    public bool Right;

    private void Awake()
    {
        if(Right)
        {
            transform.localScale = new Vector3( transform.localScale.x * -1, transform.localScale.y);
        }
        
    }

    public void Shot()
    {
        GameObject temp = Instantiate(Shell);

        temp.transform.position = ShellPosition.position;

        temp.transform.SetParent(transform);

        if (Right)
        {
            temp.GetComponent<Shell>().Direction = Vector3.right;
        }
        else
        {
            temp.GetComponent<Shell>().Direction = Vector3.left;
        }


    }

}
