using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clicker : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public int score;

    private void Start()
    {
        score = 0;
    }

    public void Score()
    {
        score++;
        scoreText.text = score.ToString(); 
    }
}
