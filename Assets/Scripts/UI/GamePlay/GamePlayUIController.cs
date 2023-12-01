using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIController: MonoBehaviour
{
    public static GamePlayUIController Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    public PauseMenuController pauseMenu;
    public GameOverController gameOver;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            pauseMenu.OnPauseKey();
        }
    }

    public void OnPlayerDeath()
    {
        gameOver.OnDeath();
    }
    
    public void OnOpenMainInventory()
    {
    }
}
