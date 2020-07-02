using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardSpawner : Spawner
{

    [SerializeField]
    private GameObject player;

    private int amountOfCards = 0;
    private int cardIndex = 0;

    protected override void Start()
    {
        base.Start();
      
        SpawnCards();
    }

    public void SpawnCards()
    {
        amountOfCards = fullDeck.Count;
        cardsOnTable.Add(null);
        cards.Add(null);

        for (int i = 1; i < 10; i++)
        {
            cardIndex = Random.Range(1, amountOfCards);

            //OnlyOneBombOnTable("Trap", "TrapHorizontal");

            if (i != 5)
            {
                cardsOnTable.Add((GameObject)Instantiate(fullDeck[cardIndex].gameObject, boardManager.TransformSpace[i].position, boardManager.TransformSpace[i].rotation, this.transform));

                cards.Add(cardsOnTable[i].GetComponent(cardsOnTable[i].tag + "Card") as Card);
            }
            else
            {
                cardsOnTable.Add(player);

                cards.Add(player.GetComponent<Player>());
            }
        }
    }

    public void SpawnNewCard(int index)
    {
        cardIndex = Random.Range(1, amountOfCards);

        cardsOnTable[index] = (GameObject)Instantiate(fullDeck[cardIndex].gameObject, boardManager.TransformSpace[index].position, boardManager.TransformSpace[index].rotation, this.transform);

        cards[index] = cardsOnTable[index].GetComponent(cardsOnTable[index].tag+"Card") as Card;
    }

    public void SpawnTokens(int index, int amount)
    {
        cardsOnTable[index] = (GameObject)Instantiate(fullDeck[0].gameObject, boardManager.TransformSpace[index].position, boardManager.TransformSpace[index].rotation, this.transform);

        cards[index] = cardsOnTable[index].GetComponent<TokenCard>() as TokenCard;
        TokenCard tokenCard = cards[index].GetComponent<TokenCard>();
        tokenCard.AmountOfTokens = amount;
        tokenCard.Delay = true;

    }
    
    public void SpawnTokens(int index)
    {
        cardsOnTable[index] = (GameObject)Instantiate(fullDeck[0].gameObject, boardManager.TransformSpace[index].position, boardManager.TransformSpace[index].rotation, this.transform);

        cards[index] = cardsOnTable[index].GetComponent<TokenCard>() as TokenCard;
    }


    public void SpawnTrap(int index)
    {
        cardsOnTable[index] = (GameObject)Instantiate(specialDeck.CardList[0].gameObject, boardManager.TransformSpace[index].position, boardManager.TransformSpace[index].rotation, this.transform);

        cards[index] = cardsOnTable[index].GetComponent<TrapCard>() as TrapCard;
    }
    
    
    
    public void OpenChest(int index)
    {
        int cardIndex = Random.Range(0, primaryDeck.CardList.Count);

        while (primaryDeck.CardList[cardIndex].CompareTag("Chest"))
        {
            cardIndex = Random.Range(0, primaryDeck.CardList.Count);
        }
        
        cardsOnTable[index] = (GameObject)Instantiate(primaryDeck.CardList[cardIndex].gameObject, boardManager.TransformSpace[index].position, boardManager.TransformSpace[index].rotation, this.transform);
        
        cards[index] = cardsOnTable[index].GetComponent(cardsOnTable[index].tag+"Card") as Card;


    }
    
    public void OpenDarkChest(int index)
    {
        int cardIndex = Random.Range(0, primaryDmgDeck.CardList.Count);

        while (primaryDmgDeck.CardList[cardIndex].CompareTag("DarkChest"))
        {
            cardIndex = Random.Range(0, primaryDmgDeck.CardList.Count);
        }
        
        cardsOnTable[index] = (GameObject)Instantiate(primaryDmgDeck.CardList[cardIndex].gameObject, boardManager.TransformSpace[index].position, boardManager.TransformSpace[index].rotation, this.transform);
        
        cards[index] = cardsOnTable[index].GetComponent(cardsOnTable[index].tag+"Card") as Card;
    }
    
    private void OnlyOneBombOnTable(string tag1, string tag2)
    {
        int cardCount1 = 0;
        int cardCount2 = 0;

        foreach (GameObject item in cardsOnTable)
        {
            if (item)
            {
                if (item.CompareTag(tag1))
                    cardCount1++;
                if (item.CompareTag(tag2))
                    cardCount2++;
            }
        }

        if (cardCount1 > 0 && cardCount2 > 0)
        {
            while (fullDeck[cardIndex].CompareTag(tag1) || fullDeck[cardIndex].CompareTag(tag2))
            {
                cardIndex = Random.Range(1, amountOfCards);
            }
        }
        else if(cardCount1 > 0)
        {
            while (fullDeck[cardIndex].CompareTag(tag1))
            {
                cardIndex = Random.Range(1, amountOfCards);
            }
        }
        else if(cardCount2 > 0)
        {
            while (fullDeck[cardIndex].CompareTag(tag2))
            {
                cardIndex = Random.Range(1, amountOfCards);
            }
        }
            
    }
    
    
    public List<GameObject> CardsOnTable { get { return cardsOnTable; } set { cardsOnTable = value; } }
    public List<Card> Cards { get { return cards; } set { cards = value; } }
}
