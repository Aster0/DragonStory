using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;


public class PlayerInteract1 : Player // for evolution 1, extends the Player class.
{
    // Start is called before the first frame update
    private Animator animator;

    private float attack1Cooldown = 0f, attack2Cooldown = 0f; // to store the cooldown so we know when the player can attack agn

    private AudioSource audioSource; // to store the audioSource instance later so we can call and play sounds

    public PotionQuantity potionQuantity; // to know how many potions the player have by getting the PotionQuantity instance.


    public AudioClip attackClip, winClip; // so we can reference the attack and win sound


    public Transform playerAttackTransform; // to get the center point of the attack radius
    public float attackRange = 0.5f; // get the attack range of the player
    public LayerMask attackLayerMask; // get the layer mask so player only hit monsters.

    public GameObject fireball; // fireball gameobject to spawn later when right click

    private bool attacking; // see if the player is attacking

    public bool dying { get; set; }  // see if the player is dying



    public override void Attack2() // right click attack, overriden method. same concept as the monsters (explained in the monsters' scripts)
    {
        base.Attack2();


        if (attack2Cooldown < 0) // under 0, then allow attack1. (cooldown system)
        {


            audioSource.clip = attackClip; // set the clip to the attack sound
            audioSource.Play(); // then play the sound when attacking

            attack2Cooldown = 1.5f / PlayerEntity.player.attackSpeed; // reset the cooldown so it needs to reach to 0 again.
            // the bigger the attack speed, the smaller the outcome because of the division to the 1f.
            animator.SetTrigger("Fire2"); // play the Fire2 animation








            StartCoroutine(AsyncDelay.AsyncDelayTask(0.3f, () => // async delay so the animation can kick in first, smoother.
            {

               
                Instantiate(fireball, transform.position + Vector3.up + new Vector3(Mathf.Sign(transform.rotation.w) * 1.2f, 0)
                    , Quaternion.identity);
                // spawn the fireball

                // mathf.sign to make it so if the player is looking left, the rotation will be around 0.7 positive, so sign it to 1.
                // but if it's -0.7, sign it to -1.
                // multiply it by *1.2f so the distance is slightly more.


            }));



        }
    }

    public override void Attack1() // override the left click attack
    {

        base.Attack1();


        if (attack1Cooldown < 0) // under 0, then allow attack1. (cooldown system)
        {



            audioSource.clip = attackClip; // set the clip to the attack sound
            audioSource.Play(); // then play the sound when attacking

            attack1Cooldown = 1f / PlayerEntity.player.attackSpeed; // reset the cooldown so it needs to reach to 0 again.
            // the bigger the attack speed, the smaller the outcome because of the division to the 1f.
            animator.SetTrigger("Fire1"); // play the Fire1 aniatmion
           

            attacking = true; // set attacking to true


            StartCoroutine(OnAttack()); // start the OnAttack Coroutine.
  



        }





    }

    
    public void OnInteract(GameObject gameObject, float timer) // when the player interacts with the collider game object
    {

        if (gameObject != null) // incase it's already dying and doesn't exist.
        {


   

            GameObject skin = gameObject.GetComponent<MonsterSetup>().skin; // get the monster skin material


            if (gameObject != null) // check again because this is in an async delay, so it might change.
            {
                


                DamageIndicatorHandler.Create(gameObject, true, PlayerEntity.player.damage1); // create a damage indicator on the monster with the player's damage

                HealthBarControl enemyHealth = gameObject.GetComponent<MonsterBehavior>().healthBarControl; // get the monster's health controller
                enemyHealth.Damage(PlayerEntity.player.damage1); // damage the monster with the player's damage



                skin.GetComponent<SkinnedMeshRenderer>().material.color = Color.red; // set the monster's material to red




                Knockback.KnockbackObject(gameObject, this.gameObject, PlayerEntity.player.knockback); // knock back the monster (gameobject) from the player (this.gameobject) with the force of the player's current knockback
            }



        



            StartCoroutine(AsyncDelay.AsyncDelayTask(timer + 0.5f, () => // async delay to change the material color back to white
            {
                if (gameObject != null) // check again because this is in an async delay, so it might change.
                    skin.GetComponent<SkinnedMeshRenderer>().material.color = Color.white; // change the monster's material back to normal

            }));




            

        }


    }





    void Start() // when the script is loaded,
    {
        animator = GetComponent<Animator>(); // get the game object's animator component.

        audioSource = GetComponent<AudioSource>(); // store the audio source instance into the audioSource var


        Invoke("setUIInactive", 0.1f); // give it a while so it can load up the Start scripts for the StatsUI system.
     


    }

    private void setUIInactive() // set the UIs to inactive
    {
        ObjectsHandler.instance.statsUI.SetActive(false); // set stat ui to not show
        ObjectsHandler.instance.inventoryUI.SetActive(false); // set inventory ui to not show

        ObjectsHandler.instance.equipManager.gameObject.SetActive(false); // set equip ui to not show
    }






    // Update is called once per frame
    void Update()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die")) // so the player can't attack while dead 
        {
            return; // dont go down any further to the codes below if the player is in die animation.
        }

        OnDeath(); // call the onDeath function

 

        if (!(attack1Cooldown < 0)) // if it's under 0, stop minusing.
            attack1Cooldown -= Time.deltaTime; // so we can slowly minus it down to 0 again when attack1 is used, cooldown essentially.

        if (!(attack2Cooldown < 0)) // if it's under 0, stop minusing.
            attack2Cooldown -= Time.deltaTime;  // so we can slowly minus it down to 0 again when attack1 is used, cooldown essentially.


        if (Input.GetButtonDown("Fire1") && !PlayerEntity.player.inUI) // if the player presses left click and is not in a UI
        {
            Attack1(); // attack.
            

        }

        else if(Input.GetButtonDown("Fire2") && !PlayerEntity.player.inUI) // if the player presses right click and is not in a UI
        {

            Attack2(); // attack 2
        }

        else if(Input.GetButtonDown("Use Potion")) // if the player presses Q 
        {

            potionQuantity.UsePotion(); // use potion
        }

        else if(Input.GetButtonDown("Open Stats")) // if the player presses Z 
        {
            ObjectsHandler.instance.statsUI.SetActive(!ObjectsHandler.instance.statsUI.activeSelf); // toggle the stat UI
        }

        else if (Input.GetButtonDown("Open Inventory")) // if the player presses TAB 
        {
            PlayerEntity.player.inUI = !ObjectsHandler.instance.inventoryUI.activeSelf; // toggle in UI
            ObjectsHandler.instance.inventoryUI.SetActive(!ObjectsHandler.instance.inventoryUI.activeSelf); // toggle the inv UI
            ObjectsHandler.instance.equipManager.gameObject.SetActive(false); // turn off equipment so it doesn't clash

            PlayerEntity.player.inEquipment = false; // equipment = false because we turned off the equipment UI

         
        }

        else if (Input.GetButtonDown("Open Equipment")) // if the player presses E 
        {
            PlayerEntity.player.inUI = !ObjectsHandler.instance.equipManager.gameObject.activeSelf;  // toggle in UI
            PlayerEntity.player.inEquipment = !ObjectsHandler.instance.equipManager.gameObject.activeSelf; // toggle the in equipment bool

            ObjectsHandler.instance.equipManager.gameObject.SetActive(!ObjectsHandler.instance.equipManager.gameObject.activeSelf); // toggle the equipment UI
            ObjectsHandler.instance.inventoryUI.SetActive(false); // turn off inventory so it doesn't clash

          

        }
        else if (Input.GetButtonDown("Secret")) // presses L
        {

            // basically lets the player cheat in level and coins (for debugging)
            ObjectsHandler.instance.player.GetComponent<PlayerSetup>().expBar.GainEXP(
                100000000000000000, ObjectsHandler.instance.player);

            PlayerEntity.player.coins += 1000000000;

            ObjectsHandler.instance.goldUpdate.SetGold(PlayerEntity.player.coins);



        }
        else if(Input.GetKeyDown(KeyCode.Escape)) // if player presses ESC
        {

            if(!ObjectsHandler.instance.escMenu.activeSelf) // pause
            {
                Time.timeScale = 0; // stop time
            }
            else // if paused already
            {
                Time.timeScale = 1; // resume time
            }

            ObjectsHandler.instance.escMenu.SetActive(!ObjectsHandler.instance.escMenu.activeSelf); // toggle the esc menu
        } 
    }

    private void OnDrawGizmosSelected() // when selected in scene editor
    {

        if (playerAttackTransform != null) // exists and assigned to the player.
            Gizmos.DrawWireSphere(playerAttackTransform.position, attackRange);
        // draw a gizmo with the center being the playerAttackTransform position with the attackRange.

    }


    private void OnDeath() // when the player dies
    {
        if (ObjectsHandler.instance.player.GetComponent<PlayerSetup>().healthBarControl.GetHealth() <= 0 && !dying) // 0 health and if it's not already dead.
        {

            ObjectsHandler.instance.player.GetComponent<PlayerMovement>().move = false; // cant move
            dying = true; // set dying to true

            animator.SetBool("Lay Down", true); // set the animation to laying down

            int goldToRemove = (int)PlayerEntity.player.level * 2; // calculate the gold to remove, 
            // scalable with the player's level because it's multiplied by 2.

            ObjectsHandler.instance.goldUpdate.SetGold(int.Parse(ObjectsHandler.instance.goldUpdate.getGold()) - (goldToRemove));
            // lose gold based off player level (player level * 2)

            if(WorldManager.instance != null) // in wild area
            {

                GameObject deadScene = WorldManager.instance.deadScene; // get the dead UI

                deadScene.GetComponent<DeadManager>().goldText.text = "You have lost " + goldToRemove + " gold."; // update the dead UI
                deadScene.SetActive(true); // show the dead UI
            }
        
        
        }
    }


    public IEnumerator OnAttack() // when it's attacking
    {
       
        bool biteAnimation = false; // set the bite animation to false because it's not yet played

        while (attacking) // while attack is in motion
        {

            if(!biteAnimation) // and the biteAnimation still hasn't kicked in
            {
                
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) // until the bite animation kicks in
                {
                    
                    biteAnimation = true; // set true so player can inflict damage
                    // bite animation has kicked in, so we are ready to inflict damage.
                }

               
            }
            else // if the biteanimation is true then check for any colliders in the area
            {

                float nearestDistance = float.MaxValue; // start with a max value because the distance is the furtherest so we can slowly narrow down later.


                float distance; // to save the distance of each collider found later in the playerAttackTransform range. 

                Collider[] colliders = Physics.OverlapSphere(playerAttackTransform.position, attackRange, attackLayerMask);
                // get the colliders of the monsters with the playerAttackTransform pos being the center with the attack range.

                Collider nearestCollider = null; // set it to null first so later we can know if we found the nearestcollider or not
            


                foreach (Collider collider in colliders) // iterate the collider array
                {
                    distance = (playerAttackTransform.position - collider.transform.position).sqrMagnitude; // find the distance between the
                    // player and the collider

                    if (distance <= nearestDistance) // if distance is lower than the saved nearestdistance, we have found our new nearest.
                    {



                        nearestDistance = distance; // update the nearest distance so in the next iteration and upcoming ones,
                        // we can check if this is indeed the nearest or there are more nearer ones to attack. 
                        // the nearest collider will be attacked.

                        nearestCollider = collider; // update the nearest collider



                    }
                }




                // attack the nearestCollider if exists (not null).

                if (nearestCollider != null) // if found a target
                {
                    
                    OnInteract(nearestCollider.gameObject, 0.3f); // interact with the nearest game object collider.

                    attacking = false; // stop the attack after attacked too
                }



                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) // if no longer in bite animation
                {
                    attacking = false; // stop the attack (the while loop)
                }



      
                
            }

            yield return null; // wait every update frame
        }

        

    }
}
