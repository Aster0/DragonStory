using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInformation : MonoBehaviour // this script is attached to the extra information u see in inventories that shows
    // the description of the item.
{

    public TextMeshProUGUI itemName, itemDescription, itemStats;
    public void SetInformation(string name, string description, string stats) // updates the UI 
    {
        itemName.text = name; // set the item name text into what's supplied in the parameter name.
        itemDescription.text = description; // set the item description text into what's supplied in the parameter description.

        itemStats.text = stats; // set the item stats text into what's supplied in the parameter stats.
    }
}
