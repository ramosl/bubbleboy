using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePickup : MonoBehaviour {

    //public GameObject bubblePickup;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Collided?");
            FindObjectOfType<Player>().refillBubble();
            this.gameObject.SetActive(false);
        }
    }
}
