using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeScript : MonoBehaviour
{
    private bool timerActive = true;
    public static float currentTime = float.MaxValue;
    public TextMeshProUGUI currentTimeText;

    void Start()
    {
        currentTime = 0;
    }

    void Update()
    {
        if(timerActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = "Time: " + time.ToString(@"mm\:ss\:ff");
    }

}
