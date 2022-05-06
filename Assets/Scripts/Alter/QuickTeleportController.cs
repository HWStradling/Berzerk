using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTeleportController : MonoBehaviour
{
    [SerializeField] private GameObject[] alters;

    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length > 0)
        {
            int maxLevel = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerMovementController>().maxLevel;
            foreach (var alter in alters)
            {
                if (alter.GetComponent<PropsAltar>().destinationBuildIndex <= maxLevel)
                {
                    alter.SetActive(true);
                }
            }
        }
        
    }
}
