using UnityEngine;

public class BouncyBubbleTest : MonoBehaviour {
    public float springForce = 10000;
    private Collision2D collision;
    private bool bouncing = false;


    public Rigidbody2D myRB;
    public float heightChange = 5f;
    public float speed = 5f;
    public float height = 10f;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            if (!bouncing)
            {
                bouncing = true;
                collision = coll;
            }

        }
    }

    void FixedUpdate()
    {
        if (bouncing)
        {
            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(0f, springForce));
            bouncing = false;
        }

        float newY = Mathf.Sin(Time.time * speed) * heightChange;

        myRB.position = new Vector2(myRB.position.x, newY + height);
    }
}