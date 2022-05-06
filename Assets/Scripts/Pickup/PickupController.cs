using UnityEngine;

public class PickupController : MonoBehaviour
{
    private bool invChecked = false;
    private void FixedUpdate()
    {
        if (!invChecked)
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length > 0)
            {
                if (gameObject.name == "basic_gun" || gameObject.name == "rifle")
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player.GetComponent<PlayerInventoryController>().inventory.Contains(gameObject.name))
                    {
                        gameObject.SetActive(false);
                    }
                }
                
            }
            invChecked = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.CompareTag("Player"))
        {
            if (collisionObject.GetComponent<PlayerInventoryController>().TryAddToInventory(gameObject.name))
            {
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("player already has item");
            }

        }
    }
}
