using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUpdater : MonoBehaviour
{

    private PlayerEntity player; // to store the player entity singleton later, cache once
    private PlayerSetup playerSetup; // store the playerSetup instance later




    [Header("Player Stats")]
    public TextMeshProUGUI healthAmount, damageAmount, totalHealth, totalDamage, totalAttackSpeed, statPoints, wavesText; 

    // Start is called before the first frame update
    void Start()
    {

        player = PlayerEntity.player; // cache singleton instance.

        // memory effiency.




        playerSetup = player.gameObject.GetComponent<PlayerSetup>();
        // fetch the PlayerSetup instance from the player singleton and assign to playerSetup variable.



    

        AddHealthAmount(player.extraHealth); // to add the initial health
        AddDamageAmount(player.extraDamage); // to add the initial damage

         
        SetTotalHealth(playerSetup.baseHealth + player.extraHealth); // update the total health in UI

        SetTotalDamage(playerSetup.baseDamage + player.extraDamage); // update the total damage in the UI

        SetTotalAttackSpeed(playerSetup.baseAttackSpeed + player.extraAtkSpeed); // update the total attack speed in the UI

        SetStatPoints(player.statPoints); // update the user's stat points UI





    }

    public void AddHealthAmount(int amount) // function to add the health, the parameter determines how much health will be added when the func is called
    {
        healthAmount.text = (int.Parse(healthAmount.text) + amount).ToString(); // update the healthAmount text UI

        player.extraHealth = int.Parse(healthAmount.text); // update the extra health by getting the healthAmount text.


        player.health = playerSetup.baseHealth + player.extraHealth; // calculate the total health by doing 
        // base health + extra health (extra health can be obtained by adding stats or items)

        // because if we want to minus the extra health and set back the base health, we need to know what the base health is, 

        // so we don't add the extra health straight to the base health but rather do a mathematic calculation baseHealth + extraHealth.



        playerSetup.healthBarControl.SetMaxHealth(player.health); // update the health UI seen above the player

        SetTotalHealth(player.health); // update the total health UI in the stats UI.

    }

    public void AddDamageAmount(int amount) // function to add the damage, the parameter determines how much damage will be added when the func is called
    {
        damageAmount.text = (int.Parse(damageAmount.text) + amount).ToString(); // update the damageAmount text UI


        player.extraDamage = int.Parse(damageAmount.text);   // update the extra damage by getting the damageAmount text.

        player.damage1 = playerSetup.baseDamage + player.extraDamage; // calculate the total damage by doing 
        // base damage + extra damage (extra damage can be obtained by adding stats or items)


        SetTotalDamage(player.damage1);  // update the total damage UI in the stats UI.


    }

    public void SetTotalHealth(int amount) // set the total health text  in the UI  with the parameter amount given.
    {
        totalHealth.text = "Total Health: " + amount.ToString(); // update the total health text visual
    }

    public void SetTotalDamage(int amount) // set the total damage  text  in the UI  with the parameter amount given.
    {
        totalDamage.text = "Total Damage: " + amount.ToString(); // update the total damage text visual
    }

    public void SetTotalAttackSpeed(float amount) // set the total atk spd text  in the UI  with the parameter amount given.
    {
        totalAttackSpeed.text = "Total Attack Speed: " + amount.ToString(); // update the total attack speed text visual
    }

    public void SetStatPoints(int amount) // set the total stats point in the UI  with the parameter amount given.
    {
        statPoints.text = "Stat Points Left: " + player.statPoints; // update the stat point text visual
    }

    public void IncreaseWavesComplicated() // increase the wave completed Ui visual
    {
        wavesText.text = "Waves Completed: " + PlayerEntity.player.wavesCompleted; // update the wave text visual
    }

    public void AddStatPoints() // call when we need to add a stat point, at the same time, update the UI.
    {

        
        player.statPoints += 1; // increment player stats by 1

        SetStatPoints(player.statPoints); //  update the total stat points available visual



    }

    public void RemoveStatPoints() // call when we need to remove a stat point, at the same time, update the UI.
    {
        player.statPoints -= 1; // decrement player's stats by one
        SetStatPoints(player.statPoints); //  update the total stat points available visual
    }


}
