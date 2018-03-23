using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //variables for the Inventory system
    public GameObject inventory;
    public GameObject slotHolder;
    public GameObject itemManager;
    private int slots;
    private Transform[] slot;
    private GameObject itemPickedup;
    private bool inventoryEnabled;
    private bool itemAdded;


    public void Start()
    {
        // Detecting inventory slots
        
        slots = slotHolder.transform.childCount;
        slot = new Transform[slots];
        DetectInventorySlots();
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
        }
        if (inventoryEnabled)
            inventory.GetComponent<Canvas>().enabled = true;
        else
            inventory.GetComponent<Canvas>().enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            itemPickedup = other.gameObject;
            AddItem(itemPickedup);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            itemAdded = false;
        }
    }

    public void AddItem(GameObject item)
    {
        for (int i = 0; i < slots; i++)
        {
            if(slot[i].GetComponent<Slot>().empty && itemAdded == false)
            {
                slot[i].GetComponent<Slot>().item = itemPickedup;
                slot[i].GetComponent<Slot>().itemIcon = itemPickedup.GetComponent<Item>().icon;

                item.transform.parent = itemManager.transform;  //adds item to itemManager
                item.transform.position = itemManager.transform.position;
                if(item.GetComponent<MeshRenderer>())
                {
                    item.GetComponent<MeshRenderer>().enabled = false;
                }
                Destroy(item.GetComponent<Rigidbody>());
                

                itemAdded = true;
            }
        }
    }

    public void DetectInventorySlots()
    {
        
        for (int i = 0; i < slots; i++) //detects each and every slot
        {
            slot[i] = slotHolder.transform.GetChild(i);
        }
    }
	
}
