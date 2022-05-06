using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string[] Inventory { get; set; }
    public int MaxLevel { get; set; }
    public bool[] Achievments { get; set; }

    public PlayerData(string[] inventory, int maxLevel, bool[] achievments)
    {
        this.Inventory = inventory;
        this.MaxLevel = maxLevel;
        this.Achievments = achievments;
    }
}
