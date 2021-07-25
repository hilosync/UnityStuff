using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    
    private SpriteRenderer theSpriteRenderer;

    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 0.8f;
    [SerializeField] float maxX = 15.2f;

    [SerializeField] Sprite defaultPaddle;
    [SerializeField] Sprite pressedPaddle;

    [SerializeField] KeyCode keyToPressSpecial;
    [SerializeField] KeyCode keyToPressLeftLean;
    [SerializeField] KeyCode keyToPressRightLean;
    

    GameSession theGameSession;
    Ball theBall;
    // Start is called before the first frame update
    void Start()
    {
        theSpriteRenderer = GetComponent<SpriteRenderer>();

        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);

        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);

        transform.position = paddlePos;

        if (!GameObject.Find("Ball").GetComponent<Ball>().hasStarted)
        {
            return;
        }
        SpecialPress();

        LeftLean();

        RightLean();
    }

    private void RightLean()
    {
        if (Input.GetKeyDown(keyToPressRightLean))
        {
            transform.eulerAngles = new Vector3(0,0,-25);
        }

        if (Input.GetKeyUp(keyToPressRightLean))
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
    }

    private void LeftLean()
    {
        if (Input.GetKeyDown(keyToPressLeftLean))
        {
            transform.eulerAngles = new Vector3(0,0,25);
        }

        if (Input.GetKeyUp(keyToPressLeftLean))
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
    }

    private void SpecialPress()
    {
        if (Input.GetKeyDown(keyToPressSpecial))
        {
            theSpriteRenderer.sprite = pressedPaddle;
        }

        if (Input.GetKeyUp(keyToPressSpecial))
        {
            theSpriteRenderer.sprite = defaultPaddle;
        }
    }

    private float GetXPos()
    {
        if(theGameSession.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            return (Input.mousePosition.x / Screen.width * 16);
        }
    }




}
