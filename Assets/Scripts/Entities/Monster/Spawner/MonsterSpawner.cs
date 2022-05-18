using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    public GameObject[] monsters; // keeps a list of monsters so we can spawn variations.
    // Start is called before the first frame update
    void Start()
    {

        GameObject monster = Instantiate(monsters[Random.Range(0, monsters.Length)], transform.position, Quaternion.Euler(new Vector3(0,82,0))); // rotation of y 82
        MonsterSetup monsterSetup = monster.GetComponent<MonsterSetup>(); // get the monster's stats.


        // snake level = player level + random range from 1-10
        // snake damage = snake level / 2
        // snake health = snake level


        monsterSetup.level = PlayerEntity.player.level + Random.Range(1, 11); // get a random range from 1 to 11 and add on to the player level
                                                                              //
                                                                              // so it scales based off the player's level


        if (ObjectsHandler.instance.locationUpdate.GetLocation().Contains("Boss")) // if it's a boss level
        {
            monsterSetup.level = PlayerEntity.player.level + Random.Range(11, 20); // + lvl 11 - 20


            if (monster.GetComponent<MonsterSetup>().entityName.Contains("Fairy")) // if it's a fairy monster
            {
                monster.GetComponent<MonsterBehavior>().attackRange = 20f; // change the attack range
            }
            else
                monster.GetComponent<MonsterBehavior>().attackRange = 2f; // change the attack range


            if (monster.GetComponent<MonsterSetup>().entityName.Contains("Werewolf")) // if it's a werewolf monster
            {
                monster.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f); // make it bigger
            }
            else // if not, for all monster, we will make it this size.
                monster.transform.localScale = new Vector3(2f, 2f, 2f); 
        }




        monsterSetup.difficulty = Random.Range(1, 4); // difficulty randomized from 1-3.

        monsterSetup.entityName = monsterSetup.entityName + " Lv."  + monsterSetup.level.ToString(); // set the entity name to be entityName (Lv. level)
        monsterSetup.damage1 = monsterSetup.level / 2 * monsterSetup.difficulty; // set the damage to scale with the monster's level / 2 and multiplied by monster difficulty.
        monsterSetup.health = monsterSetup.level * monsterSetup.difficulty; // set the health to scale with the monster's level and multiplied by monster difficulty.
        monsterSetup.exp = monsterSetup.level * 2 * monsterSetup.difficulty; // set the exp to scale with the monster's level * 2 and multiplied by monster difficulty.




        //monsterSetup.attackSpeed = 1f; 
    }

  
}
