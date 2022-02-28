using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Script : MonoBehaviour
{
    [SerializeField] private GameObject[] crow = new GameObject[3];

    public float speed;
    private float moveInput;
    private Rigidbody2D myRb;

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
        myRb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (GameManager.gMan.isAlive)
            myRb.velocity = new Vector2(moveInput * speed, myRb.velocity.y);
        else if (!GameManager.gMan.isAlive)
            HaltMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gMan.isAlive)
        {
            isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, ground); // Check if grounded
            moveInput = Input.GetAxisRaw("Horizontal");
            FlipSprite();
            JumpAction();
        }
    }

    private void JumpAction()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            myRb.velocity = Vector2.up * jumpForce;
            FindObjectOfType<AudioManager>().PlaySound("Jump");
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                myRb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    private void FlipSprite()
    {
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void HaltMovement()
    {
        myRb.velocity = new Vector2(0, 0);
    }
}
