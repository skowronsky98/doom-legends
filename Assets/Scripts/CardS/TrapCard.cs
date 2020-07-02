using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TrapCard : Card
{
    //Player player;

    Text stepCounterText;


    int maxMovesCount = 3;
    int currentMove = 0;

    
    private int trapPosition;

    protected override void Start()
    {
        base.Start();
        //playerposition when moves
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        stepCounterText = GetComponentInChildren<Text>();
        
        health = 3;
        if (!SceneManager.GetSceneByName("BestiaryScene").isLoaded)
        {
            currentMove = player.StepCount;
            UpdateText();
        }
    }
 
    public override void Ability()
    {
        base.Ability();
        
        swipeManager.CanSwap = true;
    }

    public override void UpdateText()
    {
        
        if(player.StepCount - currentMove == 2)
            stepCounterText.text = "Grenade: 1";
        else if(player.StepCount - currentMove == 1)
            stepCounterText.text = "Grenade: 2";
        else if (player.StepCount - currentMove == 0)
            stepCounterText.text = "Grenade: 3";
    }

    public override bool AfterMoveExplosion(int index)
    {
        
        if (player != null)
        {
            UpdateText();
        
            if (player.StepCount - currentMove >= maxMovesCount)
            {
                Explode();
            
                DestroyDelay();
            
                cardSpawner.SpawnTokens(index,health);
            }
        }

        return true;
    }


    public override void Explode()
    {
        trapPosition = cardSpawner.CardsOnTable.IndexOf(gameObject);
        animExplosion.Play("Explosion");
        
        if(trapPosition % 3 != 0)
            MakeDamage(trapPosition + 1);
        if (trapPosition % 3 != 1)
            MakeDamage(trapPosition - 1);
        if(trapPosition - 3 > 0)
            MakeDamage(trapPosition - 3);
        if(trapPosition + 3 < 10)
            MakeDamage(trapPosition + 3);
    }
    
    private void MakeDamage(int position)
    {
        Card card = cardSpawner.Cards[position];
        
        if (!card.Explosion(health, true))
            cardSpawner.SpawnTokens(position, health);
        else
            card.UpdateText();

        card.DisplayDamageText(health);
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }

    public override void InfoButton()
    {
        base.InfoButton();
        var info = "Trap";
        
        infoTxt.text = info;


    }
}
