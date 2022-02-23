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

    public Transform myPos;
    public int crowPosIndex;

    public bool isFollowing = true;
    [SerializeField] private float followSpeed = 3;

    public GameManager _gMan;

    private bool pheonix;
    [SerializeField] private float pheonixTime = 5;

    private bool shrink;
    private float xScale, yScale;

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

        _gMan.sending += ChangePos;

    }

    void FixedUpdate()
    {
        if (isFollowing)
        {
            myRb.velocity = new Vector2(0, 0);
            transform.position = Vector2.MoveTowards(transform.position, myPos.position, followSpeed * Time.deltaTime);
        }

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, ground); // Check if grounded
        //FlipSprite();

        if (!isFollowing && !pheonix)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");
            myRb.velocity = new Vector2(moveX * speed, moveY * speed);

            timeLimit -= Time.deltaTime;
            int seconds = (int)timeLimit % 60;
            floatingNumber.text = seconds.ToString();
            if (timeLimit <= 0)
            {
                _gMan.EmptyCurrentCharacter();
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                GetComponentInChildren<MeshRenderer>().enabled = false;
                pheonixTime = 5;
                pheonix = true;
            }
        }

        if (pheonix && !isFollowing)
        {
            pheonixTime -= Time.deltaTime;
            if (pheonixTime <= 0)
            {
                isFollowing = true;
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponentInChildren<SpriteRenderer>().enabled = true;
                GetComponentInChildren<MeshRenderer>().enabled = true;
                timeLimit = 10;
                floatingNumber.text = timeLimit.ToString();
                _gMan.crowQueue.Enqueue(gameObject);

                transform.position = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
                xScale = 3;
                yScale = 3;
                transform.localScale = new Vector2(xScale, yScale);
                shrink = true;

                for (int i = 0; i < _gMan.possibleCharacters.Length; i++)
                {
                    if (_gMan.possibleCharacters[i].GetComponent<CrowController_Script>().crowPosIndex == crowPosIndex)
                    {
                        pheonix = false;
                        return;
                    }
                }

                //ChangePos();
                pheonix = false;
            }
        }
       
        if (shrink)
        {
            xScale -= Time.deltaTime;
            yScale -= Time.deltaTime;

            transform.localScale = new Vector2(xScale, yScale);

            if (yScale <= 1)
            {
                xScale = 1;
                yScale = 1;
                shrink = false;
            }
            
        }


    }

    public void ChangePos()
    {
        crowPosIndex--;
        
        if (crowPosIndex < 0)
        {
            crowPosIndex = _gMan.crowPos.Length - 1;
        }
        myPos = _gMan.crowPos[crowPosIndex];
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
