using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clicker : MonoBehaviour
{
    public static Clicker Instance { get; private set; }

    public TextMeshProUGUI scoreText;

    public int score;
    private bool isDoubleScoreActive = false;
    private float doubleScoreEndTime = 0f;

    public ParticleSystem salt;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        score = 0;
    }

    public void Score()
    {
        if (isDoubleScoreActive && Time.time < doubleScoreEndTime)
        {
            score += Random.Range(2,6); // Skoru 2x arttýr
        }
        else
        {
            score++;
        }

        scoreText.text = score.ToString();
        salt.Play();
        StartCoroutine(SaltSecond());
    }
    private IEnumerator SaltSecond()
    {
        yield return new WaitForSeconds(0.5f);
        salt.Stop();
    }

    public void ActivateDoubleScore(float duration)
    {
        isDoubleScoreActive = true;
        doubleScoreEndTime = Time.time + duration;
        StartCoroutine(DeactivateDoubleScore(duration));
    }

    private IEnumerator DeactivateDoubleScore(float duration)
    {
        yield return new WaitForSeconds(duration);
        isDoubleScoreActive = false;
    }
}
