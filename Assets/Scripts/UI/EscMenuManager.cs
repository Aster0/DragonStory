using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscMenuManager : MonoBehaviour // attached to the esc menu 
{

    public Button resumeButton, quitButton;
    // Start is called before the first frame update
    void Start()
    {
        resumeButton.onClick.AddListener(OnResume); // add a button on listener from OnResume method.
        quitButton.onClick.AddListener(OnQuit);  // add a button on listener from OnQuit method.
    }

    private void OnResume() // when resume button is clicked
    {
        gameObject.SetActive(false); // hide the game object (the escmenu)

        Time.timeScale = 1f; // normal time
    }

    private void OnQuit() // when click button is clicked
    {
        Application.Quit(); // quit the game
    }


}
