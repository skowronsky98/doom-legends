using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIStartScene : UIManager
{
   [SerializeField] private Canvas buttonsCanvas, exitMenu;
   protected override void Start()
   {
      base.Start();
      exitMenu.enabled = false;
      buttonsCanvas.enabled = true;
   }

   public void Exit()
   {
      buttonsCanvas.enabled = false;
      exitMenu.enabled = true;
   }
   
   public void ExitY()
   {
      Application.Quit();
   }
   
   public void ExitN()
   {
      exitMenu.enabled = false;
      buttonsCanvas.enabled = true;
   }

   public void Settings()
   {
      SceneManager.LoadScene("Settings");
   }
   
   public void Info()
   {
      SceneManager.LoadScene("Info");
   }
   
}
