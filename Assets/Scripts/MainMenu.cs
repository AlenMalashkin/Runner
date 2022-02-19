using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public Text coinsCountText;
    [SerializeField] public Text recordScoreText;

    private void Start()
    {
        int coinsCount = PlayerPrefs.GetInt("coins");
        coinsCountText.text = coinsCount.ToString();


        if (PlayerPrefs.HasKey("recordScore"))
        {
            int recordScore = PlayerPrefs.GetInt("recordScore");
            recordScoreText.text = "Лучший счет: " + recordScore.ToString();
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ToShop()
    {
        SceneManager.LoadScene(2);
    }
}
