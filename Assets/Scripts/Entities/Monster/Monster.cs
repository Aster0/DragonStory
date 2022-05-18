using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{



    public virtual void Attack1(GameObject gameObject) // The monster's attack1, so we can override and give it different abilities for different mobs.
    {

    }

    public virtual void Attack2() // light attack, virtual so we can modify each evolution's attack.
    {

    }

}