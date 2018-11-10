using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownBubble : MonoBehaviour {

    public Rigidbody2D myRB;
    public float heightChange = 5f;
    public float speed = 5f;

    public float height = 0f;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        float newY = Mathf.Sin(Time.time * speed) * heightChange;

        myRB.position = new Vector2(myRB.position.x, newY + height);
    }
}
