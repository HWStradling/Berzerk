using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject[] inventoryUIItems;
    [SerializeField] UnityEvent<int> newItemAcheivment;
    // Start is called before the first frame update

    public void EnableUIElement(string item)
    {
        switch (item) // checks item tag against available  UI Icons, and sets active if equal;
        {
            case "basic_gun":
                newItemAcheivment?.Invoke(0);
                inventoryUIItems[0].SetActive(true);
                return;
            case "rifle":
                newItemAcheivment?.Invoke(3);
                inventoryUIItems[1].SetActive(true);
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
                DeselectItems();
                inventoryUIItems[0].SetActive(true);
                inventoryUIItems[0].GetComponent<SpriteRenderer>().color = Color.green;
                return;
            case "rifle":
                DeselectItems();
                inventoryUIItems[1].SetActive(true);
                inventoryUIItems[1].GetComponent<SpriteRenderer>().color = Color.green;
                return;
            default:
                DeselectItems();
                break;
        }
    }
    private void DeselectItems()
    {
        foreach (GameObject element in inventoryUIItems)
        {
            element.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}