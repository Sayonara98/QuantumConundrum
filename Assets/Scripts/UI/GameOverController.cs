using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void OnDeath()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void OnQuitClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
