using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Card
{
    [SerializeField] private PlayerClass playerClass;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private MoveController moveController;
    
    [SerializeField] private Image skin;
    [SerializeField] private Text damageText;
    
    
    private int stepCount = 1, baseStepCount = 1, amountOfSteps = 5;
    
    private int attack;
    private int shield;
    private bool poison = false;

    private int weaponId;
    private string weaponTag;
    
    private int classIndex;
    
    private List<Text> statsText = new List<Text>();
    private Ability ability;
    private int abilityIndex;

    private ScoreDisplay scoreDisplay;
    protected override void Start()
    {
        base.Start();
        GetComponentsInChildren<Text>(statsText);
        ability = GetComponent<Ability>();

        classIndex = PlayerPrefs.GetInt("SelectedPlayer", 0);

        health = playerClass.PlayerClasses[classIndex].health + PlayerPrefs.GetInt("UnlockedPlayer" + classIndex) - 1;
        attack = playerClass.PlayerClasses[classIndex].attack;
        shield = playerClass.PlayerClasses[classIndex].shield;
        skin.sprite = playerClass.PlayerClasses[classIndex].skin;
        
        
        
        this.maxHealth = health;
        damageText.enabled = false;
        
        abilityIndex = PlayerPrefs.GetInt("SelectedSkill",-1);
        Debug.Log("Ability index: " + abilityIndex);

        poison = false;
        stepCount = 1;
        

        scoreDisplay = GameObject.FindWithTag("ScoreDisplay").GetComponent<ScoreDisplay>();
        UpdateText();
    }

    public override void Ability()
    {
        if (poison)
            PosionDamage();
        
        AbilityStepCounter();

        if (attack == 0)
            weaponController.NoWeapon();            
        
        /*if(weaponId != weaponController.weaponId)
            weaponController.SetWeapon(weaponId);*/
        
    }
    public override void UpdateText()
    {
        statsText[0].text = shield.ToString();
        statsText[1].text = attack.ToString();
    }

    public override bool Explosion(int damage, bool playAnimation)
    {
        if(playAnimation)
            animExplosion.Play("Explosion");
        
        if (damage  >= health + shield)
        {
            GameOver();
            return true;
        }
        if (damage >= shield)
        {
            damage -= shield;
            shield = 0;
            health -= damage;
            
            DisplayDamageText(damage);
        }
        else if (shield > damage)
        {
            shield -= damage;
            DisplayDamageText(damage);

        }
        return true;
    }

    private void PosionDamage()
    {
        if (health > 1)
            health--;
        else
            poison = false;
    }

    public void GameOver()
    {
        stepCount = 1;
        scoreDisplay.DeactivateRoundScore();
        SceneManager.LoadScene("GameOver");
    }
    
    public override void InfoButton()
    {
        base.InfoButton();
        var info = "Player";
        
        infoTxt.text = info;
    }

    private void AbilityStepCounter()
    {
        if (stepCount - baseStepCount >= amountOfSteps)
        {
            if(abilityIndex != -1)
                ability.AbilitiesList[abilityIndex]();

            baseStepCount = stepCount;
        }
    }

    public void PickedWeapon(string tag, int id)
    {
        weaponId = id;
        weaponTag = tag;
        
        weaponController.SetWeapon(weaponTag, weaponId);
    }

    public void SellWeaponBtn()
    {
        if (attack > 0 && weaponTag != null)
        {
            moveController.UseCard(cardSpawner.CardsOnTable.IndexOf(gameObject));
            weaponController.NoWeapon();
            scoreDisplay.CollectTokens(attack);
            attack = 0;
            UpdateText();
        }
        
    }

    public void TakeDamage(int amountOfDmg)
    {
        health -= amountOfDmg;
    }
    
    public void HealHealth(int amountOfHeal)
    {
        health += amountOfHeal;
    }

    public void HealToMaxHealth()
    {
        health = maxHealth;
    }

    public int Health { get { return health; } }
    
    public int Attack { get { return attack; } set { attack = value; } }

    public int Shield { get => shield; set => shield = value; }

    public int StepCount { get { return stepCount; } set { stepCount = value; } }
    public bool Poison { get { return poison; } set { poison = value; } }

}
