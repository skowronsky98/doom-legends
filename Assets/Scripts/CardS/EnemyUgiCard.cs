using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUgiCard : EnemyCard
{
    protected override void Start()
    {
        base.Start();
        health = 5;
        UpdateText();
    }

}
