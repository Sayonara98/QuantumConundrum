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
    public CraftingManager crafting;

    private void Start()
    {
        Time.timeScale = 1f;
        TellPlayer("Click 1 and 2 or drag to place turrets",5f);
        StartCoroutine(SecondTut());
    }

    float tellPlayerCD = 2f;
    float tellPlayerTimer = 2f;
    bool tellPlayerOnCD = false;
    public void TellPlayer(string mes,float time)
    {
        GameObject pm = Instantiate(PlayerMessage,transform);
        pm.GetComponentInChildren<TextMeshProUGUI>().text= mes;
        Destroy(pm.gameObject,time);
        tellPlayerOnCD=true;
    }
    IEnumerator SecondTut()
    {
        yield return new WaitForSeconds(7f);
        TellPlayer("The Scanning turret gives resources based on the terrain",5f);
    }
    IEnumerator ThirdTut()
    {
        yield return new WaitForSeconds(14f);
        TellPlayer("Each Terrain gives a different bonus and turret blueprint", 5f);
    }
    void Update()
    {
        tellPlayerTimer -= Time.deltaTime;
        if (tellPlayerTimer <= 0 && tellPlayerOnCD)
        {
            tellPlayerTimer=tellPlayerCD;
            tellPlayerOnCD=(false);
        }
        if (Input.GetKey(KeyCode.C))
        {
            crafting.ShowItemList();
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
