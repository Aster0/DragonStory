using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 
 *  SINGLETON class that initializes once on awake, so we can access this ObjectsHandler's instance anywhere and get important data.
 *  
 *  We save things like UI game objects here because we want to instance it once at the start, instead of finding the instance again every time
 *  we need to access those game object. this saves on memory.
 */

public class ObjectsHandler : MonoBehaviour
{
    #region Singleton // singleton instanced class - loads once on runtime. (different to an object) so we can save different game objects and reference it staticly.

    public static ObjectsHandler instance { get; set; } // save the intsance of the PlayerEntity singleton in the player variable. w/ a getter and setter


    void Awake() // on awake
    {

        if(instance == null)
        {
            instance = this; // save this singleton instance into the player variable.
            DontDestroyOnLoad(gameObject); // dont destroy this game object when changing scenes
         
        }
        else
        {
            Destroy(gameObject);
        }
       
    }





    #endregion


    /*
     * 
     *  Not getters and setters like PlayerEntity so we can actually drag the gameobjects into the variables in the scene editor
     */


    public MonsterRemainingUpdate monsterRemainingUpdate; // because prefabs can't reference objects on the scene, so we save the instance here in the singleton so we can use anywhere
    public LocationUpdate locationUpdate;
    public PopupMenu popMenu;

    public InventoryManager inventoryManager;

    public GoldUpdate goldUpdate; // because prefabs can't reference objects on the scene, so we save the instance here in the singleton so we can use anywhere

    public GameObject playerDamageIndicator, monsterDamageIndicator, expIndicator, npcIndicator, escMenu;

    public GameObject snakeMonster;


    public GameObject player;

    public GameObject hitParticle, deadParticle;

    public GameObject statsUI, goldCoinPrefab, inventoryUI;

    public GameObject[] shopUI; // stores the various shop UIs

    public bool loaded;

    public GameObject equipmentSword, equipmentHelmet;
    public GameObject[] equipmentBracers;

    public EquipManager equipManager;

    public GameObject levelUpParticle;












}
