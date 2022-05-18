using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadManager : MonoBehaviour, IPointerDownHandler // for the death screen UI, attached to it.
{

    public TextMeshProUGUI goldText; // get the gold text so we can update it later
    public void OnPointerDown(PointerEventData eventData) // when clicked
    {
        
        ObjectsHandler.instance.player.transform.position = new Vector3(3.492643f, -0.8121842f, -0.82f); // teleport the player back to the start

        ObjectsHandler.instance.player.GetComponent<PlayerMovement>().move = true; // allow the player to move again
        ObjectsHandler.instance.player.GetComponent<PlayerInteract1>().dying = false; // not dying anymore because when u click, u respawn
   
        HealthBarControl healthBarControl = ObjectsHandler.instance.player.GetComponent<PlayerSetup>().healthBarControl; // get the player's healthbarcontroller
        healthBarControl.Heal(healthBarControl.GetMaxHealth()); // heal the player back to full health.
        SceneManager.LoadScene(2); // load back the home town scene

        ObjectsHandler.instance.player.GetComponent<Animator>().SetBool("Lay Down", false); // set the death animation off.


    }

  
}
