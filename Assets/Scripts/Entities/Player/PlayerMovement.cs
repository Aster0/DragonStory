using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rigidBody; // store the rigidbody so we can do physics update
  
    public float smoothTurningValue = 0.07f; // how smooth the turning should be
    public EntityInformationUI entityInformationUI; // store the entity information UI so we can update it later.

    private Animator animator;  // store the animator so we can call and play animations




    private AudioSource audioSource; // to store the audioSource instance later so we can call and play sounds



    public AudioClip jumpClip; // so we can reference the jump sound



    private bool canJump = false; // check if the player can jump

    float smoothTurningVelocity; // store the smooth turning velocity
    public float speed = 5f; // store the speed at which the player should move at

    public bool move { get; set; } // make them so they can't move for 1s after in the new scene

    // Start is called before the first frame update
    void Start()
    {
        
        entityInformationUI.SetEntityName("Dragozard"); // set the entityInformationUI to show Dragozard as the name
        rigidBody = GetComponent<Rigidbody>(); // store the rigidbody instance into the rigidbody var
        animator = GetComponent<Animator>();  // store the animator instance into the animator var
        audioSource = GetComponent<AudioSource>(); // store the audio source instance into the audioSource var

       

        move = false; // set move = to false

        Invoke("CanMoveAgain", 1f); // invoke a 1 second delay before the player can move agn
       

    }

    private void CanMoveAgain()
    {
        move = true; // set the player to be able to move
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && canJump && !animator.GetCurrentAnimatorStateInfo(0).IsName("Die") && move) 
            // if the player presses the spacebar key to jump, check if they can jump and is not currently in the die animation and can move.
        {




            audioSource.clip = jumpClip; // set the clip to the jump sound
            audioSource.Play(); // then play the sound when jumping

           
            canJump = false; // if so, we set the can jump to false because it just jumped, to prevent double jumping.
            animator.SetTrigger("Jump"); // set the animation to jump
            rigidBody.AddForce(transform.up * PlayerEntity.player.jumpHeight); // use the rigidbody to add force to jump the y value mutiplied by the
            // player's jump height stored in the PlayerEntity singleton instance.




        }
        

    }

    void OnCollisionEnter(Collision other) // check for collision enter
    {

        if (other.gameObject.CompareTag("Jumpable")) // if the player collided with an object that has the tag jumpable,
        {
            canJump = true; // the player can jump.

       
        }
 
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");





        Vector3 movement = new Vector3(moveHorizontal * speed, rigidBody.velocity.y, 0); // rigidBody.velocity.y so that the player can fall down smoothly.



        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Die") || !move)
        {
            return;
        }

        if (moveHorizontal != 0 ) // player is walking
        {


            float moveAngle = Mathf.Atan2(movement.x, 0) * Mathf.Rad2Deg; 
            // we're using atan2 to find the angle of our movement direction using our x and y coordinates.


            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, moveAngle, ref smoothTurningVelocity, smoothTurningValue);
            // using some math, we can smoothen out the turning so that it can also turn to more angles when I press A or D.

            transform.rotation = Quaternion.Euler(0f, angle, 0f); // check the rotation of the angle obtained and put it to the y value
            // so it rotates to the right when needed.

            rigidBody.velocity = movement; //set the rigidbody's velocity to the movement vector3 so it will move based
            // on the inputs i press (A or D)

            animator.SetInteger("Run", 1); // show the run animation



        }
        else // player is stationary
        {
            animator.SetInteger("Run", 0); // stop the run animation
        }








    }
}
