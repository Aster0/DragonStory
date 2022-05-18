using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI expText;


    private string expPlaceholderText = "EXP: %currentexp%/%maxexp% (Lv. %playerlevel%)"; // set a placeholder so we can easily replace it later
    // like %currentexp% with the exp value




    private void UpdateEXPText()  // to be called when the exp text needs to be updated
    {
        expText.text = expPlaceholderText.Replace("%currentexp%", GetEXP().ToString())
            .Replace("%maxexp%", slider.maxValue.ToString()).Replace("%playerlevel%", PlayerEntity.player.level.ToString());

        // set the exp text to the exp place holder but replace the placeholders like %currentexp% to the actualy exp and such.

        
    }

    public void SetMaxEXP(float exp) // set the max exp
    {

        slider.maxValue = exp; // change the max value of the slider]

        UpdateEXPText(); // update the exp text



    }

    public void MinusEXP(float exp) // minuses exp
    {
        slider.value -= exp; // minus off the level.
        UpdateEXPText(); // update the exp text

    }




    public void GainEXP(float exp, GameObject gameObject) // function to gain exp parameter float being the exp that the player should gain.
    {
        float expBeforeAdding = slider.value; // save the exp value before addition.

        slider.value += exp; // add on the level. 
        UpdateEXPText(); // update the exp text

        DamageIndicatorHandler.CreateEXP(gameObject, "+" + exp + " EXP"); // create an exp prefab text to show the exp gain on the game object 
        // supplied in the parameter

        if (slider.value == slider.maxValue) // time to level up because it's at its capacity 
        {


            GameObject particle = Instantiate(ObjectsHandler.instance.levelUpParticle,
                ObjectsHandler.instance.player.transform.position,
                Quaternion.Euler(new Vector3(-90, 0, 0))); // creates the level up particle on the player's pos

            particle.transform.parent = ObjectsHandler.instance.player.transform; // put the player as the parent.

            PlayerEntity.player.level += 1; // level the player.

            float expNeededToLevel = slider.maxValue - expBeforeAdding; // find how much EXP you need to level up


            float expLeft = exp - expNeededToLevel; // use the remaining EXP you didn't use to level up to set the exp bar back.
                                                    // so if you're level 1, your exp capacity is 100 (1 * 100) 
                                                    // if u have 90 exp right now and gain 22, you only need 10 to level up.
                                                    // so you're left with 12. So 12 will be added to your exp after leveling up.

            SetEXP(expLeft); // set the EXP bar to whatever EXP you are left with after deduction.

            SetMaxEXP(PlayerEntity.player.level * 100); // set the new max exp after leveling

            StatsUpdater statsUpdater = ObjectsHandler.instance.statsUI.GetComponent<StatsUpdater>();
            // get the StatsUpdater instance from the player, in the PlayerInteract1 script, getting the StatsUI canvas game object to get the StatsUpdater script.


     

            statsUpdater.AddStatPoints(); // plus one to the total stats points.
        }
    }


    public void SetEXP(float exp) // function to set the exp 
    {
        slider.value = exp; // set the exp
        UpdateEXPText(); // update the exp text
    }


    public float GetEXP() // get the exp
    {
        return slider.value; // return the slider value
    }
}
