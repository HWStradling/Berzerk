using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{
    public static bool IsSaved { get; private set; } = true;

    public static string ProfileName { get; private set; }

    public static PlayerData PlayerData { get; private set; }

    public static bool SaveGame(PlayerData saveData)
    {

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(GetSavePath(), FileMode.Create))
        {
            try
            {
                formatter.Serialize(stream, saveData);
            }
            catch (Exception)
            {

                return false;
            }
        }
        
        return true;
        
    }
    public static bool LoadGame()
    {
        // if changed player, clear acheivments queue,
        if (!CheckSaveGame())
        {
            return false;
        }
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(GetSavePath(), FileMode.Open))
        {
            try
            {
                PlayerData = formatter.Deserialize(stream) as PlayerData;
            }
            catch (Exception)
            {

                return false;
            }
        }
        return true;
    }
    private static bool CheckSaveGame()
    {
        return File.Exists(GetSavePath());
    }
    private static string GetSavePath()
    {
        ProfileName = PlayerPrefs.GetString("profile", "Profile 1");
        return Path.Combine(Application.persistentDataPath, ProfileName + ".sav");
    }
    
}
