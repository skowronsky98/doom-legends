using System;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    private Swipe swipe;

    [SerializeField]
    Player player;

    [SerializeField]
    MoveController moveController;

    [SerializeField]
    CardSpawner cardSpawner;

    private Vector3 desiredPosition;
    private bool isMoving = false;
    private bool secondCardIsNotMoving = true;
    private bool canShift = false;

    private bool canMove = true;
    private bool canSwap = false;

    public BoardManager boardManager;

    private int playerPositionIndex = 5;
    private int movePositionIndex = 5;

    private GameObject followCardGo;
    private int followCard = 0;
    private int followCardTargetIndex = 0;

    private GameObject followSecondCardGo;
    private int followSecondCard = 0;
    private int followSecondCardTargetIndex = 0;

    float playerSpeed = 10f, cardSpeed = 12f;

    private bool delay = true;

    private void Start()
    {
        swipe = GetComponent<Swipe>();
        desiredPosition = new Vector3(boardManager.TransformSpace[5].transform.position.x, boardManager.TransformSpace[5].transform.position.y,boardManager.TransformSpace[5].transform.position.z);
    }

    private void Update()
    {
        if (!isMoving && secondCardIsNotMoving)
        {
            moveController.OnMoveEnd();
            
            if (swipe.SwipeLeft)
            {    
                delay = false;

                if (playerPositionIndex % 3 != 1)
                {
                    
                    if (playerPositionIndex == 3)
                        followSecondCard = 9;
                    else if (playerPositionIndex == 9)
                        followSecondCard = 3;

                    movePositionIndex = playerPositionIndex - 1;

                    SetDesiredPosition(movePositionIndex, playerPositionIndex);

                    if(!canSwap)
                        followCard = moveController.SwipeLeftFollowCard(playerPositionIndex);
                }
            }

            if (swipe.SwipeRight)
            {
                delay = false;
                
                if (playerPositionIndex % 3 != 0)
                {
                    if (playerPositionIndex == 1)
                        followSecondCard = 7;
                    else if (playerPositionIndex == 7)
                        followSecondCard = 1;

                    movePositionIndex = playerPositionIndex + 1;

                    SetDesiredPosition(movePositionIndex, playerPositionIndex);

                    if (!canSwap)
                        followCard = moveController.SwipeRightFollowCard(playerPositionIndex);

                }
            }

            if (swipe.SwipeUp)
            {

                delay = false;
                
                if (playerPositionIndex > 3)
                {
                    if (playerPositionIndex == 7)
                        followSecondCard = 9;
                    else if (playerPositionIndex == 9)
                        followSecondCard = 7;

                    movePositionIndex = playerPositionIndex - 3;

                    SetDesiredPosition(movePositionIndex, playerPositionIndex);

                    if (!canSwap)
                        followCard = moveController.SwipeUpFollowCard(playerPositionIndex);

                    
                }
            }

            if (swipe.SwipeDown)
            {
                delay = false;
                
                if (playerPositionIndex < 7)
                {
                    if (playerPositionIndex == 1)
                        followSecondCard = 3;
                    else if (playerPositionIndex == 3)
                        followSecondCard = 1;

                    movePositionIndex = playerPositionIndex + 3;

                    SetDesiredPosition(movePositionIndex, playerPositionIndex);

                    if (!canSwap)
                        followCard = moveController.SwipeDownFollowCard(playerPositionIndex);

                }
            }
        }

        //If player came to destiantion then cards can move 
        if (player.transform.position == desiredPosition && playerPositionIndex != movePositionIndex)
        {
            isMoving = false;

            if (!canSwap)
            {
                secondCardIsNotMoving = false;

                //Collect data abotut follow card
                canShift = true;
                followCardGo = cardSpawner.CardsOnTable[followCard];
                followCardTargetIndex = playerPositionIndex;


                //ShiftCard In List
                moveController.MoveCard(followCard, followCardTargetIndex);

                //Check if second Card need to shift
                if (followSecondCard != 0)
                {
                    followSecondCardGo = cardSpawner.CardsOnTable[followSecondCard];
                    followSecondCardTargetIndex = followCard;
                    moveController.MoveCard(followSecondCard, followSecondCardTargetIndex);
                }
            }
            else
            {
                moveController.SwapWitfPlayer(playerPositionIndex, movePositionIndex);
                canSwap = false;
                secondCardIsNotMoving = true;
                followSecondCard = 0;
            }

            //Debug.Log("P Index" + cardSpawner.CardsOnTable.IndexOf(player.gameObject));
            playerPositionIndex = movePositionIndex;
            //Debug.Log(playerPosition);
        }

        if(delay)
            player.transform.position = Vector3.MoveTowards(player.transform.position, desiredPosition, playerSpeed * Time.deltaTime);

        if (canShift)
        {

            followCardGo.transform.position = Vector3.MoveTowards(followCardGo.transform.position, boardManager.TransformSpace[followCardTargetIndex].transform.position, cardSpeed * Time.deltaTime);

            if (followSecondCard != 0)
            {
                followSecondCardGo.transform.position = Vector3.MoveTowards(followSecondCardGo.transform.position, boardManager.TransformSpace[followSecondCardTargetIndex].transform.position, cardSpeed * Time.deltaTime);

            }


            if (followCardGo.transform.position == boardManager.TransformSpace[followCardTargetIndex].transform.position && followSecondCard == 0)
            {
                cardSpawner.SpawnNewCard(followCard);
                secondCardIsNotMoving = true;
                canShift = false;
            }
            else if (followCardGo.transform.position == boardManager.TransformSpace[followCardTargetIndex].transform.position && followSecondCardGo.transform.position == boardManager.TransformSpace[followSecondCardTargetIndex].transform.position && followSecondCard != 0)
            {
                cardSpawner.SpawnNewCard(followSecondCard);
                secondCardIsNotMoving = true;
                followSecondCard = 0;
                canShift = false;
            }

        }

        if (canSwap)
        {
            delay = true;
            cardSpawner.CardsOnTable[movePositionIndex].transform.position = Vector3.MoveTowards(cardSpawner.CardsOnTable[movePositionIndex].transform.position, boardManager.TransformSpace[playerPositionIndex].transform.position, playerSpeed * Time.deltaTime);
        }
    }

    void SetDesiredPosition(int movePos, int playerPos)
    {
        
        moveController.UseCard(playerPositionIndex, movePos);

        if (canMove)
        {
            desiredPosition = boardManager.TransformSpace[movePos].position;
            isMoving = true;
        }
        else
        {
            movePositionIndex = playerPositionIndex;
            canMove = true;
        }

    }

    public int MovePositionIndex { get { return movePositionIndex; } }
    public int PlayerPositionIndex { get { return playerPositionIndex; } }
    public bool CanMove { get { return canMove; } set { canMove = value;} }
    public bool CanSwap { get { return canSwap; } set { canSwap = value;} }

    public bool Delay
    {
        get => delay;
        set => delay = value;
    }
}
