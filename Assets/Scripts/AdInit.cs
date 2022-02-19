using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdInit : MonoBehaviour
{
    private void Awake ()
    {
        MobileAds.Initialize(initStatus => { });
    }
}
