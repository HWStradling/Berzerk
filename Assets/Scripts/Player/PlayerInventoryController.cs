using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] private List<GameObject> inventory = new List<GameObject>();
    [SerializeField] UnityEvent<GameObject> OnChangeSelectedItem;
    [SerializeField] UnityEvent<GameObject> OnAddItemToInventory;
    private GameObject selectedItemObject;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TryUpdateItemSelection(1);

        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            TryUpdateItemSelection(0);
        }

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
                if (Checkitem.CompareTag(item.tag)) // if item already exists in inventory break out of the foreach,
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
            OnAddItemToInventory?.Invoke(itemToAdd); // event for add item to inventory
            return true;
        }
    }
    private void TryUpdateItemSelection(int selection)
    {
        if (inventory.Count == 0)
        {
            return;
        }
        else if (selection == 0)
        {
            selectedItemObject = null;
            OnChangeSelectedItem?.Invoke(null);
            animator.SetInteger("Selected", selection);
        }
        else if (selection >= inventory.Count -1)
        {
            animator.SetInteger("Selected", selection);
            selectedItemObject = inventory[Mathf.Max(selection - 1,0) ];

            OnChangeSelectedItem?.Invoke(selectedItemObject); //event for item selection changed,
            Debug.Log(selectedItemObject.tag);
        }
    }

}
