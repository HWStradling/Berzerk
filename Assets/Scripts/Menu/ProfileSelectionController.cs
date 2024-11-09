using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSelectionController : MonoBehaviour
{
    [SerializeField] Dropdown profileSelector;
    private void Awake()
    {
        switch (PlayerPrefs.GetString("profile", "Profile 1"))
        {
            case "Profile 1":
                profileSelector.value = 0;
                break;
            case "Profile 2":
                profileSelector.value = 1;
                break;
            case "Profile 3":
                profileSelector.value = 2;
                break;
            default:
                HandleSelectionChange(0);
                Debug.Log("error playerprefs profile selection defaulting");
                break;
        }
    }
    public void HandleSelectionChange(int selection)
    {
        switch (selection)
        {
            case 0:
                PlayerPrefs.SetString("profile", "Profile 1");
                SaveSystem.LoadGame();
                break;
            case 1:
                PlayerPrefs.SetString("profile", "Profile 2");
                SaveSystem.LoadGame();
                break;
            case 2:
                PlayerPrefs.SetString("profile", "Profile 3");
                SaveSystem.LoadGame();
                break;
            default:
                break;
        }
    }
}
