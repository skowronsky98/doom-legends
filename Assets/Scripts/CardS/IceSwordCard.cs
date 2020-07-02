using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSwordCard : WeaponCard
{
    protected override void Start()
    {
        base.Start();
        health = Random.Range(4, 5);
    }
}
