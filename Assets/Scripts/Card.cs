using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    [SerializeField] protected int id;
    [SerializeField] public Sprite cardSprite;
    [SerializeField] protected Animator animExplosion;
    [SerializeField] protected Text dmgText;
    
    protected int health, maxHealth;
    protected SwipeManager swipeManager;
    protected CardSpawner cardSpawner;
    protected GameObject cardInfoGO;
    
    protected Text infoTxt;

    protected Player player;

    public float animDelay = 0.2f;
    //Text indexText;
    
    protected virtual void Start()
    {
        if (!SceneManager.GetSceneByName("BestiaryScene").isLoaded)
        {
            cardSpawner = GameObject.FindGameObjectWithTag("CardSpawner").GetComponent<CardSpawner>();
            swipeManager = GameObject.FindGameObjectWithTag("SwipeManager").GetComponent<SwipeManager>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        dmgText.enabled = false;
    }

    public virtual void Ability()
    {
        //swipeManager.Delay = true;
    }

    public virtual bool Explosion(int damage, bool playAnimation)
    {
        if(playAnimation)
            animExplosion.Play("Explosion");
        
        if (damage >= health)
        {
            DestroyDelay();
            return false;
        }
        
        this.health -= damage;
        UpdateText();
        return true;
    }

    public virtual void InfoButton()
    {
        cardInfoGO = GameObject.FindGameObjectWithTag("CardInfo");
        cardInfoGO.GetComponent<Canvas>().enabled = true;

        infoTxt = cardInfoGO.GetComponentInChildren<Text>();
    }
    
    public virtual void UpdateText() { }

    public virtual void Explode() { }

    public virtual bool AfterMoveExplosion(int index) { return true;}
    
    public virtual void AfterMove(int index) { }
    
    public virtual void TokenInRowAward() { }
   
    public virtual void PlayAnimation(){}
    public virtual void PlayAnimation(int index){}

    public void DisplayDamageText(int damage)
    {
        dmgText.enabled = true;
        dmgText.text = "- " + damage;
        Invoke("DisableDmgTxt", 0.2f);
    }
    
    protected void DisableDmgTxt()
    {
        dmgText.enabled = false;
    }
    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    protected virtual void DestroyDelay()
    {
        Invoke("Destroy", animDelay);
    }

    public int Health => health;

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }
}
