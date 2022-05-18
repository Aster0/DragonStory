using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeBackground : MonoBehaviour
{
    // Start is called before the first frame update

    private Image image;

    private bool fade;
    void Start()
    {
        image = GetComponent<Image>(); // get the image component

        fade = false; // set fade to false because should not start fading yet

        Invoke("StartFade", 1.5f); // in 1.5f, set fade to true so we can start the fade
    }

    private void StartFade()
    {
        fade = true;  // in 1.5f, set fade to true so we can start the fade
    }


    void Update()
    {
       
        Color tempColor = image.color; // get the color and save it into a tempColor variable
    

        if (tempColor.a >= 0 && fade) // if alpha is above equal 0 and fade is true 
        {
            // start fading
            tempColor.a -= Time.deltaTime / 2; // fade slowly accordingly to Time.deltaTime / 2

            image.color = tempColor; // reassign the tempcolor then we faded into the actual image's color
        }
        else if(tempColor.a <= 0) // if alpha is less than equal 0, means it's faded
        {

           
            gameObject.SetActive(false); // turn off the game object so this update() doesn't run anymore
        }
       
    }
}
