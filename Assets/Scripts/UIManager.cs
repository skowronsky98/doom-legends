using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   protected ScoreDisplay scoreDisplay;

   protected virtual void Start()
   {
      scoreDisplay = GameObject.FindWithTag("ScoreDisplay").GetComponent<ScoreDisplay>();
      scoreDisplay.GetComponentInChildren<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
   }

   public void LoadGameScene()
   {
      SceneManager.LoadScene("GameScene1");
      //scoreManager.EnableRoundScore();
   }

   public void LoadHeroSceneButton()
   {
      SceneManager.LoadScene("HeroScene");
   }
   
   public void LoadBestiarySceneButton()
   {
      SceneManager.LoadScene("BestiaryScene");
   }

   public void LoadStartScene()
   {
      scoreDisplay.DeactivateRoundScore();
      SceneManager.LoadScene("StartScene");
   }

   public void LoadSkillsScene()
   {
      SceneManager.LoadScene("SkillsScene");
   }

   public void MusicControll(bool isOn)
   {
      AudioController.Instance.SetSettings("MuteMusic", !isOn);
   }
  
}
