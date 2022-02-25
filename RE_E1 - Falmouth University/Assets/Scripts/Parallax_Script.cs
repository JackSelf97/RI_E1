using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_Script : MonoBehaviour
{
    private float length, startPos, yPos;
    public GameObject cam;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        yPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);
        float ydistance = (cam.transform.position.y * parallaxEffect);
        transform.position = new Vector3(startPos + distance, yPos + ydistance, transform.position.z);

        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
