using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{

    private float legth, starpos;
    public GameObject Cam;
    public float paralaxEffect;

    private void Awake()
    {
        starpos = transform.position.x;
        legth = GetComponent<SpriteRenderer>().bounds.size.x;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float temp = Cam.transform.position.x * (1 - paralaxEffect);

        float dist = Cam.transform.position.x * paralaxEffect;

        transform.position = new Vector3(starpos + dist, transform.position.y);

        if(temp > starpos + legth)
        {
            starpos += legth;
        }
        else if(temp < starpos - legth)
        {
            starpos -= legth;
        }
    }
}
