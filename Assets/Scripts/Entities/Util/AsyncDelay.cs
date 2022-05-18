using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsyncDelay : MonoBehaviour
{
    public static IEnumerator AsyncDelayTask(float time, System.Action task) // async task so we can run on a different thread on a delay
    {


        yield return new WaitForSeconds(time); // yield wait for seconds before continuing with the bottom
        task(); // when u run StartCoroutine, run this task() method as second parameter.

    }
}
