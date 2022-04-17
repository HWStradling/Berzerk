using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
     public string[] inventory;
   public PlayerData(GameObject player)
    {
        List<string>  listInventory = player.GetComponent<PlayerInventoryController>().inventory;
        // levels unlocked
        
        inventory = listInventory.ToArray();


    }
}
