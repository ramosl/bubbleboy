using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour {

    public GameManager gmScript;

    private void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
            Debug.Log("You Win!");
            gmScript.win();
        }
    }
}
