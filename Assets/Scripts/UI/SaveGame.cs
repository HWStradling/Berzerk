using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
  public void OnSaveGame()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            return;
        }
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        string[] inventory = player.GetComponent<PlayerInventoryController>().inventory.ToArray();
        int maxLevel = player.GetComponent<PlayerMovementController>().maxLevel;
        bool[] achievments = SingletonAchievmentsController.acheivmentsArray;
        PlayerData p = new PlayerData(inventory, maxLevel, achievments);

        Debug.Log("Saved: " + SaveSystem.SaveGame(p));
    }
}