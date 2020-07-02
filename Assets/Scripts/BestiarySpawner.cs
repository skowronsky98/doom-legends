using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestiarySpawner : Spawner
{
    private int amountOfCards = 0;

    private int cardIndex = 0;

    private int index = 1;

    private int amountOfPositions = 6;
    protected override void Start()
    {
        base.Start();
        
        SpawnCards();
    }

    private void SpawnCards()
    {
        amountOfCards = fullDeck.Count;
        cardsOnTable.Add(null);
        cards.Add(null);
        
        for (int i = 1; i < amountOfPositions + 1; i++)
        {
            cardsOnTable.Add((GameObject)Instantiate(fullDeck[cardIndex].gameObject, boardManager.TransformSpace[i].position, boardManager.TransformSpace[i].rotation, this.transform));

            cards.Add(cardsOnTable[i].GetComponent(cardsOnTable[i].tag + "Card") as Card);

            cardIndex++;
        }
    }

    public void LeftArrowButton()
    {
        if (cardIndex == amountOfPositions)
            cardIndex = (amountOfCards / cardIndex) * cardIndex;
        else if (cardIndex == amountOfCards)
            cardIndex -= ((amountOfCards % amountOfPositions) + amountOfPositions);            
        else
            cardIndex -= 12;
        
        Spawn();
    }

    public void RightArrowButton()
    {
        if (cardIndex > amountOfCards - 1)
            cardIndex = 0;
        
        Spawn();
    }


    private void Spawn()
    {
        for (int i = 1; i < amountOfPositions + 1; i++)
        {
            if(cardsOnTable[i].gameObject)
                Destroy(cardsOnTable[i].gameObject);
        }
        
        index = 1;
        
        while (index < 7 && cardIndex < amountOfCards)
        {
            cardsOnTable[index] = (GameObject)Instantiate(fullDeck[cardIndex].gameObject, boardManager.TransformSpace[index].position, boardManager.TransformSpace[index].rotation, this.transform);

            cards[index] = cardsOnTable[index].GetComponent(cardsOnTable[index].tag+"Card") as Card;
            
            cardIndex++;
            index++;
        }
    }
    
    public List<GameObject> CardsOnTable { get { return cardsOnTable; } set { cardsOnTable = value; } }
    public List<Card> Cards { get { return cards; } set { cards = value; } }
}
