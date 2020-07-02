using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainSwordCard : WeaponCard
{
    protected override void Start()
    {
        base.Start();
        health = Random.Range(5, 6);
    }
}
