using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
   [SerializeField] private AudioSource musicSource;

   public static AudioController Instance;

   private string music = "MuteMusic";
   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      SetSettings(music,LoadSettings(music));
   }

   public void MuteIdleSound()
   {
      musicSource.Stop();
   }

   public void PlayIdleSound()
   {
      if (!musicSource.isPlaying)
      {
         musicSource.Play();
      }
   }

   public bool LoadSettings(string name)
   {
      if (PlayerPrefs.GetInt(name) <= 0)
         return false;

      return true;
   }

   public void SetSettings(string name, bool mute)
   {
      musicSource.mute = mute;
      musicSource.Play();
      
      if (mute)
         PlayerPrefs.SetInt(name, 1);
      else
         PlayerPrefs.SetInt(name, 0);
      
      PlayerPrefs.Save();
   }

  
}
