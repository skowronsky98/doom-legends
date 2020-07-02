using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealPotionCard : Card
{
    //Player player;
    Text healText;


    protected override void Start()
    {
        base.Start();
//        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        healText = GetComponentInChildren<Text>();

        health = Random.Range(2,5);

        UpdateText();
    }

    public override void Ability()
    {
        base.Ability();
        
        if (player.Health + health > player.MaxHealth)
            player.HealToMaxHealth();
        else
            player.HealHealth(health);

        player.Poison = false;
        Destroy(gameObject);
    }

    public override void UpdateText()
    {
        healText.text = "Heal: " + health;
    }

    public override void InfoButton()
    {
        base.InfoButton();
        var info = "Heal";
        
        infoTxt.text = info;


    }

    public override void Destroy()
    {
        base.Destroy();
    }

}
