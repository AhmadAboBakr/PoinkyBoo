using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class AchievementsHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void AchievementUnlocked (string avhievementID)
    {
        Social.ReportProgress(avhievementID, 100.0f, (bool success) =>
        {
            // handle success or failure
        });
    }
}
