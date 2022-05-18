using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
 /*
 * 
 *  SINGLETON class (FOR THE WILD AREA) that initializes once on awake, so we can access this ObjectsHandler's instance anywhere and get important data.
 *  
 *  We save things like UI game objects here because we want to instance it once at the start, instead of finding the instance again every time
 *  we need to access those game object. this saves on memory.
 */

    #region Singleton // singleton instanced class - loads once on runtime. (different to an object) so we can save different game objects and reference it staticly.

    public static WorldManager instance { get; set; } // save the intsance of the PlayerEntity singleton in the player variable. w/ a getter and setter


    void Awake() // on awake
    {

       
        instance = this; // save this singleton instance into the player variable.
       
        

    }

    private void Start()
    {
        deadScene.SetActive(false); // turn off the deadScene UI, so that the gameobject's start functions still load but turn it off so update doesn't keep running.
    }







    #endregion

    public MonsterRemainingUpdate monsterRemainingUpdate; // because prefabs can't reference objects on the scene, so we save the instance here in the singleton so we can use anywhere

    public GameObject deadScene;





}
