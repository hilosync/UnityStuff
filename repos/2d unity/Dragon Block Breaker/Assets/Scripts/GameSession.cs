﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{

    [Range(0.1f,10f)] [SerializeField] float gameSpeed = 1f;
    
    [SerializeField] bool isAutoPlayEnabled;

    private void Awake() 
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;

    }

    
    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
