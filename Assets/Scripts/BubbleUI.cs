using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BubbleUI : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(Sprite s)
    {
        gameObject.GetComponent<Image>().sprite = s;
    }

}
