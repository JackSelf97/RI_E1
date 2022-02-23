using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower_Script : MonoBehaviour
{
    // Controller
    private CrowController_Script crowCont;

    // Follower Variables
    public Transform CrowPos;
    private Rigidbody2D myRb;
    [SerializeField] private float followSpeed = 3;
    //public bool isFollowing = true;

    private void Start()
    {
        //isFollowing = true;
        crowCont = GetComponent<CrowController_Script>();
        myRb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(9, 9);
    }

    // Update is called once per frame
    void Update()
    {
        if (crowCont.enabled == false/* && isFollowing == true*/)
        {
            transform.position = Vector2.MoveTowards(transform.position, CrowPos.position, followSpeed * Time.deltaTime);
            //transform.position = Vector2.MoveTowards(transform.position, new Vector2(CrowPos.position.x, transform.position.y), followSpeed * Time.deltaTime); // crows don't follow on the y-axis
        }
    }
}
