using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class TokenCard : Card
{
    //private Player player;
    private ScoreDisplay scoreDisplay;
    [SerializeField]
    private Text healText;

    [SerializeField] private Sprite bonusToken;
    [SerializeField] private Image image;

    int amountOfTokens = 0, min = 4, max = 5;
    private float tokenAwardDelay;

    private bool aftermove = false;
    private bool delay = false;

    protected override void Start()
    {
        base.Start();
        
        /*if (SceneManager.GetSceneByName("GameScene1").isLoaded)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();*/
        
        scoreDisplay = GameObject.FindWithTag("ScoreDisplay").GetComponent<ScoreDisplay>();
        
        
        if(amountOfTokens == 0)
            health = Random.Range(min,max);

        tokenAwardDelay = animDelay + 0.2f;
        
        UpdateText();
        aftermove = true;
        
        //Delay Display of Tokens
        if (delay)
        {
            Invoke(nameof(TurnOnTokens), animDelay);
            gameObject.SetActive(false);
        }
    }

    public void TurnOnTokens()
    {
        gameObject.SetActive(true);
    }

    public override void Ability()
    {
        base.Ability();
        scoreDisplay.CollectTokens(health);
        
        Destroy();
    }

    public override void AfterMove(int index)
    {
        if (aftermove)
        {
            if (index % 3 == 2 && cardSpawner.CardsOnTable[index + 1].CompareTag("Token") &&
                cardSpawner.Cards[index - 1].CompareTag("Token"))
            {
                TokenInRowAward();
                cardSpawner.Cards[index + 1].TokenInRowAward();
                cardSpawner.Cards[index - 1].TokenInRowAward();
            }
            if (index > 3 && index < 7 && cardSpawner.CardsOnTable[index + 3].CompareTag("Token") &&
                cardSpawner.CardsOnTable[index - 3].CompareTag("Token"))
            {
                TokenInRowAward();
                cardSpawner.Cards[index + 3].TokenInRowAward();
                cardSpawner.Cards[index - 3].TokenInRowAward();
            }    
        }
    }

    private void TokenAwardDelayed()
    {
        image.sprite = bonusToken;
        UpdateText();
    }
    
    public override void TokenInRowAward()
    {
        health += 3;
        
        if (health > 7)
            health = 7;
        
        Invoke("TokenAwardDelayed", tokenAwardDelay);        
    }

    public override void UpdateText()
    {
        healText.text = "Tokens: " + health;
    }

    public override void InfoButton()
    {
        base.InfoButton();
        var info = "Token";
        
        infoTxt.text = info;
    }

    public int AmountOfTokens {set => health = value; }
    public bool Delay
    {
        set { delay = value; }
    }
}
