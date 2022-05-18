using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAbility : Monster
{


    private MonsterBehavior monsterBehavior; // initializes the variable called monsterBehavior to store a MonsterBehavior instance.

    [HideInInspector]
    public bool exploding = false; // if the bomb is currently in the process of exploding


    public float explodeTime = 2f;

    private float monsterColor = 0;

    public GameObject explosionPrefab;

    private void Awake()
    {
        monsterBehavior = GetComponent<MonsterBehavior>(); // get the monster behavior component of the game object. 
        
    }

    private void Start()
    {
        monsterBehavior.monster = this; // this explosion attack1 override. so that the attack changes for the monster.
    }

    public override void Attack1(GameObject gameObject) // override the attack1 behavior of the monster.
    {
        base.Attack1(gameObject);

 
        if (!exploding) // if it's not exploding, continue seeking the player to explode
        {
            exploding = true; // explode true when Attack1() is called so it doesnt explode or move later

            monsterBehavior.OnAttack(gameObject, "Explode", 0f); // call the on attack in the monsterBehavior so the player gets damaged.
            StartCoroutine(AsyncDelay.AsyncDelayTask(explodeTime, () => //async delay so the animation can kick in first, smoother.
            {

                // async delay to when it should explode


             
                GameObject prefab = Instantiate(explosionPrefab, transform.position + Vector3.up, Quaternion.identity);
                // spawn the explosion prefab, so the explosion can take place


                WorldManager.instance.monsterRemainingUpdate.UpdateRemainingMonsters(
                    WorldManager.instance.monsterRemainingUpdate.GetRemainingMonsters() - 1); // reduce the remaining monsters by 1 because it exploded.


                prefab.transform.parent = this.gameObject.transform; // make the explosion prefab's parent to this game object which is the monster
                Destroy(this.gameObject, 0.5f); // destroy the monster






            }));

        }

   





    }


    private void Update() 
    {

      

        if(exploding)
        {
            monsterColor += Time.deltaTime * 4; // the rate of how fast the monster is going to turn red

            monsterBehavior.GetComponent<MonsterSetup>().skin.GetComponent<SkinnedMeshRenderer>().material.color = new Color(monsterColor, 0, 0);

            // change the skin mesh renderer's material color to the red that we added in monsterColor variable.
        }

    }







}
