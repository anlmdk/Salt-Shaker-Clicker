using CrazyGames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BannerModule : MonoBehaviour
{
    public CrazyBanner bannerPrefab;
    public Button continueButton; // Devam butonu referans�
    public TextMeshProUGUI buttonText; // Buton �zerindeki metin
    private Coroutine countdownCoroutine;

    private void Start()
    {
        CrazySDK.Init(UpdateBannersDisplay);
        continueButton.interactable = false; // Ba�lang��ta butonu devre d��� b�rak
    }

    public void UpdateBannersDisplay()
    {
        CrazySDK.Banner.RefreshBanners();
        StartCoroutine(ShowAdAndCountdown());
    }

    public void AddBanner()
    {
        GameObject showBanner = GameObject.Find("Banners");

        var banner = Instantiate(bannerPrefab, new Vector3(), new Quaternion(), showBanner.transform);
        banner.Size = (CrazyBanner.BannerSize)Random.Range(0, 2);
        banner.Position = new Vector2(0, 0);
    }

    public void DisableLastBanner()
    {
        var banners = FindObjectsOfType<CrazyBanner>();

        foreach (var banner in banners)
        {
            if (!banner.IsVisible()) continue;
            banner.gameObject.SetActive(false);
            return;
        }
        // Butonu devre d��� b�rak ve geri say�m� ba�lat
        continueButton.interactable = false;
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(ShowAdAndCountdown());
    }

    private IEnumerator ShowAdAndCountdown()
    {
        // Reklam� g�ster ve geri say�m� ba�lat
        yield return new WaitForSeconds(5f); // Reklam s�resi boyunca bekle

        // Geri say�m s�resi
        int countdown = 5;
        while (countdown > 0)
        {
            buttonText.text = "Continue (" + countdown + ")";
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        buttonText.text = "Continue";
        continueButton.interactable = true; // Butonu etkinle�tir
    }
}
