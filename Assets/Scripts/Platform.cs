using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D platform;
    public void Awake()
    {
        platform = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") < 0)
        {
            platform.surfaceArc = 0;
            if (Input.GetButtonDown("Jump"))
            {
                //platform.surfaceArc = 0;
            }
        }
        else
        {
            platform.surfaceArc = 90;
        }
    }
}
