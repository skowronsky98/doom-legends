using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    [System.Serializable] public struct Class
    {
        public string name;
        public int health, attack, shield;
        public Sprite skin;
        public string bio;
    }
    
    
    
    [SerializeField]
    private List<Class> playerClasses = new List<Class>();
   
    public List<Class> PlayerClasses => playerClasses;
}
