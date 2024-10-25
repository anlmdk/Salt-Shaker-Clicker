using CrazyGames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BannerModule : MonoBehaviour
{
    public CrazyBanner bannerPrefab;
    public Button continueButton; // Devam butonu referansý
    public TextMeshProUGUI buttonText; // Buton üzerindeki metin
    private Coroutine countdownCoroutine;

    private void Start()
    {
        CrazySDK.Init(UpdateBannersDisplay);
        continueButton.interactable = false; // Baþlangýçta butonu devre dýþý býrak
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
        // Butonu devre dýþý býrak ve geri sayýmý baþlat
        continueButton.interactable = false;
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(ShowAdAndCountdown());
    }

    private IEnumerator ShowAdAndCountdown()
    {
        // Reklamý göster ve geri sayýmý baþlat
        yield return new WaitForSeconds(5f); // Reklam süresi boyunca bekle

        // Geri sayým süresi
        int countdown = 5;
        while (countdown > 0)
        {
            buttonText.text = "Continue (" + countdown + ")";
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        buttonText.text = "Continue";
        continueButton.interactable = true; // Butonu etkinleþtir
    }
}
