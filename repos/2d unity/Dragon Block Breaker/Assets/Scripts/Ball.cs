using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 12f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;
    [SerializeField] float speedUpTimeLag = 0.2f;

    [SerializeField] float speedUpFactor = 1.2f;

    [SerializeField] float maxSpeedFactor = 2f;

    Vector2 paddleToBallVector;
    public bool hasStarted = false;


    AudioSource myAudioSource;
    Rigidbody2D myRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position -paddle1.transform.position;
        
        myAudioSource = GetComponent<AudioSource>(); 

        myRigidBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
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
            if(myRigidBody.velocity.magnitude <= Mathf.Sqrt(Mathf.Pow(maxSpeedFactor*xPush,2) + Mathf.Pow(maxSpeedFactor*yPush,2)))
                myRigidBody.velocity = speedUp;
        }
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(speedUpTimeLag);
    }
}
