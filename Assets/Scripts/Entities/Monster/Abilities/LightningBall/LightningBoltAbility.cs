using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltAbility : Monster
{
    private MonsterBehavior monsterBehavior; // initializes the variable called monsterBehavior to store a MonsterBehavior instance.
    private MonsterSetup monsterSetup;

    public GameObject lightningBoltPrefab; // stores the lightningBoltPrefab that we should create when attacking


    private float attack1Cooldown;

    private void Awake()
    {
        monsterBehavior = GetComponent<MonsterBehavior>(); // get the monster behavior component of the game object. 

        monsterSetup = GetComponent<MonsterSetup>(); // get the MonsterSetup component.

        attack1Cooldown = monsterBehavior.attack1Cooldown;

    }

    private void Start() // on script start
    {
        monsterBehavior.monster = this; // this lightningbolt ability attack1 override. so the base attack changes to what is overriden in this 
        // script.
    }

    private void Update()
    {
        if (!(attack1Cooldown < 0)) // if it's under 0, stop minusing.
            attack1Cooldown -= Time.deltaTime; // so we can slowly minus it down to 0 again when attack1 is used, cooldown essentially.
    }

 

    public override void Attack1(GameObject gameObject) // override the attack1 behavior of the monster. so the base atttack used in MonsterBehavior script uses this overriden one.
    {
        base.Attack1(gameObject); // call the base Monster class Attack1 method
         
     
     
        if(attack1Cooldown < 0) // if cooldown is lower than 0, meaning ready to attack agn.
        {
            attack1Cooldown = 1f / monsterSetup.attackSpeed; // reset the cooldown so it needs to reach to 0 again.
                                                             // the bigger the attack speed, the smaller the outcome because of the division to the 1f.
            monsterBehavior.OnAttack(gameObject, "LightningBolt", 0f); // call the onAttack method from monsterBeahvior.



            GameObject prefab = Instantiate(lightningBoltPrefab, transform.position + Vector3.up, Quaternion.identity); // instantiate the lightning bolt with the location of the game object but one up (y axis).



            prefab.transform.parent = this.gameObject.transform;  // make the lightningbolt prefab's parent to this game object which is the monster




        }








    }


}
