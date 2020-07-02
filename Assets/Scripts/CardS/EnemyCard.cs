using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyCard : Card
{
    //Player player;
    //Text healthTxt;

    [SerializeField] private int healthS;

    [SerializeField] protected Animator anim;

   // [SerializeField] private Text dmgTxt;

    private int cardPos;

    protected override void Start()
    {
        base.Start();
        //healthTxt = GetComponentInChildren<Text>();
        health = healthS;
        UpdateText();

        dmgText.enabled = false;

    }
    public override void Ability()
    {
        //base.Ability();
        //cardSpawner.CardsOnTable

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        
        Damage();
        UpdateText();
    }

    public override bool Explosion(int damage, bool playAnimation)
    {
        if(playAnimation)
            animExplosion.Play("Explosion");

        if (damage >= health)
        {
            DisplayDamageText(damage);
            health = 0;
            DestroyDelay();
            return false;
        }
        else
        {
            DisplayDamageText(damage);
            this.health -= damage;
            return true;
        }
    }

    protected virtual void Damage()
    {

        cardPos = cardSpawner.CardsOnTable.IndexOf(gameObject);

        if (player.Attack >= health)
        {
            
            DisplayDamageText(health);
            
            player.Attack -= health;
            swipeManager.CanMove = false;
            
            cardSpawner.SpawnTokens(cardPos, health);
            
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
            
            PlayAnimation();

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

    private void Reward()
    {
        cardSpawner.SpawnTokens(cardPos, health);
    }

    public override void PlayAnimation()
    {
        anim.Play("Splash");
    }

    public override void UpdateText()
    {
        //healthTxt.text =  health.ToString();
    }

    public override void InfoButton()
    {
        base.InfoButton();
        var info = "EnemieCard";
        
        infoTxt.text = info;


    }
   
}
