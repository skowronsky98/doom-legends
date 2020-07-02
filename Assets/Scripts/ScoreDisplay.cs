using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScoreDisplay : ScoreManager
{
    [SerializeField] private Text tokensText, roundTokenText;
    [SerializeField] private GameObject tokensInRound;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    protected override void Start()
    {
        DeactivateRoundScore();
        
        base.Start();
        
        UpdateTokenScore();
     
        Debug.Log(1%3);
        
        AudioController.Instance.PlayIdleSound();
    }

    public override void CollectTokens(int amountOfTokens)
    {
        base.CollectTokens(amountOfTokens);
        UpdateTokenScore();
    }


    public void UpdateTokenScore()
    {
        tokensText.text = tokenScore.ToString();
        roundTokenText.text = tokenRoundScore.ToString();
    }

    
    public void ShopScene()
    {
        SceneManager.LoadScene("Shop");
        DeactivateRoundScore();
    }
    
    public void EnableRoundScore()
    {
        tokenRoundScore = 0;
        tokensInRound.SetActive(true);
        //roundTokenText.enabled = true;
        UpdateTokenScore();
        
        AudioController.Instance.MuteIdleSound();
    }
    
    
    public void DeactivateRoundScore()
    {
        tokensInRound.SetActive(false);
        AudioController.Instance.PlayIdleSound();
        //roundTokenText.enabled = false;
    }
}
