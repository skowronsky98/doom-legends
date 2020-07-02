using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SocreGameOverDisplay : ScoreManager
{
    [SerializeField] private Text roundScoreText;

    private void Start()
    {
        roundScoreText.text = tokenRoundScore.ToString();
    }
    
}
