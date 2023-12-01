using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public void OnPauseKey()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void OnResumeClick()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnQuitClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
