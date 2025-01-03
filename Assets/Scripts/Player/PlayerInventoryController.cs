using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryController : MonoBehaviour
{
    public List<string> inventory = new List<string>();
    [SerializeField] UnityEvent<string> OnChangeSelectedItem; // TODO change relevent methods
    [SerializeField] UnityEvent<string> OnAddItemToInventory;
    private Animator animator;
    private void Start()
    {
        string[] loadedInventory;
        try
        {
            loadedInventory = SaveSystem.PlayerData.Inventory;
        }
        catch { 
            loadedInventory = new string[0];
        }
        
      
        if (loadedInventory != null)
        {
            foreach (var item in loadedInventory)
            {
                TryAddToInventory(item);
            }
        }
        
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
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TryUpdateItemSelection("rifle");

        }

    }

    // checks if the item passed already exists in the players inventory,
    // returns true if it already exists,
    // returns false if it doesnt already exist,
    private bool InventoryHasItem(string Checkitem)
    {
        if (inventory.Count > 0) // if inventory is not empty,
        {
            if (inventory.Contains(Checkitem))
            {
                return true;
            } else
            {
                return false;
            }
        }
        return false;
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
