using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlameThrowerCard : Card
{
    //private Player player;
    
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
        Flames(cardSpawner.CardsOnTable.IndexOf(gameObject));
        
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
   

    private void Flames(int cardPosition)
    {

        switch (id)
        {
            case 203:    //Double
                if (state == 1)
                {
                    if (cardPosition + 3 <= 9)
                    {
                        MakeDamage(cardPosition + 3);
                        PlayAnimation(1);
                    }

                    if (cardPosition - 3 >= 1)
                    {
                        MakeDamage(cardPosition - 3);
                        PlayAnimation(0);
                    }
                }
                else if (state == 3)
                {
                    if (cardPosition + 3 <= 9)
                    {
                        MakeDamage(cardPosition + 3);
                        PlayAnimation(0);
                    }

                    if (cardPosition - 3 >= 1)
                    {
                        MakeDamage(cardPosition - 3);
                        PlayAnimation(1);
                    }
                }
                else if(state == 0)
                {
                    if (cardPosition % 3 != 0)
                    {
                        MakeDamage(cardPosition + 1);
                        PlayAnimation(1);
                    }

                    if (cardPosition % 3 != 1)
                    {
                        MakeDamage(cardPosition - 1);
                        PlayAnimation(0);
                    }
                }
                else if(state == 2)
                {
                    if (cardPosition % 3 != 0)
                    {
                        MakeDamage(cardPosition + 1);
                        PlayAnimation(0);
                    }

                    if (cardPosition % 3 != 1)
                    {
                        MakeDamage(cardPosition - 1);
                        PlayAnimation(1);
                    }
                }
                
                break;
            case 204:    //Single 

                if (state == 0 && cardPosition - 3 > 0)
                {
                    MakeDamage(cardPosition - 3);
                    PlayAnimation(0);
                }
                else if (state == 1 && cardPosition % 3 != 0)
                {
                    MakeDamage(cardPosition + 1);
                    PlayAnimation(0);
                }
                else if (state == 2 && cardPosition + 3 < 10)
                {
                    MakeDamage(cardPosition + 3);
                    PlayAnimation(0);
                }
                else if (state == 3 && cardPosition % 3 != 1)
                {
                    MakeDamage(cardPosition - 1);
                    PlayAnimation(0);
                }
                
                break;
            case 205: //Corner
                if (state == 0)
                {
                    if (cardPosition - 3 > 0)
                    {
                        MakeDamage(cardPosition - 3);
                        PlayAnimation(0);
                    }

                    if (cardPosition % 3 != 0)
                    {
                        MakeDamage(cardPosition + 1);
                        PlayAnimation(1);
                    }       
                }
                else if(state == 1)
                {
                    if (cardPosition % 3 != 0)
                    {
                        MakeDamage(cardPosition + 1);
                        PlayAnimation(0);
                    }

                    if (cardPosition + 3 < 10)
                    {
                        MakeDamage(cardPosition + 3);
                        PlayAnimation(1);
                    }
                }
                else if (state == 2)
                {
                    if (cardPosition + 3 < 10)
                    {
                        MakeDamage(cardPosition + 3);
                        PlayAnimation(0);
                    }

                    if (cardPosition % 3 != 1)
                    {
                        MakeDamage(cardPosition - 1);
                        PlayAnimation(1);
                    }
                }
                else if (state == 3)
                {
                    if (cardPosition - 3 > 0)
                    {
                        MakeDamage(cardPosition - 3);
                        PlayAnimation(1);
                    }

                    if (cardPosition % 3 != 1)
                    {
                        MakeDamage(cardPosition - 1);
                        PlayAnimation(0);                        
                    }
                }
                   
                break;
            
            case 206:    //Tripple
                
                if (state == 0)
                {
                    if (cardPosition - 3 > 0)
                    {
                        MakeDamage(cardPosition - 3);
                        PlayAnimation(0);                        
                    }

                    if (cardPosition % 3 != 0)
                    {
                        MakeDamage(cardPosition + 1);
                        PlayAnimation(2);                        
                    }

                    if (cardPosition % 3 != 1)
                    {
                        MakeDamage(cardPosition - 1);
                        PlayAnimation(1);                        
                    }
                }
                else if(state == 1)
                {
                    if (cardPosition - 3 > 0)
                    {
                        MakeDamage(cardPosition - 3);
                        PlayAnimation(1);                        
                    }

                    if (cardPosition % 3 != 0)
                    {
                        MakeDamage(cardPosition + 1);
                        PlayAnimation(0);                        

                    }

                    if (cardPosition + 3 < 10)
                    {
                        MakeDamage(cardPosition + 3);
                        PlayAnimation(2);                        
                    }
                }
                else if (state == 2)
                {
                    if (cardPosition + 3 < 10)
                    {
                        MakeDamage(cardPosition + 3);
                        PlayAnimation(0);                        
                    }

                    if (cardPosition % 3 != 1)
                    {
                        MakeDamage(cardPosition - 1);
                        PlayAnimation(2);                        
                    }

                    if (cardPosition % 3 != 0)
                    {
                        MakeDamage(cardPosition + 1);
                        PlayAnimation(1);                        
                    }
                }
                else if (state == 3)
                {
                    if (cardPosition - 3 > 0)
                    {
                        MakeDamage(cardPosition - 3);
                        PlayAnimation(2);                        
                    }

                    if (cardPosition + 3 < 10)
                    {
                        MakeDamage(cardPosition + 3);
                        PlayAnimation(1);                                                
                    }

                    if (cardPosition % 3 != 1)
                    {
                        MakeDamage(cardPosition - 1);
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
            cardSpawner.SpawnTokens(position,health);
        else
            card.UpdateText();

        card.DisplayDamageText(health);
    }

    public override void UpdateText()
    {
        //strenghtText.text =  " ------- ";
    }

    public override void PlayAnimation(int index)
    {
        anim[index].Play("Flames");
    }
    
    public override void InfoButton()
    {
        base.InfoButton();
        var info = "Flame";
        
        infoTxt.text = info;
    }

}
