using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int currentScore;
    [SerializeField] private Text scoreText;


    public static ScoreManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void AddPoints()
    {
        currentScore++;
        UpdateUI();
    }

    public void RemovePoints()
    {
        currentScore--;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = currentScore.ToString("000");
    }
}
