using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDisplayController : MonoBehaviour
{
    
    [SerializeField] private Sprite[] skillIcons;

    private List<Image> image = new List<Image>();
    private int selectedSkill;
    
    void Start()
    {
        GetComponentsInChildren<Image>(image);
        selectedSkill = PlayerPrefs.GetInt("SelectedSkill",-1);

        if (selectedSkill >= 0)
        {
            image[0+1].sprite = skillIcons[selectedSkill];
            image[0+1].color = Color.white;            
        }

        /*foreach (var item in image)
        {
            Debug.Log(item.sprite.name);
        }*/

    }

}
