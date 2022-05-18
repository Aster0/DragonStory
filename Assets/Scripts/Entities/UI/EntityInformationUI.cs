using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityInformationUI : MonoBehaviour
{

    public TextMeshProUGUI entityText;

    /*public void Start()
    {
        entityText.text = entityName;
    }*/ // to be used later in Player Class.


    public void SetEntityName(string name) // so we can access this instance and call the SetEntityName to update the entityText's text.
    {
        // parameter stores the string that we want to update the text into.
        entityText.text = name;
    }



}
