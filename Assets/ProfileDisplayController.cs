using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileDisplayController : MonoBehaviour
{
    [SerializeField] Text profileName, maxLevel, basicGun, rifle, achievmentsUnlocked;


    private void FixedUpdate()
    {
        DisplayProfile();
    }
    private void DisplayProfile()
    {
        profileName.text = PlayerPrefs.GetString("profile", "") + ":";
        maxLevel.text = "Max Level: " + (SaveSystem.PlayerData.MaxLevel -1);
        bool[] achievmentsArray = SaveSystem.PlayerData.Achievments;
        if (achievmentsArray[0] == true)
        {
            basicGun.text = "Handgun";
        }
        else
        {
            basicGun.text = "";
        }
        if (achievmentsArray[3] == true)
        {
            rifle.text = "Rifle";
        }else
        {
            rifle.text = "";
        }
        int count = 0;
        foreach (var achievment in achievmentsArray)
        {
            if (achievment)
            {
                count++;
            }
        }
        achievmentsUnlocked.text = "Achievments Unlocked: " + count;
    }
}
