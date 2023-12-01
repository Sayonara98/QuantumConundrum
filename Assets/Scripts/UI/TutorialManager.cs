using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public List<GameObject> slides;
    public int current = 0;

    public GameObject previousButton;
    public GameObject nextButton;
    public GameObject playButton;

    private void Start()
    {
        SetPage(-1, current);
    }

    public void OnPreviousClick()
    {
        int last = current;
        current--;
        SetPage(last, current);
    }

    public void OnNextClick()
    {
        int last = current;
        current++;
        SetPage(last, current);
    }

    public void OnPlayClick()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void OnExitClick()
    {
        int last = current;
        current = slides.Count - 1;
        SetPage(last, current);
    }

    public void SetPage(int last, int page)
    {
        if (last >= 0)
        {
            slides[last].SetActive(false);
        }
        slides[page].SetActive(true);
        
        if (page == 0)
        {
            previousButton.SetActive(false);
        }
        else
        {
            previousButton.SetActive(true);
        }

        if (page == slides.Count - 1)
        {
            nextButton.SetActive(false);
            playButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(true);
            playButton.SetActive(false);
        }
    }
}
