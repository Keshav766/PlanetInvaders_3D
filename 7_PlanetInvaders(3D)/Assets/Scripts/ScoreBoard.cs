using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    float Score = 0f;
    TMP_Text scoreText;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    public void UpdateScore(float scoreToUpdate)
    {
        Score += scoreToUpdate;
        scoreText.text = Score.ToString();
    }
}
