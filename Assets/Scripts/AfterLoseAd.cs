using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AfterLoseAd : MonoBehaviour
{
    private InterstitialAd interstitialAd;
    private string adId = "ca-app-pub-2303810685220137/9166084595";

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        interstitialAd = new InterstitialAd(adId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }

    public void ShowAd()
    {
        if (interstitialAd.IsLoaded())
            interstitialAd.Show();
    }
}