using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject[] inventoryUIItems;
    // Start is called before the first frame update

    public void EnableUIElement(GameObject item)
    {
        switch (item.name) // checks item tag against available  UI Icons, and sets active if equal;
        {
            case "basic_gun":
                inventoryUIItems[0].SetActive(true);
                return;
            default:
                Debug.Log("unrecognised pickup");
                break;
        }

    }
    public void ChangeSelectedItem(GameObject item)
    {
        if (item != null)
        {
            switch (item.name) // checks item tag against available  UI Icons, and sets active if equal;
            {
                case "basic_gun":
                    inventoryUIItems[0].SetActive(true);
                    inventoryUIItems[0].GetComponent<SpriteRenderer>().color = Color.green;
                    return;
                default:
                    Debug.Log("unrecognised pickup");
                    break;
            }
        }
        else
        {
            foreach (GameObject element in inventoryUIItems)
            {
                element.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        

    }
}