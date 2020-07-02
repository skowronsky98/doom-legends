using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCard : Card
{
    protected override void Start()
    {
        base.Start();
        health = 1;

    }
    
    public override void Ability()
    {
        base.Ability();
               
        swipeManager.CanMove = false;


        cardSpawner.OpenChest(swipeManager.MovePositionIndex);
        Destroy(gameObject);
    }

    public override void InfoButton()
    {
        base.InfoButton();
        var info = "ChestCard";

        infoTxt.text = info;
    }
}
