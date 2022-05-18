using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBallParticle : MonoBehaviour
{

    private bool facingLeft = false; // monster don't face left first. rather right.
    public float speed = 5f;
    public float disappearTime = 3f;

    private MonsterBehavior monsterBehavior;





    // Start is called before the first frame update
    void Start()
    {
        monsterBehavior = transform.parent.GetComponent<MonsterBehavior>(); // get the parent object (monster)'s monster behavior script.
        Destroy(this.gameObject, disappearTime);


        // if the monster's x pos is less than the player's x pos, it means that the player is standing on the right side. while the monster is on the left side
        // So we should make the monster shoot to the right by setting facingLeft to false.
        if (monsterBehavior.gameObject.transform.position.x < ObjectsHandler.instance.player.transform.position.x) 
            facingLeft = false;

        // if the monster's x pos is more than the player's x pos, it means that the player is standing on the left side. while the monster is on the right side
        // So we should make the monster shoot to the left by setting facingLeft to true.
        else
            facingLeft = true;


    }


    void FixedUpdate() // makes the particle travel at a constant speed because it's a constant frame update
    {
        Vector3 moveSpeed = new Vector3(Time.deltaTime * speed, 0);
        Vector3 position;




        if (facingLeft) // facing left 
        {
            position = transform.position - moveSpeed; // we will negate the movespeed so it flys to the left

        }
        else // facing right
        {
            position = transform.position + moveSpeed; // we will add the movespeed so it flys to the right
        }


        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {

        // since the lighting ball is a parent of the monster game object which has a rigidbody, any
        // colliders that the lightning ball hits will have the trigger enter. 
        // this is different from the player's fireball ability as the fireball prefab is the child of the player game object.
        // thus, the fireball prefab only has a trigger, and no rigidbody so it finds a game object with a collider + rigidbody for
        // the trigger enter requirements to be met.

        if (other.gameObject.CompareTag("Player")) // if it's a player
        {
            monsterBehavior.OnDamage(0.1f, other.gameObject); // we will damage the player gameobject
            Destroy(this.gameObject); // then destroy this lightning game object.
        } 



    
    }


}
