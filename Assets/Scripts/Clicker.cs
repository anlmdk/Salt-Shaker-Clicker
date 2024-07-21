using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clicker : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public ParticleSystem salt;

    public int score;

    private void Start()
    {
        score = 0;
    }

    public void Score()
    {
        score++;
        scoreText.text = score.ToString();

        salt.Play();
        StartCoroutine(SaltSecond());
    }
    private IEnumerator SaltSecond()
    {
        yield return new WaitForSeconds(0.5f);
        salt.Stop();
    }
}
