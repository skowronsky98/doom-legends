using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGameScene : UIManager
{
    [SerializeField] private Canvas infoCanvas;
    [SerializeField] private SwipeManager swipeManager;

    private CardSpawner cardSpawner;

    private bool clicked = false;
    private float clickedTime;
    private float requierdHoldTime = 0.75f;
    
    private bool turnOnSwipe = false;
    private float turnOnTime;

    
    private int index = 0;
    protected override void Start()
    {
        base.Start();
        
        cardSpawner = GameObject.FindWithTag("CardSpawner").GetComponent<CardSpawner>();
        
        infoCanvas.enabled = false;
        scoreDisplay.EnableRoundScore();
    }
    
    private void Update()
    {
        if (clicked)
        {
            clickedTime += Time.deltaTime;
            if (clickedTime >= requierdHoldTime)
                ShowInfoCanvas();                
        }
        else
            Reset();
                
        
        if(infoCanvas.enabled)
            CloseInfoCanvas();

        if (turnOnSwipe)
        {
            turnOnTime += Time.deltaTime;
            if (turnOnTime >= 0.2f)
            {
                swipeManager.enabled = true;
                turnOnTime = 0;
                turnOnSwipe = false;
            }
        }
    }

    public void OnPointerDown(int cardIndex)
    {
        index = cardIndex;
        clicked = true;
    }
    public void OnPointerUp()
    {
        clicked = false;
    }
    
    private void Reset()
    {
        clicked = false;
        clickedTime = 0;
    }

    private void ShowInfoCanvas()
    {
        infoCanvas.enabled = true;
        cardSpawner.Cards[index].InfoButton();
        
        swipeManager.enabled = false;

        Reset();
    }
    public void CloseInfoCanvas()
    {
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                infoCanvas.enabled = false;
                turnOnSwipe = true;
            }            
        }

        #region ComputerInput

        if (Input.GetMouseButtonDown(0))
        {
            infoCanvas.enabled = false;
            turnOnSwipe = true;
        }

        #endregion
        
    }
    
}
