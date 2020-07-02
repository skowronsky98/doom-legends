using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldCard : Card
{
    //Player player;
    Text shieldText;

    int min = 1, max = 4;

    protected override void Start()
    {
        base.Start();
        shieldText = GetComponentInChildren<Text>();

        health = Random.Range(min, max);

        UpdateText();
    }

    public override void Ability()
    {
        base.Ability();

        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        PickShield();

        Destroy(gameObject);
    }

    public override void UpdateText()
    {
        shieldText.text = "Shield: " + health.ToString();
    }


    void PickShield()
    {
        if (player.Shield < health)
            player.Shield = health;
    }
    
    public override void InfoButton()
    {
        base.InfoButton();
        var info = "Shield";
        
        infoTxt.text = info;


    }
    
}
