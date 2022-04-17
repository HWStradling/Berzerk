using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem
{
    public static bool IsSaved { get; private set; } = true;

    public static PlayerData playerData { get; private set; }

    public static void SavePlayer(GameObject player)
    {
        // check if player exists, if not create player save,
        // if exists save,
        //IsSaved = true;
    }
    public static void LoadPlayer(string playerName)
    {
        //load player construct PlayerData Object, save in playerData,
    }

}
