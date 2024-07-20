using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float currentTime = 0;

    [SerializeField] private TextMeshProUGUI timerText;

    void Update()
    {
        TimeCount();
    }

    private void TimeCount()
    {
        currentTime += Time.deltaTime;
        int seconds = Mathf.FloorToInt(currentTime);
        timerText.text = "Time: " + seconds.ToString();
    }
}
