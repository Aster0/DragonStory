using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update

    private void Start()
    {
        if(ObjectsHandler.instance.loaded) // check if we have loaded once into the wild area or from after tutorial scene
        {
            Destroy(gameObject); // if yes, when we load back into home town, destroy so no duplicates.
            return; // return, dont go down
        }
      
        DontDestroyOnLoad(gameObject); // dont destroy the object even if we change scenes

    }
 
}
