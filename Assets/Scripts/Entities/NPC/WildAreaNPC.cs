using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WildAreaNPC : NPCInteractor // extend the base NPCInteractor script so we can override the OnInteract method so it executes this code instead.
{
    public override void OnInteract() // override the on interact behavior
    {
        base.OnInteract();




        ObjectsHandler.instance.player.transform.position = new Vector3(3.492643f, -0.8121842f, -0.82f); // teleport the user to the given vectr3 location
        SceneManager.LoadScene(3); // load scene 3 which is the wild area scene


        ObjectsHandler.instance.locationUpdate.SetLocation("Wild Area"); // change the location from the ObjectHandler singleton instance to Wild Area

        ObjectsHandler.instance.loaded = true; // loaded to true. 








    }



  
}
