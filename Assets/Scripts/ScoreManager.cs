using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    protected static int tokenScore = 0;
    protected static int tokenRoundScore = 0;

    protected virtual void Start()
    {
        tokenScore = PlayerPrefs.GetInt("Tokens");
        
    }

   
    public virtual void CollectTokens(int amountOfTokens)
    {
        tokenScore += amountOfTokens;
        tokenRoundScore += amountOfTokens;
        
        PlayerPrefs.SetInt("Tokens", tokenScore);
        PlayerPrefs.Save();
    }

    public virtual bool BuyItem(int price)
    {
        if (price > 0 && price <= tokenScore)
        {
            tokenScore -= price;
            
            PlayerPrefs.SetInt("Tokens", tokenScore);
            PlayerPrefs.Save();

            return true;
        }

        return false;
    }

   

    

    public int TokenScore => tokenScore;
}
//Delete from Game Object (Scene Manager) from GameScenes