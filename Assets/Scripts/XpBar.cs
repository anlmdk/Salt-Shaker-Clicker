using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    public Slider xpSlider;
    public TextMeshProUGUI levelText;

    public int currentXP = 0;
    private int level = 1;

    public TextMeshProUGUI xpBarText;

    void Start()
    {
        UpdateXpBar();
    }

    public void CurrentXpBar()
    {
        currentXP += Random.Range(1, 10);

        // Seviye atlandýysa güncelle
        if (currentXP >= 100)
        {
            currentXP = 0;
            level++;
            UpdateXpBar();
        }
        
    }

    private void UpdateXpBar()
    {
        xpSlider.value = currentXP;
        xpBarText.text = "%" + xpSlider.value.ToString();
        levelText.text = "Level: " + level;
    }
}
