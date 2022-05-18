using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddStats : MonoBehaviour
{
    public StatsUpdater statsUpdater;

    private PlayerEntity player;

    private Button button;


    public int healthIncrement, damageIncrement; // how much the stats will increment by.



    private void Start()
    {
        player = PlayerEntity.player; // cache the singleton into a var

        button = GetComponent<Button>(); // get the button component
        button.onClick.AddListener(OnStatsAdded); // add the onclick listener to the button for the OnStatsAdded method


    }
    public void OnStatsAdded() // on add more stats in stats UI,
    {
    


        if(button.name.Contains("Health")) // player is trying to add health
        {


        
            statsUpdater.AddHealthAmount(healthIncrement); // update the health UI with the healthIncrement in mind

    


        }
        else if (button.name.Contains("Damage")) // player is trying to add damage
        {
            statsUpdater.AddDamageAmount(damageIncrement); // update the damage UI with the damageIncrement in mind

        }

        statsUpdater.RemoveStatPoints(); // deduct one stats point as the player has used it.

    }

    void Update()
    {
       
        if(player.statPoints == 0) // if player doesn't have any stats point
        {
            button.interactable = false; // make it so the button isn't interactable.
        }
        else // player has stat points
            button.interactable = true; // make it so button is interactable.
    }
}
