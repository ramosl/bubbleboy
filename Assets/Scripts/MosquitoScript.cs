using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoScript : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    private Animator myAnim;

    public Rigidbody2D myRB;
    public float heightChange = 5f;
    public float speed = 5f;

    public float height = 10f;

    private bool isDead = false;

    public Collider2D col;
    
	// Use this for initialization
	void Start () {
        myAnim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject

        if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
            spriteRenderer.sprite = sprite1;
    }
	
	// Update is called once per frame
	private void FixedUpdate () {
        if (!isDead)
        {
            float newY = Mathf.Sin(Time.time * speed) * heightChange;

            myRB.position = new Vector2(myRB.position.x, newY + height);
        } else {
            myRB.gravityScale = 1;
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.name == "Bubble(Clone)" || collisionInfo.gameObject.name == "BouncyBubble 1(Clone)")
        {
            Destroy(collisionInfo.gameObject);
        } else if (collisionInfo.gameObject.name == "Player"){
            myAnim.enabled = false;
            Debug.Log(spriteRenderer.sprite.name);
            if (spriteRenderer.sprite == sprite1 || spriteRenderer.sprite == sprite2)
            {
                isDead = true;
                spriteRenderer.sprite = sprite3;
            }
        } else {
            col.enabled = false;
        }
    }
}
