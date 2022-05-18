using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotionQuantity : MonoBehaviour
{
    public TextMeshProUGUI potionQuantityText;
    // Start is called before the first frame update

    public void SetQuantity(int quantity) // to set the quantity of the total potions
    {
        potionQuantityText.text = quantity.ToString(); // update the potion text to reflect on what's parsed in the function's parameter
        PlayerEntity.player.potions = quantity; // player's potions in the player singleton instance to reflect on what's parsed in the function's parameter
    }

    public int GetQuantity() // get the total potion quantity
    {
        return int.Parse(potionQuantityText.text); // return the potion quantity text but parse it into an int.
    }


    public void UsePotion() // when the player uses the potion when pressing Q
    {

        if(PlayerEntity.player.potions > 0) // if player has potions (more than 0 potion)
        {
            potionQuantityText.text = (int.Parse(potionQuantityText.text) - 1).ToString(); // take the potionQuantityText and cast it into an int, then minus one before converting back to a string then setting the text as that string.

            HealthBarControl healthBarControl = ObjectsHandler.instance.player.GetComponent<PlayerSetup>().healthBarControl;
            // get the healthbar control instance from the player's PlayerSetup script.

            healthBarControl.Heal(30); // one potion heals for 30, heal the player for 30 hp.

            PlayerEntity.player.potions -= 1; // minus one potion from the player.


        }


    }
}
