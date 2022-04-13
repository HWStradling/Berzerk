using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] private List<string> inventory = new List<string>();
    [SerializeField] UnityEvent<string> OnChangeSelectedItem; // TODO change relevent methods
    [SerializeField] UnityEvent<string> OnAddItemToInventory;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TryUpdateItemSelection("basic_gun");

        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            TryUpdateItemSelection("empty");
        }

    }

    // checks if the item passed already exists in the players inventory,
    // returns true if it already exists,
    // returns false if it doesnt already exist,
    private bool InventoryHasItem(string Checkitem)
    {
        bool hasItem = true;
        if (inventory.Count > 0) // if inventory is not empty,
        {

            inventory.ForEach(delegate (string item) // foreach item in the inventory
            {
                if (item == Checkitem) // if item already exists in inventory break out of the foreach,
                    return;
                else
                    hasItem = false; // item not already in inventory,
            });
        }
        else
        {
            hasItem = false; // empty inventory therefore not in inventory already,
        }
        return hasItem;
    }
    //called by PickupController, trys to add item to inventory, returns success boolean,
    public bool TryAddToInventory(string itemToAdd)
    {
        if (InventoryHasItem(itemToAdd)) // if item already in inventory,
        {
            //add ammo, process duplicate pickup,
            return false;
        }
        else // adds item to inventory and makes call to update inventory UI,
        {
            inventory.Add(itemToAdd);
            OnAddItemToInventory?.Invoke(itemToAdd); // event for add item to inventory
            return true;
        }
    }
    private void TryUpdateItemSelection(string selection)
    {
        int selectedInt = 0;
        if (inventory.Count == 0)
        {
            return;
        }
        else if (selection == "empty")
        {
            OnChangeSelectedItem?.Invoke("empty");
            animator.SetInteger("Selected", 0);
        }
        else if (inventory.Contains(selection))
        {
            switch (selection)
            {
                case "basic_gun":
                    selectedInt = 1;
                    break;
                case "rifle":
                    selectedInt = 2;
                    break;
            }
            animator.SetInteger("Selected", selectedInt);

            OnChangeSelectedItem?.Invoke(selection); //event for item selection changed,

        }
    }

}
