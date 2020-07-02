using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected Deck primaryDeck, primaryDmgDeck, specialDeck, weaponsDeck, enemiesDeck;

    [SerializeField]
    protected BoardManager boardManager;

    protected List<Card> fullDeck = new List<Card>();
   
    protected List<GameObject> cardsOnTable = new List<GameObject>();
    protected List<Card> cards = new List<Card>();

    protected virtual void Start()
    {
        fullDeck.AddRange(primaryDeck.CardList);
        fullDeck.AddRange(primaryDmgDeck.CardList);
        fullDeck.AddRange(specialDeck.CardList);
        fullDeck.AddRange(weaponsDeck.CardList);
        fullDeck.AddRange(enemiesDeck.CardList);
    }
    
}
