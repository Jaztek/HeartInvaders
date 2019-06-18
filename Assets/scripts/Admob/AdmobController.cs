using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AdmobController
{
    public static RewardedAd rewardedLifeAd;
    public static RewardedAd rewardedBombAd;

    private static string appId = "ca-app-pub-4063805377291607~1839518177";
    private static string lifeAdId = "ca-app-pub-3940256099942544/5224354917";
    private static string bombAdId = "ca-app-pub-3940256099942544/5224354917";

    private static MainController mainController;

    public static void start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        MobileAds.Initialize(appId);
        loadLifeAd();
        loadBombAd();
    }

    private static void loadLifeAd()
    {
        rewardedLifeAd = new RewardedAd(lifeAdId);
        rewardedLifeAd.OnAdLoaded += LifeAdLoaded;
        rewardedLifeAd.OnUserEarnedReward += LifeEarnedReward;
        rewardedLifeAd.OnAdClosed += LifeAdClosed;

        AdRequest requestLife = new AdRequest.Builder().Build();
        rewardedLifeAd.LoadAd(requestLife);
    }

    private static void loadBombAd()
    {
        rewardedBombAd = new RewardedAd(bombAdId);
        rewardedBombAd.OnAdLoaded += BombAdLoaded;
        rewardedBombAd.OnUserEarnedReward += BombEarnedReward;
        rewardedBombAd.OnAdClosed += BombAdClosed;

        AdRequest requestBomb = new AdRequest.Builder().Build();
        rewardedBombAd.LoadAd(requestBomb);
    }

    public static void LifeAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("LifeAdLoaded event received");
    }

    public static void LifeEarnedReward(object sender, Reward args)
    {
        //Convert.ToInt32(args.Amount)
        LoadSaveService.addLifes(10);
        mainController.generateHeartsLifes();
        mainController.updateCanvas();
    }
    public static void LifeAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("LifeAdClosed event received");
        loadLifeAd();
    }

    public static void BombAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("BombAdLoaded event received");
    }

    public static void BombEarnedReward(object sender, Reward args)
    {
        //Convert.ToInt32(args.Amount)
        LoadSaveService.addBoms(3);
    }
    public static void BombAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("BombAdClosed event received");
        loadBombAd();
    }
}
