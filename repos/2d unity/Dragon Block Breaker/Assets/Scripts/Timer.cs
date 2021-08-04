using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    SceneLoaderScript sceneLoaderScript;
    private float startTime;

    private bool finished = false;

    Scene currentScene;
    // Start is called before the first frame update
    private void Awake() 
    {
        finished = false;
        startTime = Time.time;
        gameObject.transform.localScale = new Vector3(1,1,1);
        int gameStatusCount = FindObjectsOfType<Timer>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);

        startTime = Time.time;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        
        
        startTime = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = new Vector3(1,1,1);
        if(finished)
            return;
        float timePassed = Time.time - startTime;

        string minutes = ((int) timePassed / 60).ToString();
        string seconds = (timePassed % 60).ToString("f2");

        gameObject.GetComponent<TextMeshProUGUI>().text = minutes + ":" + seconds;

        currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "Game Over")
        {
            Finish();
            gameObject.transform.localPosition = new Vector3(24,73,0);
        } 

        if(currentScene.name == "Game Over Bad")
        {
            Finish();
            gameObject.transform.localScale = new Vector3(0,0,0);
        } 
        
    }

    public void Finish()
    {
        finished = true;
    }
}
