using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicatorHandler : MonoBehaviour
{


    private float disappearCooldown;
    private Color textColor;

    public static GameObject Create(GameObject targetObject, bool player, float damage)
    {
        try
        {
            GameObject damageIndicator = ObjectsHandler.instance.playerDamageIndicator; // calls the GameObject saved in the singleton ObjectsHandler to a local variable called damaageIndicator.



            if (!player) // if it's not a player (a monster)
            {
                damageIndicator = ObjectsHandler.instance.monsterDamageIndicator; // change damageIndicator GameObject into a monsterDamageIndicator game object from the singleton objects handler.
            }


            GameObject damageText = InstantiateObject(targetObject, damageIndicator, targetObject.transform.position - targetObject.transform.forward);

            DamageIndicatorHandler damageIndicatorHandler = damageText.GetComponent<DamageIndicatorHandler>(); // get the damageText Gameobject that we insantiated just now and get the DamageIndicatorHandler component (this instance of the script essentially) then, save it to a variable.
            damageIndicatorHandler.SetDamage(damage); // now, we can set that specific damageText's text because we have the instance of the DamageIndicatorHandler.

            return damageText;
        }
        catch (MissingReferenceException) { return null; } // if the player tries to hit a monster when it's in the process of dying, catch it and do nothing
        
    }

    public static GameObject CreateEXP(GameObject targetObject, string exp)
    {
        try
        {
            GameObject expIndicator = ObjectsHandler.instance.expIndicator; // calls the GameObject saved in the singleton ObjectsHandler to a local variable called damaageIndicator.

            GameObject expText = InstantiateObject(targetObject, expIndicator, targetObject.transform.position + targetObject.transform.forward);

            DamageIndicatorHandler damageIndicatorHandler = expText.GetComponent<DamageIndicatorHandler>(); // get the damageText Gameobject that we insantiated just now and get the DamageIndicatorHandler component (this instance of the script essentially) then, save it to a variable.
            damageIndicatorHandler.SetEXP(exp); // now, we can set that specific damageText's text because we have the instance of the DamageIndicatorHandler.

            return expText;
        }
        catch (MissingReferenceException) { return null; } // if the player tries to hit a monster when it's in the process of dying, catch it and do nothing

    }

    private static GameObject InstantiateObject(GameObject targetObject, GameObject indicatorObject, Vector3 spawnPosition)
    {
        Vector3 position = spawnPosition; // get the position, by taking the targetObject's position minusing the targetObject's forward position (1, 0, 0)

        GameObject indicatorText = Instantiate(indicatorObject, // instantiate the damageText into a GameObject variable at position that we calculated just now.. 
             position

            + new Vector3(0f, 1f, -0.3f), Quaternion.identity);  // with an adjusted vector so its at the correct height (Y) and (Z). Quaternion.identity is for no rotations.


        return indicatorText;



    }



    private TextMeshPro text;

    private void Awake() // using awake because it calls earlier than start, needs to call before it starts so it can create properly.
    {
        text = GetComponent<TextMeshPro>();
        disappearCooldown = 1f; // when awake, set the disappear cooldown to 1f so it can slowly go down to 0 later.
        textColor = text.color; // get the color of the text mesh.

    }

    private void SetEXP(string exp)
    {
        text.SetText(exp);
    }

    private void SetDamage(float damage) // set the damage of the text mesh.
    {
        text.SetText(damage.ToString()); // using the parameter of the function, set the textmesh.




    }


    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 1f) * Time.deltaTime; // it'll move at a y speed of 1f up for the damage text game object.

        disappearCooldown -= Time.deltaTime; // same like the attack cooldown system, we slowly minus it using Time.deltaTime from 1f.

        if(disappearCooldown < 0) // after minusing, check if it's below 0, meaning cool down is over.
        {
            textColor.a -= 2f * Time.deltaTime; // set the alpha of the text color to go down at a speed of 2f * Time.deltaTime.

            text.color = textColor; // set the color to the above changed alpha color.

            if(textColor.a < 0) //  if the alpha is under 0, meaning it's not visible already
            {
                Destroy(gameObject); // we destroy the game object to clear up memory.
            }
        }
    }
}
