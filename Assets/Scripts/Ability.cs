using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    private Player player;
    private List<Action> abilitiesList = new List<Action>();

    [SerializeField] private CardSpawner cardSpawner;
    private void Awake()
    {
        player = GetComponent<Player>();    
        abilitiesList.Add(() => HealPlayer(player,1));
        abilitiesList.Add(() => AddAttack(player));
        abilitiesList.Add(() => DeaShot(player, cardSpawner,1));
        abilitiesList.Add(() => Regen(player));
        abilitiesList.Add(() => Aegis(player));
    }

    private static void HealPlayer(Player player, int lvl)
    {
        var heal = 2 * lvl;
     
        if (player.Health + heal <= player.MaxHealth)
            player.HealHealth(heal);
        else if (player.Health + heal > player.MaxHealth)
            player.HealToMaxHealth();
    }

    private static void AddAttack(Player player)
    {
        var attack = 1;

        player.Attack += attack;
    }

    private static void DeaShot(Player player, CardSpawner cardSpawner, int lvl)
    {
        var playerPos = cardSpawner.Cards.IndexOf(player);
        
        /*if(playerPos % 3 != 1)
            if (!cardSpawner.Cards[playerPos - 1].Explosion(lvl))
                cardSpawner.SpawnTokens(playerPos - 1);
            else
                cardSpawner.Cards[playerPos - 1].UpdateText();
        if(playerPos % 3 != 0)
            if (!cardSpawner.Cards[playerPos + 1].Explosion(lvl))
                cardSpawner.SpawnTokens(playerPos + 1);
            else
                cardSpawner.Cards[playerPos + 1].UpdateText();
        if(playerPos + 3 < 10)
            if (!cardSpawner.Cards[playerPos + 3].Explosion(lvl))
                cardSpawner.SpawnTokens(playerPos + 3);
            else
                cardSpawner.Cards[playerPos + 3].UpdateText();
        if(playerPos - 3 > 0)
            if (!cardSpawner.Cards[playerPos - 3].Explosion(lvl))
                cardSpawner.SpawnTokens(playerPos - 3);
            else
                cardSpawner.Cards[playerPos - 3].UpdateText();
        Debug.Log("Pow");*/
        
    }

    private static void Regen(Player player)
    {
        player.HealToMaxHealth();
    }
    
    private static void Aegis(Player player)
    {
        player.Shield = 4;
    }
    
    public List<Action> AbilitiesList => abilitiesList;

}
