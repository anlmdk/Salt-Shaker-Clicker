using CrazyGames;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AdInterstitialModule : MonoBehaviour
{
    [SerializeField] private CrazyAdType adType = CrazyAdType.Midgame;

    [SerializeField] private bool adOnVisible = false;
    private int lastAdLevel = -1;

    void Start()
    {
        CrazySDK.Init(() => { });
    }


    private void FixedUpdate()
    {
        if (!adOnVisible)
        {
            Check();
            AdModuleInterstitial();
        }
    }

    public void AdModuleInterstitial()
    {
        int playerLevel = XpBar.Instance.level;

        // Oyuncunun seviyesi 10'un katý ve daha önce bu seviyede reklam gösterilmediyse
        if (playerLevel > 0 && playerLevel % 10 == 0 && playerLevel != lastAdLevel)
        {
            adOnVisible = true; // Reklam gösterim durumu ayarlanýyor
            lastAdLevel = playerLevel; // En son reklam gösterilen seviyeyi güncelle

            CrazySDK.Ad.RequestAd(adType, () =>
            {     
                print("Reklam baþladý");
            },
            (error) =>
            {
                adOnVisible = false;
                print("Reklam hatasý, tekrar gösterilmeyecek: " + error);
            },
            () =>
            {
                adOnVisible = false;
                print("Reklam Bitti! Oyuncu yeniden doðuyor!");
            });
        }
        else
        {
            adOnVisible = false;
        }
    }

    public void Check()
    {
        if(adType == CrazyAdType.Midgame)
        {
            adType = CrazyAdType.Midgame;
        }
    }
}
