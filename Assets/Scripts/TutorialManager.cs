using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerEntity.player.level > 1) // if player is set above 1.
        {
            for (int i = 0; i < PlayerEntity.player.level; i++) // so the player can get extra stat point if i were to set it to higher level for debugging purposes
            {
                ObjectsHandler.instance.statsUI.GetComponent<StatsUpdater>().AddStatPoints(); // update the player's stat points each iteration.
            }

        }

    }

}
