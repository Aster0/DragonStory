using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
* 
*  SINGLETON class (FOR THE PLAYER) that initializes once on awake, so we can access this ObjectsHandler's instance anywhere and get important data.
*  
*  We can know what the player current health, damage, etc is and call it anywhere in any scripts. We can also set new damages, health if the player
*  equips item or add the stats.
*/
public class PlayerEntity : MonoBehaviour
{
    #region Singleton // singleton instanced class - loads once on runtime. (different to an object) since we have only one player for now, we can use a singleton instance to run it once and save everything.


    /*
     * Getter and setters properties so we can get and set the variables safely.
     */


    public static PlayerEntity player { get; set; } // save the intsance of the PlayerEntity singleton in the player variable. w/ a getter and setter


    public int health { get; set; } // stores the player health per instance. can expand to have multiple players in the future.
    public float attackSpeed { get; set; }

    public int damage1 { get; set; } // for attack1 damage.
    public float knockback { get; set; }

    public long level { get; set; }

    public int jumpHeight { get; set; }

    public int coins { get; set; }

    public int potions { get; set; }

    public float wavesCompleted { get; set; }


    public int statPoints { get; set; }

    public int extraDamage { get; set; }
    public int extraAtkSpeed { get; set; }
    public int extraHealth { get; set; }


    public List<Item> inventory;
    public Item[] equipment { get; set; }

    public bool inEquipment { get; set; }

    public bool tutorialLevel;

    public bool inUI { get; set; } // if true, prevents player from attacking

    private void OnValidate() // called when I change the variables in the sceneview 
    {

        if(ObjectsHandler.instance != null) // if instance is alreaedy created
            ObjectsHandler.instance.inventoryManager.UpdateWholeInventory(); // we'll update inventory again, this is for debugging so i can edit the inv in the scene viewr

    }

    void Awake() // on awake
    {

        if(player == null)
        {
            player = this; // save this singleton instance into the player variable.

            if(!tutorialLevel) // if not tutorial level then
                DontDestroyOnLoad(gameObject); // dont destroy this game object when changing scenes
        }
        else
        {
            Destroy(gameObject); // destroy this object
        }
   
    }





    #endregion



}
