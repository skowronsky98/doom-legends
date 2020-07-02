using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroLevelStars : MonoBehaviour
{

    [SerializeField] private Sprite star, starShadow;
    
    private List<Image> images = new List<Image>();

    private int playerLevel;
    void Start()
    {
        GetComponentsInChildren<Image>(images);

        UpdateStars(PlayerPrefs.GetInt("SelectedPlayer", 0));
    }

    public void UpdateStars(int heroIndex)
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].sprite = starShadow;
        }
        
        playerLevel = PlayerPrefs.GetInt("UnlockedPlayer" + heroIndex, 1);

        for (int i = 0; i < playerLevel; i++)
        {
            images[i].sprite = star;
        }
    }
}
