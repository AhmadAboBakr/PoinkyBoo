using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;

public class AchievementsHandler : MonoBehaviour {
    public static AchievementsHandler instance;
    void Awake()
    {
        if (!AchievementsHandler.instance)
            AchievementsHandler.instance = this;
        GameManager.Move += Move;
    }
	public void Move()
    {
        switch (HUDManager.instance.score)
        {
            case 10:
                Social.ReportProgress("Score10", 10, (bool success) =>
                {
                    // handle success or failure
                });
                break;
            case 50:
                Social.ReportProgress("Score50", 50, (bool success) =>
                {
                    // handle success or failure
                });
                break;
            case 100:
                Social.ReportProgress("Score100", 100, (bool success) =>
                {
                    // handle success or failure
                });
                break;
            case 250:
                Social.ReportProgress("Score100", 100, (bool success) =>
                {
                    // handle success or failure
                });
                break;
            case 500:
                Social.ReportProgress("Score100", 100, (bool success) =>
                {
                    // handle success or failure
                });
                break;
            default:
                break;
        }
    }
    public void ReportMagnetAchivement(int numberOfPowerUPS)
    {
        switch (numberOfPowerUPS)
        {
            case 2:
                break;
            case 4:
                break;
            case 8:
                break;
            case 16:
                break;
            default:
                break;
        }
    }
    public void ReportShieldAchivement(int numberOfPowerUPS)
    {
        switch (numberOfPowerUPS)
        {
            case 2:
                break;
            case 4:
                break;
            case 8:
                break;
            case 16:
                break;
            default:
                break;
        }
    }
    public void ReportCollectingCoinsInOneGame(int numberOfCoins)
    {
        switch (numberOfCoins)
        {
            case 50:
                break;
            case 100:
                break;
            case 200:
                break;
            case 400:
                break;
            default:
                break;
        }
    }
    public void ReportCollectingCoinsWithMagnetInOneGame(int numberOfCoins)
    {
        switch (numberOfCoins)
        {
            case 10:
                break;
            case 50:
                break;
            case 100:
                break;
            case 200:
                break;
            default:
                break;
        }
    }
    public void ReportTotalCoins(int numberOfCoins)
    {
        PlayGamesPlatform.Instance.IncrementAchievement("ID", 5, (bool success) =>
      {
          // handle success or failure
      });
    }

}
