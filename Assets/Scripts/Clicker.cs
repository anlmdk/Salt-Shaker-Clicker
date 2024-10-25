using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clicker : MonoBehaviour
{
    public static Clicker Instance { get; private set; }

    public TextMeshProUGUI scoreText;

    public int score;
    private int scoreIncrement = 1;

    public ParticleSystem salt;
    public AudioSource saltSFX;

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
        score += scoreIncrement; // AutoClick için kullanýlan artýþ miktarý

        UpdateScoreText();
        salt.Play();
        saltSFX.Play();
        StartCoroutine(SaltSecond());
    }
    private IEnumerator SaltSecond()
    {
        yield return new WaitForSeconds(0.5f);
        salt.Stop();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void SetScoreIncrement(int increment)
    {
        scoreIncrement = increment;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }
}
