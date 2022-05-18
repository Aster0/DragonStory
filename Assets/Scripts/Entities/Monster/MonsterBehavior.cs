using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : Monster
{


    public Transform monsterAttackTransform; // to store the center point of where the attack point should start.
    public float attackRange; // so different monster can have different attack ranges.

    public HealthBarControl healthBarControl; // store the healthbarControl script so we cna control the monster's health
    public EntityInformationUI entityInformationUI; // store the entityInformationUI script.

    public LayerMask attackLayerMask; // store an attack layer mask so it only attacks players.
     
    private Animator animator; // stores an animator to animate the monster.

    [HideInInspector]
    public Monster monster; // interface instance.


    private MonsterSetup monsterSetup; // get the monsterSetup script to get initial setup values.

    [HideInInspector]
    public float attack1Cooldown = 0f; // initialize attack1Cooldown as 0 so the monster can hit right after it spawns.


    private bool dying = false; // check if the monster is dying

  


    private void Awake() // before start()
    {
        monster = this; // put the monster interface to this class because this class is extending the monster interface so we can later
        // override Attack1()

    }

    // Start is called before the first frame update
    void Start()
    {
        monsterSetup = GetComponent<MonsterSetup>(); // get the current monster's setup

        animator = GetComponent<Animator>(); // get the animator component and store inside animator var.


        healthBarControl.SetMaxHealth(monsterSetup.health); // set max health.
        healthBarControl.Heal(monsterSetup.health); // heal up to max health.

        entityInformationUI.SetEntityName(monsterSetup.entityName); // set the entity name using the value obtained from monsterSetup.

    
    }

    // Update is called once per frame
    void Update()
    {
        if (!(attack1Cooldown < 0)) // if it's under 0, stop minusing.
            attack1Cooldown -= Time.deltaTime; // so we can slowly minus it down to 0 again when attack1 is used, cooldown essentially.

        CheckAttackRadius(); // check if there's player in the attack Radius.

        OnDeath(); // check if the monster should die.
    }

    void OnDeath()
    {
        if (healthBarControl.GetHealth() <= 0 && !dying) // 0 health and if it's not already dead.
        {
            animator.SetTrigger("Die"); // trigger the die animation.

            dying = true; // set the dying to true because hp is lower than 0.


            Destroy(this.gameObject, 0.5f); // delete this instance of the game object.


            try // try this block of code first
            { 
                WorldManager.instance.monsterRemainingUpdate.UpdateRemainingMonsters(
            WorldManager.instance.monsterRemainingUpdate.GetRemainingMonsters() - 1); 
                // reduce the remaining monsters by 1 because we just killed one.

            }
            catch(NullReferenceException) // in tutorial, can't reduce the remaining monster so we catch the error
            {
                // do nothing
            }


            StartCoroutine(AsyncDelay.AsyncDelayTask(0.32f, () => //async delay so the animation can kick in first, smoother.
            {
                Destroy(Instantiate(ObjectsHandler.instance.deadParticle, transform.position
                        + new Vector3(0, 1, -1), Quaternion.identity), 0.5f); // show death particles at the monster's location, destroying it 0.5 seconds later.

                GameObject coin = Instantiate(ObjectsHandler.instance.goldCoinPrefab, transform.position
                   + new Vector3(0, 0.5f), Quaternion.Euler(new Vector3(0, -90, -90))); // spawn a coin at the monster's location.
                // rotate the coin by y -90 degrees and z -90 degrees

                coin.GetComponent<CoinPickup>().amount = (int) monsterSetup.level + monsterSetup.difficulty;  
                // access the coin gameobject's CoinPickup script and set the amount to the monster's level + the difficulty.


                EXPBar playerEXPBar = ObjectsHandler.instance.player.GetComponent<PlayerSetup>().expBar; // get the player's exp bar from the 
                // object handler singleton instance

                playerEXPBar.GainEXP(monsterSetup.exp, this.gameObject); // let the player gain exp when the monster dies.

      
                

                


            }));


        }
    }

    public void OnDamage(float delay, GameObject gameObject) // on monster damaged player
    {
        GameObject skin = gameObject.GetComponent<PlayerSetup>().skin;
        StartCoroutine(AsyncDelay.AsyncDelayTask(delay, () => //async delay so the animation can kick in first, smoother.
        {


            DamageIndicatorHandler.Create(gameObject, false, monsterSetup.damage1); // create a damage indicator on the player when it has damaged
            // the player.

            PlayerSetup playerSetup = gameObject.GetComponent<PlayerSetup>(); // get the player PlayerSetup script

            playerSetup.healthBarControl.Damage(monsterSetup.damage1); // in the playerSetup script, access the health and damage the player
            // with the monster's damage

             
            skin.GetComponent<SkinnedMeshRenderer>().material.color = Color.red; // change hte player's material to red to show damage.




            Knockback.KnockbackObject(gameObject, this.gameObject, 100); // knockback the player with the force of 100 and away from the monster (parameter two, this.gameobject)

        }));


        StartCoroutine(AsyncDelay.AsyncDelayTask(delay + 0.18f, () => // async delay to change the material color back to white
        {

            skin.GetComponent<SkinnedMeshRenderer>().material.color = Color.white; // make the player's material back to normal.

        }));
    }


    public void OnAttack(GameObject gameObject, string animation, float delay) // on monster tries to attack player
    {
        // parameter for the gameobject basically takes from the collider which is filtered by the player layer mask so the gameobject fetched
        // is the player.

        if (attack1Cooldown < 0 & !dying) // if it's not under  cooldown, attack.
        {
            

            attack1Cooldown = 1f / monsterSetup.attackSpeed; // reset the cooldown so it needs to reach to 0 again.
                                                             // the bigger the attack speed, the smaller the outcome because of the division to the 1f.
            
            animator.SetTrigger(animation); // trigger the animation stated as the string parameter.



            if(delay > 0)  // if the delay is more than 0
            {
                OnDamage(delay, gameObject); // damage the player.
            }
      
        }
    }

    public override void Attack1(GameObject gameObject) // override the Attack1 so we can change the Attack1's behavior.
    {
        base.Attack1(gameObject); // call Attack1 from the base class (Monster)


        OnAttack(gameObject, "Attack1", 0.32f); // call the onAttack method because it's ready to attack.


    }



    private void CheckAttackRadius() // check the radius if there's any players in it to attack.
    {

        float nearestDistance = float.MaxValue; // start with a max value because the distance is the furtherest so we can slowly narrow down later.


        float distance; // to save the distance of each collider found later in the playerAttackTransform range. 

        Collider[] colliders = Physics.OverlapSphere(monsterAttackTransform.position, attackRange, attackLayerMask);
        // get all the colliders in the attack range with the monsterAttackTransform position being the center.
        // layer mask masks out everything except players.

        Collider nearestCollider = null; // set nearest to null so if we don't get any nearest collider, we can know.



        foreach (Collider collider in colliders)  // iterate the collider array obtained 
        {
            distance = (monsterAttackTransform.position - collider.transform.position).sqrMagnitude; //get the distance between the monster and the player (collider.transform.position)


            if (distance <= nearestDistance) // if distance is lower than the saved nearestdistance, we have found our new nearest.
            {

                // this is so if in the future, we expand and there're more than one players.

                nearestDistance = distance; // set the nearest distance so in the next iteration, we can check if there's a closer collider or not.

                nearestCollider = collider; // set the nearest collider so we know something is found because it's no longer null.
               


            }
        }



        // attack the nearestCollider if exists (not null).

        if (nearestCollider != null)
        {
            // attack the player

            if (ObjectsHandler.instance.player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Die")) // stop attack if plyaer is dead
            {
                return; // return, dont go down further if it's in a die anim.
            }


            monster.Attack1(nearestCollider.gameObject); // call the interface method that we set to just now, and call the overriden Attack1() method. 
            // to attack the player.


        }

            
    }

    private void OnDrawGizmosSelected() // when u click in scene editor
    {

        if (monsterAttackTransform != null) // exists and assigned to the player.
            Gizmos.DrawWireSphere(monsterAttackTransform.position, attackRange); // draw the wiresphere to debug with the monsterattacktransform pos being the center and attack range

    }
    


 
}
