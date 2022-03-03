using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] private GameObject[] inventoryUIItems;
    [SerializeField] private List<GameObject> inventory = new List<GameObject>();
    private GameObject selectedItemObject;
    private int selectedItem;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateItemSelection(1);

        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            UpdateItemSelection(0);
        }

    }
    public List<GameObject> GetInventory()
    {
        return inventory;
    }

    public GameObject GetSelectedItem()
    {
        return selectedItemObject;
    }

    // checks if the item passed already exists in the players inventory,
    // returns true if it already exists,
    // returns false if it doesnt already exist,
    private bool InventoryHasItem(GameObject Checkitem)
    {
        bool hasItem = true;
        if (inventory.Count > 0) // if inventory is not empty,
        {
            inventory.ForEach(delegate (GameObject item) // foreach item in the inventory
            {
                if (item.tag == Checkitem.tag) // if item already exists in inventory break out of the foreach,
                {
                    return;
                }
                else
                {
                    hasItem = false; // item not already in inventory,
                }
            });
        }
        else
        {
            hasItem = false; // empty inventory therefore not in inventory already,
        }
        return hasItem;
    }
    //called by PickupController, trys to add item to inventory, returns success boolean,
    public bool TryAddToInventory(GameObject itemToAdd)
    {
        if (InventoryHasItem(itemToAdd)) // if item already in inventory,
        {
            //add ammo, process duplicate pickup,
            return false;
        }
        else // adds item to inventory and makes call to update inventory UI,
        {
            inventory.Add(itemToAdd);
            UpdateInventoryUI(itemToAdd);
            return true;
        }
    }

    private void UpdateInventoryUI(GameObject item)
    {
        switch (item.name) // checks item tag against available Inventory UI Icons, and sets active if equal;
        {
            case "basic_gun":
                inventoryUIItems[0].SetActive(true);
                return;
            default:
                Debug.Log("unrecognised pickup");
                break;
        }
        foreach (GameObject element in inventoryUIItems)
        {
            element.GetComponent<SpriteRenderer>().color = Color.white;
        }

        inventoryUIItems[Mathf.Max(selectedItem - 1, 0)].GetComponent<SpriteRenderer>().color = Color.green;


    }
    private void UpdateItemSelection(int selection)
    {
        if (inventory.Count == 0)
        {
            return;
        }
        else if (selection >= inventory.Count -1)
        {
            animator.SetInteger("Selected", selection);

            selectedItemObject = inventory[Mathf.Max(selection - 1,0) ];
            selectedItem = selection;

            Debug.Log(selectedItemObject.tag);
            // update ui to highligh selected item,
            // update animator to display player with item sprites,
        }
    }

}
