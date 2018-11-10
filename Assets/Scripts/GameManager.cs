using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public static int health = 5;

    public GameObject winText;

    bool gameHasEnded = false;

    public GameObject[] bubbles;

    public GameObject LevelParams;

    // Use this for initialization
    void Start () {
        //LevelParams = GameObject.Find("LevelParams");
        //Debug.Log(LevelParams.GetComponent<LevelParamVars>().bubbleChoices);
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void win(){
        winText.gameObject.SetActive(true);
        Invoke("nextScene", 2.5f);
    }

    void nextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public float restartDelay = 0.25f;

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", restartDelay);
        }

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
