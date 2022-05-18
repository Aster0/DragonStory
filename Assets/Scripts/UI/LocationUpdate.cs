using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocationUpdate : MonoBehaviour
{
    public TextMeshProUGUI locationText;

    public void SetLocation(string location) // update the location
    {
        locationText.text = location; // set the location's text to the parameter string location
    }

    public string GetLocation() // get location
    {
        return locationText.text; // return the location text's text.
    }
}
