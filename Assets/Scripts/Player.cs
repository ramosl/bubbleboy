using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D myRB;

    private Animator myAnim;

    private bool facingRight;
    private bool isAttacking;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private Transform[] groundPoints;

    private bool isGrounded;
    private bool isJump;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    public int bubbleCount;

    public bool infiniteBubbles;

    public AudioClip BlowClip;
    public AudioSource BlowSource;

    public AudioClip JumpClip;
    public AudioSource JumpSource;

    private GameObject[] bubbles;
    public int bubbleChoice;

    public GameObject LevelParams;
    
    public int[] bubbCosts;
    private int[] curBubbCosts;

    // Use this for initialization
    void Start()
    {
        BlowSource.clip = BlowClip;
        JumpSource.clip = JumpClip;

        // This is all for picking the bubbles that are usable in the level as defined by levelparams
        LevelParams = GameObject.Find("LevelParams");
        bool[] curBubbleChoices = LevelParams.GetComponent<LevelParamVars>().bubbleChoices;

        int numActiveBubbles = 0;
        for (int i = 0; i < curBubbleChoices.Length; i++)
        {
            if (curBubbleChoices[i] == true)
            {
                numActiveBubbles++;
            }
        }

        bubbles = new GameObject[numActiveBubbles];
        curBubbCosts = new int[numActiveBubbles];

        int curCount = 0;
        for (int i = 0; i < curBubbleChoices.Length; i++){

            if (curBubbleChoices[i] == true)
            {
                bubbles[curCount] = FindObjectOfType<GameManager>().bubbles[i];
                curBubbCosts[curCount] = bubbCosts[i];
                curCount++;
            }
        }
        // End of the code
        
        bubbleCount = LevelParams.GetComponent<LevelParamVars>().resourceCount;
        UIScript.numBubbles = bubbleCount;
        bubbleChoice = 0;
        facingRight = true;
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        airControl = true;
        infiniteBubbles = false;
    }

    private void Update()
    {
        handleInput();
        if (infiniteBubbles)
        {
            bubbleCount = 3;
            UIScript.numBubbles = bubbleCount;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        isGrounded = isGroundedCheck();
        
        handleMovement(horizontal);
        handleAttacks();

        Flip(horizontal);

        resetValues();
        checkFallingToDeath();

        if (myRB.position.y < -50f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    void handleMovement(float horizontal)
    {
        if ((!this.myAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && isGrounded) || airControl)
        {
            myRB.velocity = new Vector2(horizontal * movementSpeed, myRB.velocity.y);

        }
        if (isGrounded && isJump)
        {
            isGrounded = false;
            myRB.AddForce(new Vector2(0, jumpForce));
            JumpSource.Play();
        }
        myAnim.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void handleAttacks()
    {
        if (isAttacking && !this.myAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myAnim.SetTrigger("attack");
            myRB.velocity = Vector2.zero;
        }
    }

    void handleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            isAttacking = true;
            makeBubble();

        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            switchBubble(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            switchBubble(1);
        }
    }

    void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    void resetValues()
    {
        isAttacking = false;
        isJump = false;
    }

    void switchBubble(int i){
        bubbleChoice = (bubbleChoice + i) % bubbles.Length;
        if(bubbleChoice < 0){
            bubbleChoice = bubbles.Length - 1;
        }
        UIScript.currentBubb = bubbleChoice;
        Debug.Log(bubbleChoice);

    }

    private bool isGroundedCheck()
    {
        myAnim.SetBool("falling", false);
        //if (myRB.velocity.y <= 1)
        //{
        int count = 0;
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        count++;
                        //return true;
                    }
                }
            }
        //}
        if(count > 1){
            return true;
        }
        return false;
    }

    private void checkFallingToDeath()
    {
        if (myRB.position.y <= -1)
        {
            myAnim.SetBool("falling", true);
        }
    }

    public void makeBubble()
    {
        if (bubbleCount - curBubbCosts[bubbleChoice] >= 0)
        {
            bubbleCount -= curBubbCosts[bubbleChoice];
            UIScript.numBubbles = bubbleCount;
            BlowSource.Play();
            if (facingRight)
            {
                GameObject tmp = (GameObject)Instantiate(bubbles[bubbleChoice], transform.position, Quaternion.identity);
                tmp.GetComponent<Bubble>().Initialize(Vector2.right);
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(bubbles[bubbleChoice], transform.position, Quaternion.identity);
                tmp.GetComponent<Bubble>().Initialize(Vector2.left);
            }
        }


    }

    public void refillBubble()
    {
        bubbleCount += 3;
        UIScript.numBubbles = bubbleCount;
    }
}
