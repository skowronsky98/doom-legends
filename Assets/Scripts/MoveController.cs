using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField]
    private SwipeManager swipeManager;

    [SerializeField]
    private CardSpawner cardSpawner;

    [SerializeField]
    private Player player;

    private float delayTime = 0.05f, time = 0;
    
    private bool invokeAfterMoveExplosion = false, invokeAfter = false;

    [SerializeField]
    private float delayMove = 0.2f;

    public static MoveController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UseCard(int playerIndex, int cardIndex)
    {
        
        player.StepCount++;

        cardSpawner.Cards[cardIndex].Ability();
        cardSpawner.Cards[swipeManager.PlayerPositionIndex].Ability();
        cardSpawner.Cards[swipeManager.PlayerPositionIndex].UpdateText();

        //Shift Player Card in Cards List
        if (swipeManager.CanMove && !swipeManager.CanSwap)
            MoveCard(playerIndex, cardIndex);
        
        invokeAfterMoveExplosion = invokeAfter = true;
        time = 0;

        Invoke("TurnOnSwiperManager", delayMove);

    }

    public void UseCard(int playerPosition)
    {
        player.StepCount++;

        cardSpawner.Cards[swipeManager.PlayerPositionIndex].Ability();
        cardSpawner.Cards[swipeManager.PlayerPositionIndex].UpdateText();
        
        invokeAfterMoveExplosion = invokeAfter = true;
        time = 0;
        
        Invoke("TurnOnSwiperManager", delayMove);
    }
    
    public void OnMoveEnd()
    {
        for (var i = 1; i < cardSpawner.Cards.Count; i++)
            if (invokeAfterMoveExplosion)
                cardSpawner.Cards[i].AfterMoveExplosion(i);
        invokeAfterMoveExplosion = false;

        time += Time.deltaTime;

        if (time >= delayTime)
        {
            if(invokeAfter)
                for (var i = 1; i < cardSpawner.Cards.Count; i++)
                    cardSpawner.Cards[i].AfterMove(i);
            invokeAfter = false;
        }
    }

    public void MoveCard(int fromIndex, int toIndex)
    {
        cardSpawner.CardsOnTable[toIndex] = cardSpawner.CardsOnTable[fromIndex];
        cardSpawner.CardsOnTable[fromIndex] = null;

        cardSpawner.Cards[toIndex] = cardSpawner.Cards[fromIndex];
        cardSpawner.Cards[fromIndex] = null;
    
    }

    public void SwapWitfPlayer(int fromIndex, int toIndex)
    {

        cardSpawner.CardsOnTable[0] = cardSpawner.CardsOnTable[toIndex];
        cardSpawner.CardsOnTable[toIndex] = cardSpawner.CardsOnTable[fromIndex];
        cardSpawner.CardsOnTable[fromIndex] = cardSpawner.CardsOnTable[0];
        cardSpawner.CardsOnTable[0] = null;

        cardSpawner.Cards[0] = cardSpawner.Cards[toIndex];
        cardSpawner.Cards[toIndex] = cardSpawner.Cards[fromIndex];
        cardSpawner.Cards[fromIndex] = cardSpawner.Cards[0];
        cardSpawner.Cards[0] = null;

    }

    private void TurnOnSwiperManager()
    {
        swipeManager.Delay = true;
    }

    #region FollowCardIndex

    public int SwipeLeftFollowCard(int playerIndex)
    {
        if(playerIndex % 3 != 0)
        {
            return playerIndex + 1;
        }
        else if (playerIndex == 6)
        {
            return playerIndex = 3;
        }
        else
        {
            return playerIndex = 6;
        }
    }

    public int SwipeRightFollowCard(int playerIndex)
    {
        if (playerIndex % 3 == 2)
        {
            return playerIndex - 1;
        }
        else if (playerIndex == 4)
        {
            return playerIndex = 7;
        }
        else
        {
            return playerIndex = 4;
        }
    }

    public int SwipeUpFollowCard(int playerIndex)
    {
        if (playerIndex < 7)
        {
            return playerIndex + 3;
        }
        else if (playerIndex == 8)
        {
            return playerIndex = 9;
        }
        else
        {
            return playerIndex = 8;
        }
    }

    public int SwipeDownFollowCard(int playerIndex)
    {
        if (playerIndex > 3)
        {
            return playerIndex - 3;
        }
        else if (playerIndex == 2)
        {
            return playerIndex = 1;
        }
        else
        {
            return playerIndex = 2;
        }
    }

    #endregion

    public bool InvokeAfter
    {
        get => invokeAfter;
        set => invokeAfter = value;
    }
}
