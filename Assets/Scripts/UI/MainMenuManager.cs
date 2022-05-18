using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button playButton, quitButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(OnPlay);  // add a button on listener from OnPlay method.
        quitButton.onClick.AddListener(OnQuit);  // add a button on listener from OnQuit method.
    }

    private void OnPlay()
    {
        SceneManager.LoadScene(1); // Load the main scene
    }


    private void OnQuit()
    {
        Application.Quit(); // quit the game
    }

   
}
