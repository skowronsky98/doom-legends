using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TrapHorizontalCard : Card
{
    //Player player;

    Text stepCounterText;

    //private int indexOfBomb = 0;

    int maxMovesCount = 3;
    int currentMove = 0;

    private int trapHorizontalPosition;

    protected override void Start()
    {
        base.Start();

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
    
    public override bool AfterMoveExplosion(int index)
    {
        
        if (player != null)
        {
            UpdateText();

            if (player.StepCount - currentMove >= maxMovesCount)
            {
                Explode();

                DestroyDelay();

                cardSpawner.SpawnTokens(index, health);
            }
        }

        return true;
    }
    public override void UpdateText()
    {
        if(player.StepCount - currentMove == 2)
            stepCounterText.text = "Dynamite: 1";
        else if(player.StepCount - currentMove == 1)
            stepCounterText.text = "Dynamite: 2";
        else if (player.StepCount - currentMove == 0)
            stepCounterText.text = "Dynamite: 3";
    }

    public override void Explode()
    {
        trapHorizontalPosition = cardSpawner.CardsOnTable.IndexOf(gameObject);
        animExplosion.Play("Explosion");


        if (trapHorizontalPosition % 3 == 0)
        {
            MakeDamage(trapHorizontalPosition - 1);
            MakeDamage(trapHorizontalPosition - 2);
        }
        else if (trapHorizontalPosition % 3 == 1)
        {
            MakeDamage(trapHorizontalPosition + 1);
            MakeDamage(trapHorizontalPosition + 2);
        }
        else if (trapHorizontalPosition % 3 == 2)
        {
            MakeDamage(trapHorizontalPosition - 1);
            MakeDamage(trapHorizontalPosition + 1);
        }

    }
    
    private void MakeDamage(int position)
    {
        Card card = cardSpawner.Cards[position];
        
        if (!card.Explosion(health, true))
            cardSpawner.SpawnTokens(position,health);
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
        var info = "TrapH";
        
        infoTxt.text = info;


    }

}
