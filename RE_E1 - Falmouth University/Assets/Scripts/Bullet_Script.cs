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
        myRb.velocity = new Vector2(-speed, myRb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            //GameManager.gMan.TakeDamage();
        }

        Destroy(gameObject);
    }
}
