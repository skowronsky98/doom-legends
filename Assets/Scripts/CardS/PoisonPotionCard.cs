using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PoisonPotionCard : Card
{
   
   //Player player;
   Text poisonText;


   protected override void Start()
   {
      base.Start();
      //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
      poisonText = GetComponentInChildren<Text>();

      health = 2;

      UpdateText();
   }
   public override void Ability()
   {
      base.Ability();
      
      if (player.Health + player.Shield <= health)
      {
         player.GameOver();
      }
      else
      {
         if (player.Shield >= health)
         {
            player.Shield -= health;
            player.HealHealth(1);
         }
         else
         {
            health -= player.Shield;
            player.Shield = 0;
            player.TakeDamage(health - 1);

         }
         player.Poison = true;
      }
      
      Destroy(gameObject);
      
   }
   
   public override void UpdateText()
   {
      poisonText.text = "Poison: " + health;
   }
   
   public override void InfoButton()
   {
      base.InfoButton();
      var info = "Poison";
      
      infoTxt.text = info;


   }
   
}
