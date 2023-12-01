using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayUIController: MonoBehaviour
{

    public static GamePlayUIController Instance;
    [SerializeField] GameObject PlayerMessage;
    private void Awake()
    {
        Instance = this;
    }
    
    public PauseMenuController pauseMenu;
    public GameOverController gameOver;

    private void Start()
    {
        Time.timeScale = 1f;
        TellPlayer("Click 1 and 2 or drag to place turrets");
    }

    float tellPlayerCD = 2f;
    float tellPlayerTimer = 2f;
    bool tellPlayerOnCD = false;
    public void TellPlayer(string mes)
    {
        GameObject pm = Instantiate(PlayerMessage,transform);
        pm.GetComponentInChildren<TextMeshProUGUI>().text= mes;
        Destroy(pm.gameObject,2f);
        tellPlayerOnCD=true;
    }

    void Update()
    {
        tellPlayerTimer -= Time.deltaTime;
        if (tellPlayerTimer <= 0 && tellPlayerOnCD)
        {
            tellPlayerTimer=tellPlayerCD;
            tellPlayerOnCD=(false);
        }
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
