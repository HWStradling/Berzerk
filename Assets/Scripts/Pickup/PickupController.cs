using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        Debug.Log("collision with: " + collisionObject.tag);
        if (collisionObject.CompareTag("Player"))
        {
            if (collisionObject.GetComponent<PlayerInventoryController>().TryAddToInventory(gameObject.name))
            {
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("player already has item");
                // player already has item/inventory add failed, process here,
            }
            
        }
    }
}
