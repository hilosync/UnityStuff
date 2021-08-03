using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class Timer : MonoBehaviour
{
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        float timePassed = Time.time - startTime;

        string minutes = ((int) timePassed / 60).ToString();
        string seconds = (timePassed % 60).ToString("f2");

        gameObject.GetComponent<TextMeshProUGUI>().text = minutes + ":" + seconds;
        
    }
}
