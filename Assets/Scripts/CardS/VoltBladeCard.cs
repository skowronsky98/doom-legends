using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VoltBladeCard : WeaponCard
{
    protected override void Start()
    {
        base.Start();
        health = 5;
        UpdateText();
    }
}
