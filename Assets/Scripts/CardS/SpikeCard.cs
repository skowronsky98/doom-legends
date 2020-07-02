using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpikeCard : Card
{
    Text strenghtText;
    int state = 0, prevStep;

    private float angle = 0;
    [SerializeField]
    private Transform go;

    [SerializeField] private List<Animator> anim;
    
    protected override void Start()
    {
        base.Start();
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        strenghtText = GetComponentInChildren<Text>();
        
        if (!SceneManager.GetSceneByName("BestiaryScene").isLoaded)
            RotateFlameThrowerOnStart();

        health = 3;
        
        UpdateText();
    }

 

    public override void Ability()
    {
        Spike(cardSpawner.CardsOnTable.IndexOf(gameObject));
        
        DestroyDelay();
        //Destroy(gameObject);

    }

    public override bool AfterMoveExplosion(int index)
    {
        if (player != null)
        {
            if (prevStep != player.StepCount)
            {
                if (state == 3)
                    state = 0;
                else
                    state++;

                go.transform.Rotate(go.transform.rotation.x, go.transform.rotation.y, -90f);

                prevStep = player.StepCount;

                UpdateText();
            }
        }

        return true;
    }

    private void RotateFlameThrowerOnStart()
    {
        state = Random.Range(0,4);
        prevStep = player.StepCount;
        
        switch (state)
        {
            case 0:
                angle = 0f;
                break;
            case 1:
                angle = -90f;
                break;
            case 2:
                angle = -180f;
                break;
            case 3:
                angle = -270f;
                break;
        }
        
        go.transform.Rotate(go.transform.rotation.x, go.transform.rotation.y,angle);
    }
   

    private void Spike(int cardPosition)
    {

        int playerPos = cardSpawner.Cards.IndexOf(player);
        
        switch (id)
        {
            case 208:    //Double
                if (state == 1)
                {
                    if (cardPosition + 3 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(1);
                    }

                    if (cardPosition - 3 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(0);
                    }
                }
                else if (state == 3)
                {
                    if (cardPosition + 3 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(0);
                    }

                    if (cardPosition - 3 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(1);
                    }
                }
                else if(state == 0)
                {
                    if (cardPosition + 1 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(1);
                    }

                    if (cardPosition - 1 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(0);
                    }
                }
                else if(state == 2)
                {
                    if (cardPosition + 1 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(0);
                    }

                    if (cardPosition - 1 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(1);
                    }
                }
                
                break;
            case 207:    //Single 

                if (state == 0 && cardPosition - 3 == playerPos)
                {
                    MakeDamage(playerPos);
                    PlayAnimation(0);
                }
                else if (state == 1 && cardPosition + 1 == playerPos)
                {
                    MakeDamage(playerPos);
                    PlayAnimation(0);
                }
                else if (state == 2 && cardPosition + 3 == playerPos)
                {
                    MakeDamage(playerPos);
                    PlayAnimation(0);
                }
                else if (state == 3 && cardPosition - 1 == playerPos)
                {
                    MakeDamage(playerPos);
                    PlayAnimation(0);
                }
                
                break;
            case 209: //Corner
                if (state == 0)
                {
                    if (cardPosition - 3 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(0);
                    }

                    if (cardPosition + 1 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(1);
                    }       
                }
                else if(state == 1)
                {
                    if (cardPosition + 1 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(0);
                    }

                    if (cardPosition + 3 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(1);
                    }
                }
                else if (state == 2)
                {
                    if (cardPosition + 3 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(0);
                    }

                    if (cardPosition - 1 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(1);
                    }
                }
                else if (state == 3)
                {
                    if (cardPosition - 3 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(1);
                    }

                    if (cardPosition - 1 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(0);                        
                    }
                }
                   
                break;
            
            case 210:    //Tripple
                
                if (state == 0)
                {
                    if (cardPosition - 3 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(0);                        
                    }

                    if (cardPosition + 1 == playerPos)
                    {
                        MakeDamage(cardPosition + 1);
                        PlayAnimation(2);                        
                    }

                    if (cardPosition - 1 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(1);                        
                    }
                }
                else if(state == 1)
                {
                    if (cardPosition - 3 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(1);                        
                    }

                    if (cardPosition + 1 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(0);                        

                    }

                    if (cardPosition + 3 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(2);                        
                    }
                }
                else if (state == 2)
                {
                    if (cardPosition + 3 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(0);                        
                    }

                    if (cardPosition - 1 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(2);                        
                    }

                    if (cardPosition + 1 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(1);                        
                    }
                }
                else if (state == 3)
                {
                    if (cardPosition - 3 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(2);                        
                    }

                    if (cardPosition + 3 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(1);                                                
                    }

                    if (cardPosition - 1 == playerPos)
                    {
                        MakeDamage(playerPos);
                        PlayAnimation(0);                                                                        
                    }
                }
                
                break;
        }

    }

    private void MakeDamage(int position)
    {
        Card card = cardSpawner.Cards[position];
        
        if (!card.Explosion(health, false))
            cardSpawner.SpawnTokens(position,3);
        else
            card.UpdateText();

        card.DisplayDamageText(health);
    }

    public override void UpdateText()
    {
        //strenghtText.text =  " ------- ";
    }

    private void PlayAnimation(int index)
    {
        //anim[index].Play("Flames");
    }
    
    public override void InfoButton()
    {
        base.InfoButton();
        var info = "Spike";
        
        infoTxt.text = info;


    }

        
}
