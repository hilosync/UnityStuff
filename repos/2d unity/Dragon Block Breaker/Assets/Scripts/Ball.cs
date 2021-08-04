using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] KeyCode keyToPress;
    [SerializeField] KeyCode activatePowerButton;
    [SerializeField] bool ballTouchingPaddle;
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 12f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;
    [SerializeField] float speedUpTimeLag = 0.2f;

    [SerializeField] float speedUpFactor = 1.2f;

    [SerializeField] float maxSpeedFactor = 2f;

    [SerializeField] float powerIncrement = 0.2f;

    Vector2 paddleToBallVector;
    public bool hasStarted = false;

    public bool isHit = false;

    GameObject fireVFX;

    public PowerBar sliderScript;

    AudioSource myAudioSource;
    Rigidbody2D myRigidBody;
    // Start is called before the first frame update
    void Awake() 
    {
        fireVFX = GameObject.Find("fx_fire_a");
    }
    void Start()
    {
        paddleToBallVector = transform.position -paddle1.transform.position;
        
        myAudioSource = GetComponent<AudioSource>(); 

        myRigidBody = GetComponent<Rigidbody2D>();

        sliderScript = FindObjectOfType<PowerBar>();

    }

    // Update is called once per frame
    void Update()
    {
        fireVFX.transform.position = transform.position;
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
        else
        {
            PowerMode();
            if(Input.GetKeyDown(keyToPress))
            {
                if(ballTouchingPaddle)
                {
                    sliderScript = FindObjectOfType<PowerBar>();
                    sliderScript.PowerProgress(powerIncrement);
                }
            }
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

        private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
            {
                myRigidBody.velocity = new Vector2(xPush,yPush);
                hasStarted = true;
            }

    }

    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(-randomFactor,randomFactor),Random.Range(-randomFactor,randomFactor));


        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody.velocity += velocityTweak;
            Vector2 speedUp = new Vector2(myRigidBody.velocity.x * speedUpFactor,myRigidBody.velocity.y * speedUpFactor);
            StartCoroutine(Waiter());
            if(myRigidBody.velocity.magnitude <= (Mathf.Sqrt(Mathf.Pow(maxSpeedFactor*xPush,2) + Mathf.Pow(maxSpeedFactor*yPush,2))))
                myRigidBody.velocity = speedUp;

        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Activator")
        {
            ballTouchingPaddle = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Activator")
        {
            ballTouchingPaddle = false;
        }
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(speedUpTimeLag);
    }

    public void PowerMode()
    {
        if(Input.GetKeyDown(activatePowerButton))
        {
            if(sliderScript.slider.value == 1f)
            {
                sliderScript.PowerActivated();
            }
        }
    }
}
