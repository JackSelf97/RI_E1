using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    private Rigidbody2D myRb;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myRb.velocity = new Vector2(speed, myRb.velocity.y);
        
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Kill player
            GameManager.gMan.timesDiedNo++;
            GameManager.gMan.RespawnAtLastCheckpoint();
        }
        if (collision.gameObject.tag == "Crow")
        {
            // Kill Crow
            GameManager.gMan.crowsLostNo++;
            collision.GetComponent<CrowController_Script>().timeLimit = 0;
        }

        Destroy(gameObject);
    }
}
