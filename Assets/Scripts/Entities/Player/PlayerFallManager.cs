using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        
        if(other.gameObject.CompareTag("Player")) // if the player enters the trigger,
        {
            other.gameObject.transform.position = new Vector3(3.492643f, -0.8121842f, -0.82f); // teleport the player back up because they fell.
        }
    }
}
