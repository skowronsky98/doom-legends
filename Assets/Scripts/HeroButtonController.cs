using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeroButtonController : MonoBehaviour
{

    [SerializeField] private Sprite[] skins;

    private Image image;
    private int selectedSkin;
    
    void Start()
    {
        image = GetComponent<Image>();
        selectedSkin = PlayerPrefs.GetInt("SelectedPlayer", 0);
        image.sprite = skins[selectedSkin];
    }

   
}
