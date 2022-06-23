using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WinnerScript : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeText2;
    public float newTime;
    public float recordTime;
    private TimeSpan time;

    void Start()
    {
        //newTime = TimeScript.currentTime;
        //time = TimeSpan.FromSeconds(newTime);
        //timeText.text = "TIME: " + time.ToString(@"mm\:ss\:ff");

        newTime = TimeScript.currentTime;
        recordTime = TestScript.recordTime;

        if (newTime < recordTime)
        {
            time = TimeSpan.FromSeconds(newTime);
            timeText.text = "NEW RECORD TIME: " + time.ToString(@"mm\:ss\:ff");
            TestScript.recordTime = newTime;
        }
        else
        {
            time = TimeSpan.FromSeconds(recordTime);
            timeText.text = "RECORD TIME: " + time.ToString(@"mm\:ss\:ff");
        }

        time = TimeSpan.FromSeconds(newTime);
        timeText2.text = "CURRENT TIME: " + time.ToString(@"mm\:ss\:ff");

    }

}
