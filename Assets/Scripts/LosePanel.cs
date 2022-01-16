using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    [SerializeField] Text recordText;

    private void Start()
    {
        int lastScore = PlayerPrefs.GetInt("lastScore");
        int recordScore = PlayerPrefs.GetInt("recordScore");

        if (lastScore > recordScore)
        {
            recordScore = lastScore;
            PlayerPrefs.SetInt("recordScore", recordScore);
            recordText.text = recordScore.ToString();
        } 
        else
        {
            recordText.text = recordScore.ToString();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
