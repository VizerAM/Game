using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private PlatformEffector2D platform;
    private LayerMask layerMask;

    private void Start()
    {
        platform = GetComponent<PlatformEffector2D>();
        layerMask = platform.colliderMask;
    }

    void Update()
    {
        if(Input.GetAxis("Vertical") < 0)
        {
            if(Input.GetButtonDown("Jump"))
             platform.colliderMask -= LayerMask.GetMask("Player");
        }
        else
        {
            platform.colliderMask = layerMask;
        }
    }
}
