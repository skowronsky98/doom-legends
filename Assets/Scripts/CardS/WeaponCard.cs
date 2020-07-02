using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponCard : Card
{
    //Player player;
    Text strengthText;

    private int min = 4, max = 5;

    protected override void Start()
    {

        base.Start();
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        strengthText = GetComponentInChildren<Text>();

        health = Random.Range(min, max);
        

        UpdateText();
    }

    public override void Ability()
    {
        base.Ability();
        // Debug.Log("Weapn");


        PickWeapon();

        Destroy(gameObject);
    }

    public override void UpdateText()
    {
        strengthText.text = "Attack: " + health.ToString();
    }


    public override void Destroy()
    {
        base.Destroy();
    }

    void PickWeapon()
    {
        if (player.Attack < health)
        {
            player.Attack = health;
            player.PickedWeapon(gameObject.tag, id);

        }
    }
    
    public override void InfoButton()
    {
        base.InfoButton();
        var info = "Weapon";
        
        infoTxt.text = info;

    }
}
