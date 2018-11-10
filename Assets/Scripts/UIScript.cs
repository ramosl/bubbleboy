using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public static int numBubbles;
    public static int currentBubb;

    public GameObject bub1;

    [SerializeField]
    private GameObject bubbleUIPrefab;

    private GameObject[] bubbles;

    public int curChoice;

    public Sprite[] bubbleSpriteList;

    public Sprite test;

    public GameObject LevelParams;
    public GameObject selectorElem;

    Vector2[] selectUIPos;

    public Text bubbleCountText;
    

    // Use this for initialization
    void Start () {
        //numBubbles = 3;
        currentBubb = 0;

        curChoice = 0;

        LevelParams = GameObject.Find("LevelParams");
        numBubbles = LevelParams.GetComponent<LevelParamVars>().resourceCount;
        bool[] curBubbleChoices = LevelParams.GetComponent<LevelParamVars>().bubbleChoices;

        int numActiveBubbles = 0;
        for (int i = 0; i < curBubbleChoices.Length; i++){
            if(curBubbleChoices[i] == true){
                numActiveBubbles++;
            }
        }


        selectUIPos = new Vector2[numActiveBubbles];

        //Debug.Log(LevelParams.GetComponent<LevelParamVars>().bubbleChoices);

        int curCount = 0;
        for (int i = 0; i < bubbleSpriteList.Length; i++){
            if (curBubbleChoices[i] == true)
            {
                // MATH IS WRONG, WILL NEED TO FIX
                selectUIPos[curCount] = new Vector2(-285 + 50 * curCount, 175);

                //WILL REVISIT THIS CODE WHEN I FIX THE THINGS ON 2D
                GameObject tmp = Instantiate(bubbleUIPrefab, selectUIPos[curCount], Quaternion.identity, gameObject.transform);
                tmp.GetComponent<RectTransform>().localPosition = selectUIPos[curCount];
                tmp.GetComponent<BubbleUI>().Initialize(bubbleSpriteList[i]);

                curCount++;

            }
        }
        selectorElem.GetComponent<RectTransform>().localPosition = selectUIPos[0];
    }


    // Update is called once per frame
    void Update () {
        bubbleCountText.text = numBubbles.ToString("0");
        //selectorElem.GetComponent<RectTransform>().localPosition = new Vector2(-250 + 80 * 1, 177);
        selectorElem.GetComponent<RectTransform>().localPosition = selectUIPos[currentBubb];
        //transform.position = 

    }
}
