using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotManager : MonoBehaviour
{

    public Item item;
    public TextMeshProUGUI itemName, statIndicator;
    public GameObject panel;


    public bool equipment;




    public Image itemSprite;
    // Start is called before the first frame update
    void Start()
    {
        OnSlotChange(); // update the slot on start so it shows all empty slots 

    }


    private void SetImageAlpha(float alpha)  // function to quickly set the Image's alpha.
    {

        Image image = itemSprite.GetComponent<Image>(); // get the image component of the itemSprite

        Color tempColor = image.color; // saves the image's color into a temp variable so we can manipulate the alpha

        tempColor.a = alpha; // set the alpha with what's specified in the function parameter

        image.color = tempColor; // update the image's color to the edited tempColor alpha.

    }
    public void OnSlotChange() // function to update the slots' visual
    {
        if (item != null) // if the item is not null, means there's an item
        {
            itemSprite.sprite = item.sprite; // so update the item's sprite 
            itemName.text = item.name;  // update the itemName's text into the item's name

            SetImageAlpha(1f); // set the alpha of the image to 1f, means visible

            if(equipment) // if it's an equipment
            {
                statIndicator.text = "+" + item.adder + " " + item.stats; // update the stat indicator that shows beside the slot.
            }

        }
        else // if item is null, means no item resides in the current slot.
        {
            itemSprite.sprite = null; // set sprite to null
            itemName.text = "";  // item name to nothing
            SetImageAlpha(0f); // set alpha to 0 so it's not visible


            if (equipment) // if it's an equipment
            {
                statIndicator.text = ""; // update the stat indicator that shows beside the slot to nothing because no item in this slot.
            }
        }
    }



 

}
