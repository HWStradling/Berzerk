using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        Debug.Log("collision with: " + collisionObject.tag);
        if (collisionObject.tag == "Player")
        {
            if (collisionObject.GetComponent<PlayerInventoryController>().TryAddToInventory(gameObject))
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
