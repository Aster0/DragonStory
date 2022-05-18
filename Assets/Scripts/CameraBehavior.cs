using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour
{
    
    private GameObject player; // variable for keeping the player game object.

    private Vector3 offset; // variable for keeping the offset vector3.

    void Start()
    {

        player = ObjectsHandler.instance.player; // get the player game object from the singleton instance
        offset = transform.position - player.transform.position; // creates an offset from the position of the camera minus the player's position.
    }

    void LateUpdate() // late update because wait till all the physics calculations are done so we move the camera after
    {
        transform.position = player.transform.position + offset; // moves the camera's position with the offset in mind from the player.
    }
}