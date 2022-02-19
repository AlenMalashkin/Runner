using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] public Text score;
    [SerializeField] public int multiplier = 1;
    public GameObject buttonBoost;

    void Start()
    {
        if (!PlayerPrefs.HasKey("boost"))
        {
            buttonBoost.SetActive(false);
        }



        if (PlayerPrefs.HasKey("boostx3"))
            multiplier = 3;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = ((int)player.position.z / 2 * multiplier).ToString();
    }

    public void useScoreBooster()
    {
        multiplier = 2;
        buttonBoost.gameObject.SetActive(false);
        PlayerPrefs.DeleteKey("boost");
    }
}
