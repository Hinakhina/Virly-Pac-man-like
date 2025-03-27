using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField]private TMP_Text scoreText;
    private int score;
    private int maxScore;


    public void AddScore(int value)
    {
        score += value;

        UpdateUI();
    }


    public void UpdateUI()
    {
        scoreText.text = ": " + score + "/" + maxScore;

    }

    public void SetMaxScore(int value)
    {
        maxScore = value;
        UpdateUI();
    }


    private void Awake()
    {
        score = 0;
        maxScore = 0;
    }

    private void Start()
    {
        UpdateUI();
    }

}