using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroCollider : MonoBehaviour
{

    public TextMeshProUGUI introText2; // public var so we can drag in the introText2 and introText3 into the var
    public TextMeshProUGUI introText3;
    

    void OnTriggerEnter(Collider collision) // when a collider triggers this collider
    {

        
        if (collision.gameObject.CompareTag("Intro Collider 2")) // check if the tag is Intro Collider 2
        {
           
            introText2.transform.localScale = new Vector3(1, 1, 1); // if it is, we make the scale normal so it shows
         

        }
        else if (collision.gameObject.CompareTag("Intro Collider 3")) // check if the tag is Intro Collider 3
        {
            introText3.transform.localScale = new Vector3(1, 1, 1); // if it is, we make the scale normal so it shows


        }
    }
}

