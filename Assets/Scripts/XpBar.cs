using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    public Slider xpSlider;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpBarText;

    public int currentXP = 0;
    private int maxXP = 100;
    private int level = 1;

    private int targetXP = 0; // Hedef XP
    private Coroutine xpCoroutine;

    void Start()
    {
        xpSlider.maxValue = maxXP;
        UpdateXpBar();
    }

    public void CurrentXpBar()
    {
        int xpGain = Random.Range(1, 5);
        targetXP += xpGain;

        // Seviye atland�ysa g�ncelle
        if (targetXP >= maxXP)
        {
            level++;
            targetXP -= maxXP; // Kalan XP'yi bir sonraki seviyeye aktar
            currentXP = 0; // currentXP'yi s�f�rla
        }

        if (xpCoroutine != null)
        {
            StopCoroutine(xpCoroutine);
        }

        xpCoroutine = StartCoroutine(AnimateXpBar());
    }

    private IEnumerator AnimateXpBar()
    {
        float duration = 0.5f; // Animasyon s�resi (saniye)
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
        xpCoroutine = null; // Coroutine'in bitti�ini belirtmek i�in null yap
    }

    private void UpdateXpBarText()
    {
        float expPercent = (float)currentXP / maxXP;
        xpBarText.text = "%" + (expPercent * 100).ToString(""); // % de�erini 2 ondal�k basamakla g�ster
        levelText.text = "Level: " + level;
    }

    private void UpdateXpBar()
    {
        xpSlider.value = currentXP;
        UpdateXpBarText();
    }
}
