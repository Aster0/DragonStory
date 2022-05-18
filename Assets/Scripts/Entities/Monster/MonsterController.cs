using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    public float lookRadius = 5f;
  
    // typical variables to store information below
    private Transform player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Vector3 pathToPosition = new Vector3(0, 0, 0);

    private bool foundPlayer = false;

    private bool exploding = false;

    private ExplosionAbility explosionAbility; // this var is to check later if there's an explosionAbility attached to the monster.
    // so we can make it so the monster wouldn't move when it's exploding.

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // there's only one player so it's fine to use this.
        navMeshAgent = GetComponent<NavMeshAgent>(); // get the navmesh agent component and tie it to the navMeshAgent var
        animator = GetComponent<Animator>(); // get the animator component and tie it to the animator var

        try // try this block
        {
            explosionAbility = GetComponent<ExplosionAbility>(); // try to get the ExplosionAbility component.
        }
        catch(Exception) // if there's an error, means it failed to fetch that ExplosionAbility component means the monster doesn't have it.
        {
           // so do nothing.
        }      
    }

    // Update is called once per frame
    void Update()
    {

        if(explosionAbility != null) // if it's an exploding monster, then 
            exploding = explosionAbility.exploding; // take the explosionAbility instance and get the exploding boolean value.

        if(!exploding) // if exploding is false then we allow monster to move.
        {
            float distance = Vector3.Distance(player.position, transform.position); // see the distance between the player and the enemy (transform.position)





            if (distance <= lookRadius) // if player's distance is in the look radius,
            {

                pathToPosition = player.position; // set the new path to the player's position
                navMeshAgent.SetDestination(pathToPosition); // we move the enemy to the player's position.

                animator.SetBool("Walk", true); // animate the monster walking.


                foundPlayer = true; // foundPlayer is set to true because it found.




            }
            else // if the plaeyr is not in lookRadius
            {
                foundPlayer = false; // found player is set to false.
            }



            if (!foundPlayer) // if no longer found player,
            {
                if (Vector3.Distance(transform.position, pathToPosition) <= navMeshAgent.stoppingDistance + 0.5f) // see if the monster have navigated to the last point it got.
                {
                    animator.SetBool("Walk", false); // if yes, stop the animation.
                    // so the monster is standing still now.

                }
            }
        }
      

    }

    private void OnDrawGizmosSelected() // when selected in scene viewer
    {
        Gizmos.color = Color.red; // set the color to red for gizmos

        Gizmos.DrawWireSphere(transform.position, 5f); // to see where the AI's radius is for looking.
    }
}
