using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    public static XpBar Instance { get; private set; }

    public Slider xpSlider;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpBarText;

    public int currentXP = 0;
    private int maxXP = 100;
    public int level = 1;


    public int xpGain;
    public int targetXP = 0; // Hedef XP
    private Coroutine xpCoroutine;

    private bool isDoubleXpActive;
    private float doubleXpEndTime = 0f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        xpSlider.maxValue = maxXP;
        UpdateXpBar();
    }

    public void CurrentXpBar()
    {
        if (isDoubleXpActive && Time.time < doubleXpEndTime)
        {
            xpGain = Random.Range(5, 10);
        }
        else
        {
            xpGain = Random.Range(1, 5);
        }
        targetXP += xpGain;

        // Seviye atlandýysa güncelle
        if (targetXP >= maxXP)
        {
            level++;
            targetXP -= maxXP; // Kalan XP'yi bir sonraki seviyeye aktar
            currentXP = 0; // currentXP'yi sýfýrla
        }

        if (xpCoroutine != null)
        {
            StopCoroutine(xpCoroutine);
        }

        xpCoroutine = StartCoroutine(AnimateXpBar());
    }

    public void IncreaseXp(int scoreIncrease)
    {
        int xpIncrease = 0;

        if (scoreIncrease >= 1000)
        {
            xpIncrease = Random.Range(20, 40);
        }
        else if (scoreIncrease >= 500)
        {
            xpIncrease = Random.Range(15, 30);
        }
        else if (scoreIncrease >= 100)
        {
            xpIncrease = Random.Range(10, 20);
        }
        else
        {
            xpIncrease = Random.Range(1, 5);
        }

        targetXP += xpIncrease;

        // Seviye atlandýysa güncelle
        if (targetXP >= maxXP)
        {
            level++;
            targetXP -= maxXP; // Kalan XP'yi bir sonraki seviyeye aktar
            currentXP = 0; // currentXP'yi sýfýrla
        }

        if (xpCoroutine != null)
        {
            StopCoroutine(xpCoroutine);
        }

        xpCoroutine = StartCoroutine(AnimateXpBar());
    }


    public void ActiveDoubleXp()
    {
        xpGain = Random.Range(5, 15);
        targetXP += xpGain;
    }

    private IEnumerator AnimateXpBar()
    {
        float duration = 0.5f; // Animasyon süresi (saniye)
        float elapsed = 0f;
        int startXP = currentXP;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / duration);
            currentXP = (int)Mathf.Lerp(startXP, targetXP, progress);
            xpSlider.value = currentXP;
            UpdateXpBarText();
            yield return null;
        }

        currentXP = targetXP;
        xpSlider.value = currentXP;
        UpdateXpBarText();
        xpCoroutine = null; // Coroutine'in bittiðini belirtmek için null yap
    }

    private void UpdateXpBarText()
    {
        float expPercent = (float)currentXP / maxXP;
        xpBarText.text = "%" + (expPercent * 100).ToString(""); // % deðerini 2 ondalýk basamakla göster
        levelText.text = "Level: " + level;
    }

    private void UpdateXpBar()
    {
        xpSlider.value = currentXP;
        UpdateXpBarText();
    }

    public void ActivateDoubleXp(float duration)
    {
        isDoubleXpActive = true;
        doubleXpEndTime = Time.time + duration;
        StartCoroutine(DeactivateDoubleXp(duration));
    }

    private IEnumerator DeactivateDoubleXp(float duration)
    {
        yield return new WaitForSeconds(duration);
        isDoubleXpActive = false;
    }
}
