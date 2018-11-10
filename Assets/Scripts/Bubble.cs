using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class Bubble : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Rigidbody2D myRB;

    [SerializeField]
    private BoxCollider2D myCollider;

    [SerializeField]
    private CircleCollider2D myCollider2;

    public Sprite sprite1;
    public Sprite sprite2;

    private Vector2 direction;

    public float slowtime;
    public float bubbleTick;

    public string bubbleScriptName;

    // Use this for initialization
    void Start(){
        myRB = GetComponent<Rigidbody2D>();
        myCollider.enabled = false;
        myCollider2.enabled = false;
        slowtime = Time.time;
        bubbleTick = 0.2f;

        UpDownBubble upDown = gameObject.GetComponent<UpDownBubble>();
        upDown.height = myRB.position.y;

        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
            spriteRenderer.sprite = sprite1;
    }

    public BouncyBubbleTest1 bouncyScript;


    private void FixedUpdate()
    {
        if(speed > 0.5f){
            myRB.velocity = direction * speed;

            if (Time.time > slowtime)
            {
                speed /= 2f;
                //slowtime = 2000000f;
                slowtime = Time.time + bubbleTick;
            }
        } else {
            // THIS HAPPENS WHEN THE CODE IS ENABLED
            myRB.velocity = Vector2.zero;
            // myRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            myRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            myCollider.enabled = true;
            myCollider2.enabled = true;

            //SHITTY WORKAROUND, HAVE TO POTENTIALLY LIST ALL SCRIPTS ABOVE TO SET ENABLE TO TRUE, IDK OTHER WAY
            if(bouncyScript){
                bouncyScript.enabled = true;
            }


            //BouncyBubbleTest1 a = gameObject.GetComponent(bubbleScriptName);
            //script.setEnable(true)

            if (spriteRenderer.sprite == sprite1) // if the spriteRenderer sprite = sprite1 then change to sprite2
            {
                // Turns off Y constraint so that bubble can go up and down
                myRB.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                spriteRenderer.sprite = sprite2;
            }
        }


    }

    // Update is called once per frame
    void Update () {

    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    // Code below destroys if it leaves screen
    //private void OnBecameInvisible()
    //{
    //    Destroy(gameObject);
    //}
}
