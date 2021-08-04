using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PowerBar : MonoBehaviour
{
    public Ball isHitScript;

    public bool fillBarTurnedOff = true;
    public Slider slider;

    [SerializeField] float powerUpDuration = 10f;
    ParticleSystem fireVFX;

    GameObject fireVFXGameObject;
    public bool poweredUp = false;

    public float sliderValue;
    public GameObject FillBar;

    Scene currentScene;
    
    void Awake() 
    {
        fireVFX = GameObject.Find("fx_fire_a").GetComponent<ParticleSystem>();
        gameObject.transform.localScale = new Vector3(2,2,1);
        fireVFX.Stop();
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        fireVFX = GameObject.Find("fx_fire_a").GetComponent<ParticleSystem>();
        FillBar = GameObject.Find("Fill Area");
        FillBar.SetActive(false);
        fireVFX.Stop();
        isHitScript = FindObjectOfType<Ball>();
        slider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
            gameObject.transform.localScale = new Vector3(2,2,1);
            currentScene = SceneManager.GetActiveScene();
            if(currentScene.name == "Game Over" | currentScene.name == "Game Over Bad" | currentScene.name == "Start Menu" )
            {
                gameObject.transform.localScale = new Vector3(0,0,0);
            } 
    }

    public void PowerProgress(float newPower)
    {
        if(!poweredUp)
        {
            if(fillBarTurnedOff)
                fillBarTurnedOff = false;
                FillBar.SetActive(true);
            slider.value += newPower;
        }
    }

    public void PowerActivated()
    {
        fireVFX = GameObject.Find("fx_fire_a").GetComponent<ParticleSystem>();
        fillBarTurnedOff = true;
        FillBar.SetActive(false);
        slider.value = 0f;
        fireVFX.Play();
        poweredUp = true;

        StartCoroutine(StoppingFireVFX());

    }

    IEnumerator StoppingFireVFX()
    {
        yield return new WaitForSeconds(powerUpDuration);
        poweredUp = false;
        fireVFX = GameObject.Find("fx_fire_a").GetComponent<ParticleSystem>();
        fireVFX.Stop();
    }
}
