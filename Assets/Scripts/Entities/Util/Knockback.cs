using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{


    public static void KnockbackObject(GameObject enemy, GameObject knockbacker, float knockbackRadius)
        // method to call when I want to knockback an obj. enemy means the object to knock back, knockbacker is so we know which direction to knockback from.
        // static so we can call from anywhere
    {
        Rigidbody enemyRigidBody = enemy.GetComponent<Rigidbody>(); // get the enemy's rigidbody

        enemyRigidBody.AddForce((enemyRigidBody.transform.position - knockbacker.transform.position).normalized * knockbackRadius);
        // add force, and calculate the position that should be knockback to multiplied with the knocback radius force.

        Destroy(Instantiate(ObjectsHandler.instance.hitParticle, enemyRigidBody.transform.position
        + new Vector3(0, 1, 0), Quaternion.identity), 0.5f); // spawn hit particles that destroy in 0.5 seconds

    }

}
