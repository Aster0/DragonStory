using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupMenu : MonoBehaviour
{

    public float popDuration;
    public TextMeshProUGUI message;



    private void SetDisabled()
    {
        this.gameObject.SetActive(false);
    }

    public void SetMessage(string message)
    {
        this.gameObject.SetActive(true); // set active to true so it shows
        this.message.text = message; // change the message

        Invoke("SetDisabled", popDuration); // set disabled (unshow the menu) after popDuration.
    }

    public void SetMessage(string message, int duration)
    {
        this.gameObject.SetActive(true); // set active to true so it shows
        this.message.text = message; // change the message

        Invoke("SetDisabled", duration); // set disabled (unshow the menu) after popDuration.
    }
}
