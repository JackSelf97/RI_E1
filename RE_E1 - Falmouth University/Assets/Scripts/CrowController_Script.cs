using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowController_Script : MonoBehaviour
{
    public float speed;
    private float moveX;
    private float moveY;
    private Rigidbody2D myRb;

    public TextMesh floatingNumber;
    private float timeLimit = 10;

    public Transform CrowPos;
    public bool isFollowing = true;
    [SerializeField] private float followSpeed = 3;

    #region Jumping
    public float jumpForce;
    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask ground;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    #endregion

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 144;
    }

    // Start is called before the first frame update
    void Start()
    {
        isFollowing = true;
        myRb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(9, 9);
    }

    void FixedUpdate()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, ground); // Check if grounded
                                                                                     //FlipSprite();
        if (!isFollowing)
        {
            timeLimit -= Time.deltaTime;
            int seconds = (int)timeLimit % 60;
            floatingNumber.text = seconds.ToString();
            if (timeLimit <= 0)
            {
                Debug.Log("END");
            }
        }


        if (!isFollowing)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");
            myRb.velocity = new Vector2(moveX * speed, moveY * speed);
        }
        else
        {
            myRb.velocity = new Vector2(0, 0); // stops them moving!!!!!!!!!!!
            transform.position = Vector2.MoveTowards(transform.position, CrowPos.position, followSpeed * Time.deltaTime);

        }

    }

    private void FlipSprite()
    {
        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveX < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
