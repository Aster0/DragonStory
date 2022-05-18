using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAbility : MonoBehaviour
{

    private PlayerInteract1 playerInteract;
    private bool facingLeft = true; // player starts facing left first.

    public float disappearTime = 3f;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        playerInteract = ObjectsHandler.instance.player.GetComponent<PlayerInteract1>();
        Destroy(this.gameObject, disappearTime);


        if (ObjectsHandler.instance.player.transform.rotation.w < 0) // facing left
            facingLeft = true;
        else
            facingLeft = false;


    }

    void FixedUpdate() // makes the particle travel at a constant speed because it's a constant frame update
    {
        Vector3 moveSpeed = new Vector3(Time.deltaTime * speed, 0);
        Vector3 position;

  
        if(facingLeft) // facing left 
        {
            position = transform.position - moveSpeed;
           
        }
        else // facing right
        {
            position = transform.position + moveSpeed;
        }


        transform.position = position;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Monster"))
        {
            playerInteract.OnInteract(other.gameObject, 0f);
        }

        if(!other.gameObject.CompareTag("Player"))
            if(!other.gameObject.CompareTag("Ability")) // if it's not an ability, destroy it - so it goes through abilities
                Destroy(this.gameObject);

        
    }
}
