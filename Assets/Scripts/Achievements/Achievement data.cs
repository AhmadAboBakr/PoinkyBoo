using UnityEngine;
using System.Collections;

public class Achievementdata {
    //public int Value;
    public string ID;
    public bool Unlocked;
	
    public Achievementdata (string id,bool unlocked)
    {
        ID = id;
        Unlocked = unlocked;
    }
}
