using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTutorialNPC : NPCInteractor // extend the base NPCInteractor script so we can override the OnInteract method so it executes this code instead.
{

    public override void OnInteract() // override the OnInteract method so we know which NPC we are interacting with. 
    {
        base.OnInteract();

        ObjectsHandler.instance.player.transform.position = new Vector3(3.492643f, -0.8121842f, -0.82f); // teleport the player to the given vector3 location.

        ObjectsHandler.instance.player.GetComponent<PlayerSetup>().expBar.GainEXP(100, ObjectsHandler.instance.player); // let the player earn 100 exp

        ObjectsHandler.instance.loaded = true; // the game has loaded so access the object handler singleton to set loaded to true.
        SceneManager.LoadScene(2); // load scene index 2 which is the home town scene.











    }
}
