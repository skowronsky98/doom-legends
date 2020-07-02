using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private List<Card> deck = new List<Card>();
    
    private void Awake()
    {
        gameObject.GetComponentsInChildren<Card>(deck);
        gameObject.SetActive(false);
    }

    public List<Card> CardList { get { return deck; } }
}
