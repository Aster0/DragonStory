using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUpdate : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    
    public void SetGold(int gold) // function to call when want to set the gold
    {
        goldText.text = gold.ToString(); // set the gold text to what's supplied in the parameter. ToString because text requires string.
     
    }

    public string getGold() // get the gold amount
    {
        return goldText.text; // by returning the goldText's text.
    }
}
