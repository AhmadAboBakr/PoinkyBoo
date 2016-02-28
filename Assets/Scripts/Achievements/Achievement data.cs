using UnityEngine;
using System.Collections;

public class Achievementdata {
    //public int Value;
    //public string ID;
    public bool Unlocked;
    public string Description;
    public string Name;
    public Achievementdata (bool unlocked,string description,string name)
    {
        Unlocked = unlocked;
        Name = name;
        Description = description;
    }
    public Achievementdata( string description, string name)
    {
        Unlocked = false;
        Name = name;
        Description = description;
    }
}
