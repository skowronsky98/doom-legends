using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{

   [SerializeField] private Deck weaponDeck;

   [SerializeField] private Image weaponImage;
   [SerializeField] private Sprite bgSprite;
   
   List<Card> weaponList = new List<Card>();

   private Color bgColor;

   private void Start()
   {
      weaponList.AddRange(weaponDeck.CardList);
      bgColor = weaponImage.color;
   }

   public void SetWeapon(string tag, int id)
   {
      foreach (var item in weaponList)
         if (item.gameObject.CompareTag(tag))
            weaponImage.sprite = item.cardSprite;
      
      weaponImage.color = Color.white;
   }

   public void NoWeapon()
   {
      weaponImage.sprite = bgSprite;
      weaponImage.color = bgColor;
   }

}
