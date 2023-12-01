using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIController: MonoBehaviour
{
    public PauseMenuController pauseMenu;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            pauseMenu.OnPauseKey();
        }
    }

    public void OnOpenMainInventory()
    {
    }
}
