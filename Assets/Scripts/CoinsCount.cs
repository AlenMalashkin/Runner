using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCount : MonoBehaviour
{
    [SerializeField] private Text coinsCountText;

    void Start()
    {
        int coinsCount = PlayerPrefs.GetInt("coins");
        coinsCountText.text = coinsCount.ToString();
    }
}
