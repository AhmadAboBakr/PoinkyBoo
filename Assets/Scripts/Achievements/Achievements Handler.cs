using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;

public class AchievementsHandler : MonoBehaviour {
    public static AchievementsHandler instance;
    public Dictionary<int, Achievementdata> Achievements;
    void Awake()
    {
        if (!AchievementsHandler.instance)
            AchievementsHandler.instance = this;
        GameManager.Move += Move;
        GameManager.clear += Clear;
    }
	public void Move()
    {
        //if()
        //{

        //}
        //else if()
        //{

        //}
    }
    public void Clear ()
    {

    }
    void AchievementUnlocked (string avhievementID,double value)
    {
        Social.ReportProgress(avhievementID, value, (bool success) =>
        {
            // handle success or failure
        });
    }
    void Start()
    {
        Achievements=new Dictionary<int,Achievementdata>();
        Achievements.Add(10, new  Achievementdata("id1", false));
        Achievements.Add(20, new Achievementdata("id2", false));
        Achievements.Add(50, new Achievementdata("id3", false));
        Achievements.Add(100, new Achievementdata("id4", false));
    }
}
