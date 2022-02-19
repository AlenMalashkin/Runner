using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    [SerializeField] public Text coinsText;
    public int coinsCount;
    public string itemName;
    public int cost;

    void Start()
    {
        coinsCount = PlayerPrefs.GetInt("coins");
        coinsText.text = coinsCount.ToString();
    }

    public void buyItem()
    {
        if (coinsCount >= cost)
        {
            coinsCount -= cost;
            PlayerPrefs.SetInt("coins", coinsCount);
            coinsText.text = coinsCount.ToString();
            PlayerPrefs.SetString(itemName, itemName);
            PlayerPrefs.Save();
            Debug.Log("Item " + itemName + " bought");
        }
    }

    public void buyItemBoost ()
    {
        cost = 1000;
        itemName = "boost";

        if (!PlayerPrefs.HasKey("boost"))
            buyItem();
    }

    public void buyItemShield()
    {
        cost = 500;
        itemName = "shield";

        if (!PlayerPrefs.HasKey("shield"))
            buyItem();
    }

    public void buyItemBoostX3()
    {
        cost = 10000;
        itemName = "boostx3";

        if (!PlayerPrefs.HasKey("boostx3"))
            buyItem();
    }
}
