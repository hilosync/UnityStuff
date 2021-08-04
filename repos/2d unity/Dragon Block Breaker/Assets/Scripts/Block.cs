using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    Level level;
    GameSession points;


    [SerializeField] Color[] hitColour;
    //[SerializeField] int maxHits;
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int timesHit;

    PowerBar powerBarScript;
    private void Start()
    {
        powerBarScript = FindObjectOfType<PowerBar>();
        level = FindObjectOfType<Level>();

        if (tag == "Breakable")
        {
            level.CountBreakableBlocks();
        }
        
        points = FindObjectOfType<GameSession>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        powerBarScript = FindObjectOfType<PowerBar>();
        if (tag == "Breakable")
        {
            if(powerBarScript.poweredUp)
            {
                timesHit += 2;
            }
            else
            {
                timesHit++;
            }
            int maxHits = hitColour.Length + 1;
            if (timesHit >= maxHits)
                DestroyBlock();
            else
                showNextHitSprite();
        }
    }


    private void showNextHitSprite()
    {

        int colourIndex = timesHit - 1;
        if(hitColour[colourIndex] != null)
            GetComponent<SpriteRenderer>().color = hitColour[colourIndex];
        else
            Debug.LogError("Block colour is missing from colour array" + gameObject.name);
        

    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX();
    }
    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX,transform.position, transform.rotation);
        Destroy(sparkles,2f);
    }
}
