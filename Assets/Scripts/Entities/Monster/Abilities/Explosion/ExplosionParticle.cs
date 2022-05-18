using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticle : MonoBehaviour
{


    private MonsterSetup monsterSetup; // to store the MonsterSetup script instance


    // Start is called before the first frame update

    void Start()
    {
        monsterSetup = transform.parent.GetComponent<MonsterSetup>(); // get the parent monster's MonsterSetup script.


     
     
    }


    private void OnTriggerEnter(Collider other) // when a collider triggers this collider trigger
    {
       if(other.gameObject.CompareTag("Monster") 
            || other.gameObject.CompareTag("Player")) // if it's a monster or a player so monsters can actually trigger the explosion too
        {
            GameObject skin = null;

            if(other.gameObject.CompareTag("Player")) // if it's a plaeyr
            {  
                // we will get the player related scripts
                DamageIndicatorHandler.Create(gameObject, false, monsterSetup.damage1); // make a damage indicator at the monster's position with the monster's damage
                // because it killed itself too by exploding

                skin = other.GetComponent<PlayerSetup>().skin; // assign skin to the player's skin

                PlayerSetup playerSetup = other.GetComponent<PlayerSetup>(); // get the player setup instance

            
                playerSetup.healthBarControl.Damage(monsterSetup.damage1); // so we damage the player using the monster's damage



            }
            else if(other.gameObject.CompareTag("Monster")) // if it's a monster
            {
                skin = other.GetComponent<MonsterSetup>().skin; // we'll get the monster's skin instead

                HealthBarControl enemyHealth = other.GetComponent<MonsterBehavior>().healthBarControl; // get the monster's health script instance
                enemyHealth.Damage(monsterSetup.damage1); // and damage the monster using this exploding monster's damage.
            }

            Knockback.KnockbackObject(other.gameObject, this.gameObject, 300); // knock back the collided game object from the monster, with a force of 300
            DamageIndicatorHandler.Create(other.gameObject, false, monsterSetup.damage1);  // create a damage indicator on the collided game object so if it's a player, it shows how much damage the player took from the explosion.

            skin.GetComponent<SkinnedMeshRenderer>().material.color = Color.red; // set the skin material to red (if it's a monster or player, skin was assigned before)

            StartCoroutine(AsyncDelay.AsyncDelayTask(0.32f, () => // async delay to change the material color back to white
            {

                skin.GetComponent<SkinnedMeshRenderer>().material.color = Color.white; // set the skin material back to normal.

            }));
        }
    }
}
