using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HeroList : MonoBehaviour
{
    private ScoreDisplay scoreDisplay;
    
    [SerializeField] private Text levelText, maxHealtText, descriptionText, unlockButtonText, upgradePrcieText;
    private List<SpriteRenderer> heroList = new List<SpriteRenderer>();
    private int lenght = 0;
    private int index = 0;
    private List<int> unlockedPlayers = new List<int>();
    private int selectedHero;

    private int maxHealth = 10;

    [SerializeField] private PlayerClass playerClass;
    
    [SerializeField] private int[] heroPrice;
    [SerializeField] private int[] upgradePrice;
    [SerializeField] private HeroLevelStars heroLevelStars;
    [SerializeField] private GameObject upgradeButton;
    private void Start()
    {
        GetComponentsInChildren<SpriteRenderer>(heroList);
        lenght = heroList.Count;

        selectedHero = PlayerPrefs.GetInt("SelectedPlayer", 0);
        index = selectedHero;
        
        for (int i = 0; i < lenght; i++)
        {
            /*PlayerPrefs.DeleteKey("UnlockedPlayer" + i);
            PlayerPrefs.DeleteKey("SelectedPlayer");*/

            heroList[i].enabled = false;
            
            unlockedPlayers.Add(PlayerPrefs.GetInt("UnlockedPlayer" + i, 0));
        }

        //Unlock defult Heros
        if (unlockedPlayers[0] == 0)
        {
            unlockedPlayers[0] = 1;
            PlayerPrefs.SetInt("UnlockedPlayer0", 1);
            PlayerPrefs.Save();
            
        }
        
        heroList[index].enabled = true;

        scoreDisplay = GameObject.FindWithTag("ScoreDisplay").GetComponent<ScoreDisplay>();
        
        CheckUnlockedPlayers();
    }

    public void LeftButton()
    {
        heroList[index].enabled = false;
        
        if (index > 0)
            index--;
        else
            index = lenght - 1;

        heroList[index].enabled = true;

        CheckUnlockedPlayers();
        heroLevelStars.UpdateStars(index);
    }
   
    public void RightButton()
    {
        heroList[index].enabled = false;

        if (index < lenght-1)
            index++;
        else
            index = 0;

        heroList[index].enabled = true;
        
        CheckUnlockedPlayers();
        
        heroLevelStars.UpdateStars(index);

    }

    private void CheckUnlockedPlayers()
    {
        if (unlockedPlayers[index] < 1)
        {
            unlockButtonText.text = "Unlock for: " + heroPrice[index];
            levelText.text = "Level: 1";
            
            maxHealth = unlockedPlayers[index] + playerClass.PlayerClasses[index].health;
            maxHealtText.text = maxHealth.ToString();
        }
        else
        {
            
            if (selectedHero == index)
                unlockButtonText.text = "Selected";
            else
                unlockButtonText.text = "Select";
            
            levelText.text = "Level: " + unlockedPlayers[index];
            maxHealth = unlockedPlayers[index] + playerClass.PlayerClasses[index].health - 1;
            maxHealtText.text = maxHealth.ToString();
        }

        if (unlockedPlayers[index] > 0 && unlockedPlayers[index] <= upgradePrice.Length)
        {
            upgradePrcieText.text = upgradePrice[unlockedPlayers[index] - 1].ToString();
            upgradeButton.SetActive(true);
        }
        else
        {
            upgradePrcieText.text = " ";
            upgradeButton.SetActive(false);
        }
        

    }

    public void UnlockButton()
    {
        if (unlockedPlayers[index] == 0)
        {
            if (scoreDisplay.BuyItem(heroPrice[index]))
            {
                scoreDisplay.UpdateTokenScore();
                unlockedPlayers[index] = 1;
                PlayerPrefs.SetInt("UnlockedPlayer" + index, unlockedPlayers[index]);
                selectedHero = index;
                PlayerPrefs.SetInt("SelectedPlayer", selectedHero);
            }
        }
        else
        {
            selectedHero = index;
            PlayerPrefs.SetInt("SelectedPlayer", selectedHero);
        }
        CheckUnlockedPlayers();
        PlayerPrefs.Save();
    }

    public void UpgradeButton()
    {
        if (unlockedPlayers[index] > 0 && unlockedPlayers[index] <= upgradePrice.Length)
        {
            if (scoreDisplay.BuyItem(upgradePrice[unlockedPlayers[index]-1]))
            {
                scoreDisplay.UpdateTokenScore();
                unlockedPlayers[index]++; 
                PlayerPrefs.SetInt("UnlockedPlayer" + index, unlockedPlayers[index]);
                PlayerPrefs.Save();
                CheckUnlockedPlayers();
                heroLevelStars.UpdateStars(selectedHero);

            }
        }
    }
    
    
    
}

    