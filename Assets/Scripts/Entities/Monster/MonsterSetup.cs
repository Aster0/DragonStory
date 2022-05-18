using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSetup : MonoBehaviour
{

    /*
     * 
     * Class to setup the monster in the scene editor to set damage, health, etc.
     */
 
    public GameObject skin; // we wont know which monster is spawned, so the prefab saves the skin to tell us.

    public string entityName;
    public float damage1;
    public long health;
    public float attackSpeed = 1;

    [HideInInspector]
    public long level;
    [HideInInspector]
    public int difficulty;
    [HideInInspector]
    public long exp;



}
