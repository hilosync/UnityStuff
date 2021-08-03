using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

       
    // Start is called before the first frame update
    void Start()
    {
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
