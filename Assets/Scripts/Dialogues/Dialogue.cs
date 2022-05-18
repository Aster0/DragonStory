using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public Text dialogueText, continueText; // get the instance of both dialogueText and continueText.


    public List<string> dialogues { get; set; } // a List that stores string variable with a getter and setter




    private bool end; // store a boolean for end, to see if the dialogue has ended.

    public bool start { get; set; } // store a boolean for start, to see if the dialogue should be started or not. if it's true, it should.




    private int dialogueCount = 0, letterCount = 1; // start dialogueoCount at 0 and letterCount at 1. It'll slowly increment later in t he dialogue.

    private StringBuilder stringBuilder; // a stringBuilder with no instance.


    public IEnumerator enumerator { get; set; } // an Ienumerator variable with a getter and setter. 

    public float dialogueSpeed, fastDialogueSpeed; // initializes a float of dialogueSpeed and fastDialogueSpeed variable so we can know how fast the dialogue should go.
    private float initialDialogueSpeed; // to save the initial dialogue speed so when we fasten it, we still have the initial.

    private Color color; // to save the clickToContinue color.


    private int formatCount = 0; // formatting in the dialogue starts with <b, so before formatCount reaches 2, it means it can ignore <b in the dialogue and not show it.

    private bool formatting; // if there's formatting.

    private string formatMessageStart = "", formatMessageEnd = ""; 

    private List<String> formatString; // a list to save the format string.

    public List<String> test; 

    private Button button; // so we can assign the button instance later to add a listener.

    private Animator animator; // we can assign the player's animator later so we can make the player "sleep"
     


    private void Start()
    {

        animator = ObjectsHandler.instance.player.GetComponent<Animator>(); // get the player's animator instance from the object handler singleton.


        animator.SetBool("Lay Down", true); // set the player to play a Lay Down animation.

        formatString = new List<String>(); // initializes an empty list into formatString.
        color =  new Color(255, 255, 255); // get the white color and initializes into the color variable.




        initialDialogueSpeed = dialogueSpeed; // save the initialDialogue speed for later.



        SetDialogue(test); // set the dialogue to the test list, whatever is on the test list will be shown in the dialogue.


    }



    public void OnPointerDown(PointerEventData eventData) // checks when the player is clicking on the UI
    {


        dialogueSpeed = fastDialogueSpeed * (Time.deltaTime * 0.05f); // assign a new dialogue speed, essentially making it faster with time.Delta so it's smoother.



        if(end) // if it's the end of the dialogue.
        {
        
            OnDialogueEnd(); // call the onDialogueEnd function. 
            animator.SetBool("Lay Down", false); // off the Lay Down animation of the player.

            return; // return so we don't go down any further in this function.
        }


        if(!start) // if it's not started, means it's in a dialogue currently but shouldn't start a new one - this should be at the end, when 
            // the player click to say that they are ready for the next dialogue, that's why it's under pointer down.
        {
            dialogueSpeed = initialDialogueSpeed; // we should put the dialogue speed back to normal.
            start = true; // and say we can start the next dialogue. 
        }
       
     
    }

    public void OnDialogueEnd() // for calling when the dialogue should end.
    {
       
        gameObject.SetActive(false); // set this game object to false, essentially turning off the dialogue ui. 
        end = false; // reset the end boolean, make it false.

        PlayerEntity.player.inUI = false; // player is no longer in the dialogue UI, so we can set it in the player entity singleton.
        // basically this is so we can ref it from other scripts to say the player can't open other UIs or attack while in a UI already.
   
    

        dialogueText.text = ""; // reset the dialogue text to empty. 

        color.a = 0; // alpha of the color to 0

        continueText.color = color; // assign the clickToContinue text to have a 0 alpha.
    }

    public void OnPointerUp(PointerEventData eventData) // when the click is released by the player,
    { 
        dialogueSpeed = initialDialogueSpeed; // set the dialogue speed back to normal.
    }

    public bool SetDialogue(List<string> dialogues) // for to set when the dialogue should appear. 
    {
        stringBuilder = new StringBuilder(); // new instance of StringBuilder assigned to stringBuilder var.

        dialogueCount = 0; // reset the dialogueCount to 0
        letterCount = 1; // reset the letterCount to 1

        if (dialogues.Count == 0) // if there's np entries in the list, 
        {
            return false; // we shouldn't continue any ffurther.
        }

        this.dialogues = dialogues; // if there are entries in the list, we can continue and save the parameter's dialogues into our instance
        // dialogue variable.




  
        start = true; // set start is true so the dialogue rolls.
        PlayerEntity.player.inUI = true; // set the player entity singleton inUI boolean to true, so that we can know we're in a dialogue.

        enumerator = null; // reset the enumerator into null.

        this.gameObject.SetActive(true); // show the dialogue UI.

        return true; // return true for success.



    }

    private void FixedUpdate() // FixedUpdate because Update() might take an unreliable time so the text loading might be reliable too. 
    {
        if (start) // if it's ready to start
        {
            if (enumerator == null) // and enumerator is null
            {

         

                enumerator = BuildDialogue(); // we can set a new enumerator instance
                StartCoroutine(enumerator); // and start coroutine it, async.
            }
        }


 
    }




    private IEnumerator BuildDialogue() // responsible for how the dialogue builds, the animation of the letter coming out one by one.
    {
        yield return new WaitForSeconds(dialogueSpeed); // wait for dialogue speed seconds before we continue with the next letter in the dialogue.





    


        try // try this block of code first, because in the later
        {

         
            string currentDialogue = dialogues[dialogueCount]; // get the current dialogue sentence and save it into a string variable.


        

            if (letterCount == currentDialogue.Length + 1) // end of first dialogue - last letter of the dialogue sentence.
            {
                
                stringBuilder.Clear(); // clear the string builder because dialogue has ended 

                start = false; // put start to false so a new coroutine wont be run again (essentially pausing the dialogue until the player clicks)

                letterCount = 1; // reset the letterCount back to 1.


                dialogueCount++; // + 1 to the dialogue count so we can go to the next dialogue


                color.a = 0.6f; // set 0.6 alpha to the color intsance
                continueText.color = color; // assign clickToContinue text to have 0.6 alpha so it shows that we can continue.


                currentDialogue = dialogues[dialogueCount]; // update the current dialogue sentence into the string var.



            }
            else // not last letter
            {
                color.a = 0; // click to continue needs to disappear again because it's still not finished with the current dialogue.

                continueText.color = color;
            }



            if(start) // if start is true,
            {

            



                
                String appendLetter = ""; // set appendLetter to an empty string.

                appendLetter = currentDialogue[letterCount - 1].ToString(); // we substring the currentDialogue taking the letterCount - 1's index.
                // and save it to appendLetter. This way, we get the next letter we need to append to the dialogue. so the animation flows.

                try // try this block of code, because at how formatting works for my dialogue system is <b test b> says test should be bolded.
                    // however, the > at the end might not be caught because of how I subString the compare variable down below as 2 length.
                    // so I catch the error and entirely ignore > so it doesn't appear in the dialogue and doesn't have an error too.
                {
                 





                    String compare = currentDialogue.Substring(letterCount - 1, 2); // get the current letter and the letter in front of it too.
                    // so if it sees <b, it'll know to format in the if clause below.
                 
                    

                    if (compare.Contains("<c") || compare.Contains("<b")) // see if it's a formatting string
                    {
                        formatting = true; // set format to true because we're gonna format. 
                    
                        formatCount++; // increment formatCount.
                        formatString.Add(compare); // add the string into the formatString list so if we have other formats that we need to do, we can 
                        // know too later. like <c to color and then <b to bold too, so we can do both coloring and bolding.


                    }
                    else if (compare.Contains("c>") || compare.Contains("b>")) // if it contains the closing format, we should stop formatting.
                    {
                        formatting = false; // so set format to false.
                
                        formatCount++; // and increment format count.
                        

                    }







                    if (formatCount > 0) // if format count is more than 0,
                    {

                        formatMessageStart = ""; // we should empty the formatMessageStart because we shouldn't format <b or b> because formatCount only increments when
                        // we find that.
                        formatMessageEnd = ""; // same logic for this
                         
                   

                        appendLetter = "";
                        
                        if(formatCount == 2)
                            formatCount = 0;
                        else
                            formatCount++;



                    }
                    else // so if it's after <b or before b>, we should start formatting. in the last example <b test b>, each letter of test will be added formatting.
                    {
                        if (formatting) // if formatting is true
                        {
                           

                            if (formatString.Contains("<c")) // if it contains the color format in our list
                            {

                                if(!formatMessageStart.Contains("<color")) // and doesn't already contain the color tags in the start format message,
                                    formatMessageStart += "<color=#800080>"; // we'll add the starting tag for the color with hex value 800080
                                 
                                if (!formatMessageEnd.Contains("</color>")) // and doesn't already contain the color tags in the end format message,
                                    formatMessageEnd += "</color>"; // we'll add the closing tag for the color with hex value 800080

                            }

                            // both are if so it checks for both formats!

                            if (formatString.Contains("<b"))  // if it contains the bold format in our list
                            {
                                if (!formatMessageStart.Contains("<b>")) // and doesn't already contain the bold tags in the start format message,
                                    formatMessageStart += "<b>"; // we'll add the starting tag for bold in the letter.

                                if (!formatMessageEnd.Contains("</b>"))  // and doesn't already contain the bold tags in the end format message,
                                    formatMessageEnd = "</b>" + formatMessageEnd; // we'll add the starting tag for bold in the letter. + the color  closing tag if it has.


                            }

                        
                        }
                        else // if formatting is false
                        {

                            formatMessageStart = ""; // no format start tag
                            formatMessageEnd = ""; // no format end tag.

           

                            formatString = new List<String>(); // reset the formatString list into a new List instance.

                        }
                  
                    }





                }
                catch(ArgumentOutOfRangeException) // catch the error that we explained just now
                {
                    String compare = currentDialogue.Substring(letterCount - 1, 1); // get 1 length instead of 2 because now we know it's the last letter of the dialogue

                    if(compare.Equals(">")) // if it's a closing tag, we should ignore it and not put in the dialogue
                    {
                        appendLetter = ""; // so appendLetter will be set to empty so we dont append anything
                    }
                }





                stringBuilder.Append(formatMessageStart + appendLetter + formatMessageEnd); // append to the stringBuilder so we keep the previously
                // appended letter too, so we can slowly form a full sentences with lettres. formatMessageStart and end depends on whether there's 
                // formatting tags, if there's not, it's empty. 

                letterCount++; // increment the letterCount so we can go to the next letter in the sentence.




                dialogueText.text = stringBuilder.ToString(); // update the dialogue Text using the stringBuilder.


                enumerator = null; // reset the enumerator. 
            }
          
        }

        catch (Exception e) { // catch an error because the dialogueCount might ++ into one bigger than the total index, so we can catch and ignore it.

            if(e is ArgumentOutOfRangeException || e is IndexOutOfRangeException) // out of range, it's bigger than the total index ^^^^
            {
                
                // reset everything and because it's the end of the dialogue. 
                start = false; 
                end = true;
           

            }
  
            
   

         
    

        
        }
        finally
        {
            enumerator = null;
         
        }
        

    }

    

}
