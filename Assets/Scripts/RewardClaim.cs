using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class RewardClaim : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private string adId = "ca-app-pub-2303810685220137/3709737616";

    private void OnEnable()
    {
        rewardedAd = new RewardedAd(adId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(adRequest);
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        int coins = PlayerPrefs.GetInt("coins");
        coins += 1000;
        PlayerPrefs.SetInt("coins", coins);
    }

    public void ShowAd()
    {
        if (rewardedAd.IsLoaded())
            rewardedAd.Show();
    }
}
