using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Follower script
public class CrowController_Script : MonoBehaviour
{
    // Controller
    private Controller_Script controller;

    // Follower Variables
    public Transform CrowPos;
    private Rigidbody2D myRb;
    [SerializeField] private float followSpeed = 3;
    public bool isFollowing = true;

    private void Start()
    {
        isFollowing = true;
        controller = GetComponent<Controller_Script>();
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (controller.enabled == false && isFollowing == true)
        {
            //transform.position = Vector2.MoveTowards(transform.position, CrowPos.position, speed * Time.deltaTime);

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(CrowPos.position.x, transform.position.y), followSpeed * Time.deltaTime); // crows don't follow on the y-axis
        }
            
        if (myRb.velocity.y < 0 && !controller.enabled) // falling
        {
            isFollowing = false;
        }

        if (myRb.velocity.y == 0 && !controller.enabled)
        {
            isFollowing = true;
        }
    }
}
