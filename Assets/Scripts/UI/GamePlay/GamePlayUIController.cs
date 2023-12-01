using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayUIController: MonoBehaviour
{
    public static GamePlayUIController Instance;
    [SerializeField] TextMeshProUGUI PlayerMessage;
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

    public void TellPlayer(string mes)
    {
        TextMeshProUGUI pm = Instantiate(PlayerMessage,transform);
        pm.text= mes;
        Destroy(pm.gameObject,2f);
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
