using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject[] inventoryUIItems;
    [SerializeField] UnityEvent<int> newItemAcheivment;
    // Start is called before the first frame update

    public void EnableUIElement(string item)
    {
        Debug.Log(item);
        switch (item) // checks item tag against available  UI Icons, and sets active if equal;
        {
            case "basic_gun":
                newItemAcheivment?.Invoke(0);
                inventoryUIItems[0].SetActive(true);
                return;
            default:
                Debug.Log("unrecognised pickup");
                break;
        }

    }
    public void ChangeSelectedItem(string item)
    {
        switch (item) // checks item tag against available  UI Icons, and sets active if equal;
        {
            case "basic_gun":
                inventoryUIItems[0].SetActive(true);
                inventoryUIItems[0].GetComponent<SpriteRenderer>().color = Color.green;
                return;
            default:
                foreach (GameObject element in inventoryUIItems)
                {
                    element.GetComponent<SpriteRenderer>().color = Color.white;
                }
                break;
        }
    }
}