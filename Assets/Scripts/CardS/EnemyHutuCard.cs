using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHutuCard : EnemyCard
{
    protected override void Start()
    {
        base.Start();
        health = 4;
        UpdateText();
    }

    protected override void Damage()
    {
        if (player.Attack >= health)
        {
            DisplayDamageText(health);
            
            player.Attack -= health;
            swipeManager.CanMove = false;
            cardSpawner.SpawnTrap(swipeManager.MovePositionIndex);
            
            PlayAnimation();
            health = 0;
            DestroyDelay();
        }
        else if (player.Attack != 0 && player.Attack < health)
        {
            DisplayDamageText(player.Attack);

            swipeManager.CanMove = false;
            health -= player.Attack;
            player.Attack = 0;
        }
        else if (player.Shield >= health)
        {        
            DisplayDamageText(health);
            player.DisplayDamageText(health);
            
            player.Shield -= health;
            PlayAnimation();
            health = 0;
            DestroyDelay();
        }
        else if (player.Shield != 0 && player.Shield < health)
        {
            DisplayDamageText(health);
            player.DisplayDamageText(health);

            
            health -= player.Shield;
            player.Shield = 0;

            player.TakeDamage(health);


            PlayAnimation();
            health = 0;
            DestroyDelay();
        }
        else
        {
            DisplayDamageText(health);
            player.DisplayDamageText(health);

            player.TakeDamage(health);

            PlayAnimation();
            health = 0;
            DestroyDelay();
        }

        if (player.Health <= 0)
            player.GameOver();
    }
}
