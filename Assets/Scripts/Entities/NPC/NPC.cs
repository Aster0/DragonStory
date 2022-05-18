using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    // class to handle interface methods onInteract so we have can override different behaviors to different NPCS on interacting.
    // Start is called before the first frame update


    public virtual void OnInteract() // on npc interact, override later in weapon smith npc, wild area npc, etc
    {
       
    }

    public virtual void OnUnInteract() // on uninteract on npc/ override later in the npc.
    {

    }
}
