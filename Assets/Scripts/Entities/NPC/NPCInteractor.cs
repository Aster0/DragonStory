using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCInteractor : NPC // the base NPC interactor class that extends the NPC interface class.
{

    private GameObject indicatorPrefab;

    private Color textColor;
    private float disappearCooldown;
    private TextMeshPro text;

    private bool disappear, interactable;


    

    // Update is called once per frame
    void Update()
    {


        if(Input.GetButtonDown("Interact")) // if we're pressing the interact button (G)
        {
       

            if(interactable) // and it's interactable
            {
                OnInteract(); // then we will call the OnInteract abstract method that will be overriden based off the NPC type it is.

                //PlayerEntity.player.statPoints += 1;
            }
            
        }


        if (disappear) // if it's time for the word (Press G to interact) to disappear
        {
        
            disappearCooldown -= Time.deltaTime; // same like the attack cooldown system, we slowly minus it using Time.deltaTime from 1f.

            if (disappearCooldown < 0) // after minusing, check if it's below 0, meaning cool down is over.
            {
                textColor.a -= 2f * Time.deltaTime; // set the alpha of the text color to go down at a speed of 2f * Time.deltaTime.

                text.color = textColor; // set the color to the above changed alpha color.

                if (textColor.a < 0) //  if the alpha is under 0, meaning it's not visible already
                {
                    Destroy(indicatorPrefab); // we destroy the game object to clear up memory.
                    disappear = false; // set disappear to false because it's disappeared
                    indicatorPrefab = null; // set indicator to null because there's no more indicator after it disappeared.
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other) // when a collider trigegrs this collider
    {
       
        if (other.gameObject.CompareTag("Player") && indicatorPrefab == null) // if it's a player that triggered the collider, and if the indicatorPrefab is null (meaning not spawned yet so no double spawns)
        {
            indicatorPrefab = Instantiate(ObjectsHandler.instance.npcIndicator, other.transform.position + new Vector3(0, 1.3f, -0.2f), Quaternion.identity); 
            // summon the indicator prefab so the user can be prompted to press a key to interact with the NPC.

            text = indicatorPrefab.GetComponent<TextMeshPro>();
            disappearCooldown = 0.2f; // when awake, set the disappear cooldown to 1f so it can slowly go down to 0 later.
            textColor = text.color; // get the color of the text mesh.
            interactable = true; // set interactable to true because it's in the range to interact.
        }
    
    }

    private void OnTriggerExit(Collider other) // when the collider exits the trigger
    {
      
        disappear = true; // start making the word disappear
        interactable = false; // no longer interactable.
        OnUnInteract(); // call the uninteract abstract method. for things like the ShopNPC to close the menu dependen on what the 
        // NPC script override the OnUnInteract as, like the OnInteract().



    }


}
