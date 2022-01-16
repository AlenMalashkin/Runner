using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text coinsCountText;

    private void Start()
    {
        int coinsCount = PlayerPrefs.GetInt("coins");
        coinsCountText.text = coinsCount.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
}
