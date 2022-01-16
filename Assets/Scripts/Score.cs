using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] public Text score;

    // Update is called once per frame
    void Update()
    {
        score.text = ((int)player.position.z / 2).ToString();
    }
}
