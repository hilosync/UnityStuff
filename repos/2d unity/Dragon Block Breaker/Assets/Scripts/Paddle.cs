using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 0.8f;
    [SerializeField] float maxX = 15.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseXPos = (Input.mousePosition.x / Screen.width * 16);

        Vector2 paddlePos = new Vector2 (transform.position.x, transform.position.y);

        paddlePos.x = Mathf.Clamp(mouseXPos, minX, maxX);

        transform.position = paddlePos;

    }
}
