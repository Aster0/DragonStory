using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterRemainingUpdate : MonoBehaviour
{

    public TextMeshProUGUI monsterRemainingText;

    private int remainingMonsters;

    private int remainingTimeLeft;


    private IEnumerator enume;

    private IEnumerator UpdateTimeRemaining()
    {
        yield return new WaitForSeconds(1); // wait 1 second before going down

        remainingTimeLeft--; // minus the remaining time


         
        monsterRemainingText.text = "You'll be transported back in " + remainingTimeLeft + " seconds"; // update the text with the remaining time

        if (remainingTimeLeft > 0) // if above 0 then start one more to countdown
        {
            StartCoroutine(UpdateTimeRemaining()); // do it all over again, to minus until it's 0.
        }
        else // if it's 0 already
        {
            SceneManager.LoadScene(2); // load the home town scene

            ObjectsHandler.instance.player.transform.position = new Vector3(3.492643f, -0.8121842f, -0.82f); // teleport player 

            ObjectsHandler.instance.locationUpdate.SetLocation("Home Town"); // update the top text to Home Town.
        }
    }
    
    public void UpdateRemainingMonsters(int number) // to update the remaining monsters
    {
        monsterRemainingText.text = number + " monsters remaining"; // update the remaining monster text with the number supplied in the parameter
        remainingMonsters = number; // update the remaining monsters to the number supplied in the parameters

        if (remainingMonsters == 0) // if remaining monsters is 0,
        {

            AudioSource audioSource = ObjectsHandler.instance.player.GetComponent<AudioSource>(); // get the player's audio source

            audioSource.clip = ObjectsHandler.instance.player.GetComponent<PlayerInteract1>().winClip; // then get the player's plaeyrInteract script and get the winclip from there, assigning it to the audio source.
            audioSource.Play(); // then play the sound when win.

            remainingTimeLeft = 4; // set the remaining time to start at 4
            monsterRemainingText.text = "You'll be transported back in 4 seconds"; // notify player that they have 4 seconds before being transported back.
            StartCoroutine(UpdateTimeRemaining()); // start the count down.

            PlayerEntity.player.wavesCompleted++; // increment the number of waves the player has completed.
            ObjectsHandler.instance.statsUI.GetComponent<StatsUpdater>().IncreaseWavesComplicated(); // update the waves completed visual in stats UI


        }
    }

    public int GetRemainingMonsters() // get the remaining monsters
    {
        return remainingMonsters; // return the remainingMonsters int.
    }
}
