using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private PlatformEffector2D platform;
    public void Awake()
    {
        platform = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") == 0)
        {
            platform.surfaceArc = 45;
        }
        else
        {
            platform.surfaceArc = 0;
        }
        
    }
}
