using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldGeneration : MonoBehaviour
{

 

    public int maxPlatforms;

    public MonsterRemainingUpdate monsterRemainingUpdate;


    void Awake() // set in awake so if we disable a terrain, it'll disable before spawning a monster through the MonsterSpawner object because it's gone before Start().
    {


   


        int platforms = Random.Range(1, maxPlatforms); // 1 platforms min, 9 max.

        int chance = Random.Range(1, 100); // 1-100 random number


        if (chance <= 25) // 25% chance // initially it was 40% as shown in my video, but it seemed quite frequent so this number is now lowered to 30%.
        {
            // boss map

            platforms = 1; // one platform only

            ObjectsHandler.instance.locationUpdate.SetLocation("Boss Area"); // udpate the location to Boss Area
        }


        monsterRemainingUpdate.UpdateRemainingMonsters(platforms); // update the number of monsters because each platforms = 1 monster (plus the first platform).

        for(int i = 2; i <=  maxPlatforms; i++) // starts at 2, ignoring the first terrain so the first terrain is always there.
        {


            // if the current iteration is not below the random platform number, meaning it's above, so we off the above terrains.

            // let's say, random generated platform number = 5, we start iteration at 2,
            // so 2 is <= 5 but we negate it with a ! so it never goes into this if clause until 6 because
            // 6 <= 5 = false but negates because true. so we start  finding a gameobject called Terrain6 and  turning it off,
            // 7th iteration is the same.
            if (!(i <= platforms))
            {
               
                GameObject terrain = GameObject.Find("Terrain" + (i)); // we'll find that terrain based off the iteration
                terrain.SetActive(false); // disable platform because it's out of the random number.
            }


            

        }

       





    }


}
