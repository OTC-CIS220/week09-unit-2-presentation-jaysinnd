using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //variables for Item Slots
    public bool hoverItem;
    public bool empty;
    public GameObject item;
    public Texture itemIcon;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        hoverItem = false;
    }

    void Update()
    {
        if (item) //checks if item already exists in the slot in the inventory holder
        {
            empty = false;
            itemIcon = item.GetComponent<Item>().icon;
            this.GetComponent<RawImage>().texture = itemIcon; //fills inventory with image from the item we 'hit'
        }
        else
        {
            empty = true;
            itemIcon = null; 
            this.GetComponent<RawImage>().texture = null; //removes the item icon from the inventory AFTER using (clicking) the item.
        }
            

        
    }
    public void OnPointerEnter(PointerEventData eventData) //currently not working but supposed to highlight hovered over item slots in inventory
    {
        hoverItem = true;

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        hoverItem = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(item)
        {
            Item thisItem = item.GetComponent<Item>();  //checks for item type
            if (thisItem.type == "Water")
            {
                player.GetComponent<Player>().Drink(thisItem.decreaseValue);  //if the item type is water, this is called.
                Destroy(item); //uses the drink item if you click on it in the inventory
            }
        }
    }
}
