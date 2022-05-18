using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{


    public Transform pickupBounds; // variable to store the pickupBounds transform.
    private float pickRange = 0.3f; // variable to store the pickRange in float.
    public LayerMask playerLayer; // variable to store the LayerMask so we can say only players can pick up the coin..

    public int amount; // variable to store how many coins this one pickup is.


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(pickupBounds.position, pickRange); // draw a wiresphere when you click on the game object in the scene editor
        // so we can see how big the pickup range is and so we can debug. - center point being the pickupBounds' transform pos.
    }

    private void Update() 
    {
        Collider[] colliders = Physics.OverlapSphere(pickupBounds.position, pickRange, playerLayer);
        // get a collider array on where the pickRange is, center point being the pickUpBounds' transform pos. With a layer mask that only detects
        // players.






        if (colliders.Length > 0) // since there's only one player for now, so we can just check if the collider array has a length more than 0.
            // one player means we dont need to iterate the collider array.
        {
            PlayerEntity.player.coins += amount; // if so, update the player's coin.

            ObjectsHandler.instance.goldUpdate.SetGold(PlayerEntity.player.coins); // update visually in the UI

            Destroy(this.gameObject); // destroy the coin as it's already picked up.
        }
         
           
        
    }


}
