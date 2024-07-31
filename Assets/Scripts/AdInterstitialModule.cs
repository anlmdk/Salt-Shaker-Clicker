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

        // Oyuncunun seviyesi 10'un kat� ve daha �nce bu seviyede reklam g�sterilmediyse
        if (playerLevel > 0 && playerLevel % 10 == 0 && playerLevel != lastAdLevel)
        {
            adOnVisible = true; // Reklam g�sterim durumu ayarlan�yor
            lastAdLevel = playerLevel; // En son reklam g�sterilen seviyeyi g�ncelle

            CrazySDK.Ad.RequestAd(adType, () =>
            {     
                print("Reklam ba�lad�");
            },
            (error) =>
            {
                adOnVisible = false;
                print("Reklam hatas�, tekrar g�sterilmeyecek: " + error);
            },
            () =>
            {
                adOnVisible = false;
                print("Reklam Bitti! Oyuncu yeniden do�uyor!");
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
