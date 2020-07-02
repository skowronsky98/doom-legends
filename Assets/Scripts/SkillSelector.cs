using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class SkillSelector : MonoBehaviour
{
   [SerializeField] private Text skillInfo, selectText, upgradePriceText;
   [SerializeField] private GameObject unlockButton, upgradeButton;
   private ScoreDisplay scoreDisplay;
   private int index = 0;

   private List<Button> skillsButtons = new List<Button>();
   
   private List<int> unlockedSkills = new List<int>();
   private int selectedSkill = 0;
   private int length;

   [SerializeField] private int[] price;
   [SerializeField] private int[] upgradePrice;
   
   private List<Transform> skillPosition = new List<Transform>();
   [SerializeField] private Transform framePosition;
   
   string[] skillInformation = new string[]   // Add short description about ability
   {
      "Healer\nHeal 2 points of life every 10 moves.",
      "Attacker\nIt's giving you +2 to attack every 10 moves.",
      "Dead Shot\nBefor move dealing damage to nearby cards",
      "Regen\nIt's regenerating you hp to max",
      "Aegis\nIt's giving you max amount of Shield"
   };
   
   private void Start()
   {
      GetComponentsInChildren<Button>(skillsButtons);
      GetComponentsInChildren<Transform>(skillPosition);
      skillPosition.RemoveAt(0);
      
      scoreDisplay = GameObject.FindWithTag("ScoreDisplay").GetComponent<ScoreDisplay>();
      
      selectedSkill = PlayerPrefs.GetInt("SelectedSkill", 0);
      index = selectedSkill;
      
      length = skillsButtons.Count;
      
      for (int i = 0; i < length; i++)
      {
         /*PlayerPrefs.DeleteKey("SelectedSkill");
         PlayerPrefs.DeleteKey("UnlockedSkill" + i);*/
         
         unlockedSkills.Add(PlayerPrefs.GetInt("UnlockedSkill" + i,0));
      }
      
      
      if (unlockedSkills[selectedSkill] > 0)
      {
         GetInfo();
         skillInfo.text = skillInformation[selectedSkill];

         var pos = 0;
         
         if (selectedSkill == 0)
            pos = 0;
         else
            pos = 2 * selectedSkill + 1;
         
         framePosition.position = new Vector3(skillPosition[pos].position.x,skillPosition[pos].position.y);      
      }
      else
      {
         unlockButton.SetActive(false);
         upgradeButton.SetActive(false);
         framePosition.gameObject.SetActive(false);         
      }
   }

   public void ChooseSkill(int indexBtn)
   {
      index = indexBtn;

      skillInfo.text = skillInformation[index];
      GetInfo();
      unlockButton.SetActive(true);

      var pos = 0;
         
      if (index == 0)
         pos = 0;
      else
         pos = 2 * index + 1;
      
      framePosition.position = new Vector3(skillPosition[pos].position.x,skillPosition[pos].position.y);      
      framePosition.gameObject.SetActive(true);  
      
   }

   private void GetInfo()
   {
      if (unlockedSkills[index] == 0)
         selectText.text = "Unlock for: " + price[index];
      else if (unlockedSkills[index] > 0 && index != selectedSkill)
         selectText.text = "Select";
      else if (unlockedSkills[selectedSkill] > 0 && index == selectedSkill)
         selectText.text = "Selected";

      if (unlockedSkills[index] > 0 && unlockedSkills[index] <= upgradePrice.Length)
      {
         upgradePriceText.text = upgradePrice[unlockedSkills[index] - 1].ToString();
         upgradeButton.SetActive(true);         
      }
      else
      {
         upgradePriceText.text = "";
         upgradeButton.SetActive(false);         
      }
   }

   public void SelectSkill()
   {
      if (unlockedSkills[index] > 0)
      {
         selectedSkill = index;
         PlayerPrefs.SetInt("SelectedSkill", selectedSkill);
      }
      else
         UnlockSkill();
      
      PlayerPrefs.Save();
      GetInfo();
   }

   private void UnlockSkill()
   {
      if (scoreDisplay.BuyItem(price[index]))
      {
         scoreDisplay.UpdateTokenScore();
         unlockedSkills[index] = 1;
         selectedSkill = index;
         PlayerPrefs.SetInt("SelectedSkill", selectedSkill);
         PlayerPrefs.SetInt("UnlockedSkill" + selectedSkill, unlockedSkills[selectedSkill]);
      }
   }

   public void UpgradeSkill()
   {
      if (unlockedSkills[index] > 0 && unlockedSkills[index] <= upgradePrice.Length)
      {
         if (scoreDisplay.BuyItem(upgradePrice[unlockedSkills[index]-1]))
         {
            scoreDisplay.UpdateTokenScore();
            unlockedSkills[index]++;
            PlayerPrefs.SetInt("UnlockedSkill" + index, unlockedSkills[index]);
            PlayerPrefs.Save();
            GetInfo();
         }

      }
      
      
      
   }





}   
