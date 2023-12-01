using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerText;
    float time;
    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        time += Time.deltaTime;
        timerText.text = Mathf.FloorToInt(time/60)+ " : " + Mathf.Round( time%60);
    }
}
