using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;
using UnityEngine.UI;


public class AchievementsHandler : MonoBehaviour
{
    public static AchievementsHandler instance;
    //public Text ttttt;
    public int NumberOfMagnits;
    public int NumberOfSaftyNets;
    public int NumOfTilesWithSafty;
    public int NumOfCollectablesWithMagnet;
    public int NumOfTilesWithSfatyNet;


    void Awake()
    {
        if (!AchievementsHandler.instance)
            AchievementsHandler.instance = this;
        GameManager.Move += Move;
        GameManager.clear += Clear;

        //ttttt.text += "fata7";
    }
    public void Clear()
    {
        NumberOfMagnits = 0;
        NumberOfSaftyNets = 0;
        NumOfCollectablesWithMagnet = 0;
        NumOfTilesWithSfatyNet = 0;
    }
#if UNITY_ANDROID
    
	public void Move()
    {

        switch (HUDManager.instance.score)
        {
            case 20:
                Social.ReportProgress("CgkInbf4694CEAIQCw", 100, (bool success) =>
                {
                    //reach spiral done
                });
                break;
            case 50:
                Social.ReportProgress("CgkInbf4694CEAIQDA", 100, (bool success) =>
                {
                    // Reach desert done
                });
                break;
            case 85:
                Social.ReportProgress("CgkInbf4694CEAIQDQ", 100, (bool success) =>
                {
                    // reach lap second time done
                });
                break;
            case 105:
                Social.ReportProgress("CgkInbf4694CEAIQEA", 100, (bool success) =>
                {
                    // Reach desert with water lesa done
                });
                break;
            case 500:
                Social.ReportProgress("CgkInbf4694CEAIQDg", 100, (bool success) =>
                {
                    // 500 tile
                });
                break;
            case 1000:
                Social.ReportProgress("CgkInbf4694CEAIQDw", 100, (bool success) =>
                {
                    // another 1000 tile lesa
                });
                break;
            default:
                break;
        }
    }
    public void ReportMagnetAchivement()
    {
        switch (NumberOfMagnits)
        {
            case 5:
                Social.ReportProgress("CgkInbf4694CEAIQBQ", 100, (bool success) =>
                {
                   //done magnet lover
                });
                break;
            case 15:
                Social.ReportProgress("CgkInbf4694CEAIQBg", 100, (bool success) =>
                {
                    //done center of attraction 
                });
                break;
            case 50:
                Social.ReportProgress("CgkInbf4694CEAIQEQ", 100, (bool success) =>
                {
                    //done king of magnits 
                });
                break;
            
            default:
                break;
        }
    }
    
    public void ReportCollectingCoinsInOneGame()
    {
        switch (HUDManager.instance.collectables)
        {
            case 50:
                Social.ReportProgress("CgkInbf4694CEAIQCQ", 100, (bool success) =>
                {

                });
                break;
            case 100:
                Social.ReportProgress("CgkInbf4694CEAIQFg", 100, (bool success) =>
                {
                    // handle success or failure
                });
                break;
            case 500:
                Social.ReportProgress("CgkInbf4694CEAIQCg", 100, (bool success) =>
                {
                    // handle success or failure
                });
                break;
            default:
                break;
        }
    }

    public void ReportTotalCoins(int numberOfCoins)
    {
        PlayGamesPlatform.Instance.IncrementAchievement("CgkInbf4694CEAIQEg", numberOfCoins, (bool success) =>
        {
            //number of coins 500
        });
        PlayGamesPlatform.Instance.IncrementAchievement("CgkInbf4694CEAIQEw", numberOfCoins, (bool success) =>
        {
            // 1000
        });
        PlayGamesPlatform.Instance.IncrementAchievement("CgkInbf4694CEAIQFA", numberOfCoins, (bool success) =>
        {
            // 5000
        });
        PlayGamesPlatform.Instance.IncrementAchievement("CgkInbf4694CEAIQFQ", numberOfCoins, (bool success) =>
        {
            // 5000
        });
        
    }
   
    //public void ReportJumpingOnTilesWithSaftNetInOneGame()
    //{
    //    switch (NumOfTilesWithSfatyNet)
    //    {
    //        case 10:
    //            Social.ReportProgress("CgkInbf4694CEAIQAg", 100, (bool success) =>
    //            {

    //            });
    //            break;
    //        case 50:
    //            Social.ReportProgress("CgkInbf4694CEAIQAw", 100, (bool success) =>
    //            {
    //                // handle success or failure
    //            });
    //            break;
    //        case 100:
    //            Social.ReportProgress("CgkInbf4694CEAIQBA", 100, (bool success) =>
    //            {
    //                // handle success or failure
    //            });
    //            break;
    //        case 500:
    //            Social.ReportProgress("CgkInbf4694CEAIQBQ", 100, (bool success) =>
    //            {
    //                // handle success or failure
    //            });
    //            break;
    //        case 1000:
    //            Social.ReportProgress("CgkInbf4694CEAIQBg", 100, (bool success) =>
    //            {
    //                // handle success or failure
    //            });
    //            break;
    //    }
    //}
    
    public void ReportShieldAchivement()
    {
        //get saved by saftynets 
        PlayGamesPlatform.Instance.IncrementAchievement("CgkInbf4694CEAIQBw", 1, (bool success) =>
        {
            //living on the edge 10
        });
        PlayGamesPlatform.Instance.IncrementAchievement("CgkInbf4694CEAIQCA", 1, (bool success) =>
        {
            // 1000
        });
        PlayGamesPlatform.Instance.IncrementAchievement("CgkInbf4694CEAIQFw", 1, (bool success) =>
        {
            // 5000
        });
    }
    //public void ReportCollectingCoinsWithMagnetInOneGame()
    //{

    //    switch (NumOfCollectablesWithMagnet)
    //    {
    //        case 50:
    //            Social.ReportProgress("CgkInbf4694CEAIQAw", 100, (bool success) =>
    //            {
    //                // handle success or failure
    //            });
    //            break;
    //        case 100:
    //            Social.ReportProgress("CgkInbf4694CEAIQBA", 100, (bool success) =>
    //            {
    //                // handle success or failure
    //            });
    //            break;
    //        case 500:
    //            Social.ReportProgress("CgkInbf4694CEAIQBQ", 100, (bool success) =>
    //            {
    //                // handle success or failure
    //            });
    //            break;
    //        case 1000:
    //            Social.ReportProgress("CgkInbf4694CEAIQBg", 100, (bool success) =>
    //            {
    //                // handle success or failure
    //            });
    //            break;
    //    }
    //}
#else

    public Text AchievementName;
    public GameObject Achievement;
    Achievementdata[] Achievements = {
                                         new Achievementdata("Reach the spiral rooms","I feel dizzy"),
                                         new Achievementdata("Reach the desert","I got sand in my shoes"),
                                         new Achievementdata("Loop back to the beginning","Start over"),
                                         new Achievementdata("Reach the desert with water","I believe i can swim"),
                                         new Achievementdata("Collect 5 magnets in one game","Magnet lover"),
                                         new Achievementdata("Collect 15 magnets in one game","Centre of attraction"),
                                         new Achievementdata("Collect 50 magnets in one game","King of magnets"),
                                         new Achievementdata("Collect 50 Coin in one game","Coin lover"),
                                         new Achievementdata("Collect 100 Coin in one game","Master of coin"),
                                         new Achievementdata("Collect 500 Coin in one game","Greed is good"),
                                         new Achievementdata("Get score 500","I would walk for 500 miles"),
                                         new Achievementdata("Get score 1000","And I would walk for 500 more")
                                     };

    void Start()
    {


        for (int i = 0; i < Achievements.Length; i++)
        {
            if (PlayerPrefs.HasKey("ACH" + i))
            {
                Achievements[i].Unlocked = true;
            }
            else
            {
                PlayerPrefs.SetString("ACH" + i, "false");
                Achievements[i].Unlocked = false;
            }
        }
    }
    public void ReportShieldAchivement()
    {

    }
    public void ReportTotalCoins(int numberOfCoins)
    {
    }

    public void Move()
    {

        switch (HUDManager.instance.score)
        {
            case 20:
                AchievementName.text = Achievements[0].Name;
                Achievement.GetComponent<Animator>().SetBool("unlock", true);
                StartCoroutine("StopAnimation");
                Achievements[0].Unlocked = true;
                break;
            case 50:
                AchievementName.text = Achievements[1].Name;
                Achievement.GetComponent<Animator>().SetBool("unlock", true);
                StartCoroutine("StopAnimation");
                Achievements[1].Unlocked = true;

                break;
            case 85:
                AchievementName.text = Achievements[2].Name;
                Achievement.GetComponent<Animator>().SetBool("unlock", true);
                StartCoroutine("StopAnimation");
                Achievements[2].Unlocked = true;

                break;
            case 105:
                AchievementName.text = Achievements[3].Name;
                Achievement.GetComponent<Animator>().SetBool("unlock", true);
                StartCoroutine("StopAnimation");

                Achievements[3].Unlocked = true;

                break;
            case 500:
                AchievementName.text = Achievements[10].Name;
                Achievement.GetComponent<Animator>().SetBool("unlock", true);
                StartCoroutine("StopAnimation");
                Achievements[10].Unlocked = true;

                break;
            case 1000:
                AchievementName.text = Achievements[11].Name;
                Achievement.GetComponent<Animator>().SetBool("unlock", true);
                StartCoroutine("StopAnimation");
                Achievements[11].Unlocked = true;

                break;
            default:
                break;
        }
    }
    public void ReportMagnetAchivement()
    {
        switch (NumberOfMagnits)
        {
            case 5:
                AchievementName.text = Achievements[4].Name;
                Achievement.GetComponent<Animator>().SetBool("unlock",true);
                StartCoroutine("StopAnimation");
                Achievements[4].Unlocked = true;
                break;
            case 15:
                AchievementName.text = Achievements[5].Name;
                Achievement.GetComponent<Animator>().SetBool("unlock",true);
                StartCoroutine("StopAnimation");
                 Achievements[5].Unlocked = true;
                break;
            case 50:
                AchievementName.text = Achievements[6].Name;
                Achievement.GetComponent<Animator>().SetBool("unlock",true);
                StartCoroutine("StopAnimation");
                 Achievements[6].Unlocked = true;
                break;
            default:
                break;
        }
    }

    public void ReportCollectingCoinsInOneGame()
    {
        switch (HUDManager.instance.collectables)
        {
            case 50:
                AchievementName.text = Achievements[6].Name;
                Achievement.GetComponent<Animator>().SetBool("unlock",true);
                StartCoroutine("StopAnimation");
                 Achievements[7].Unlocked = true;
                break;
            case 100:
                AchievementName.text = Achievements[8].Name;
                Achievement.GetComponent<Animator>().SetBool("unlock",true);
                StartCoroutine("StopAnimation");
                 Achievements[8].Unlocked = true;
                break;
            case 500:
                AchievementName.text = Achievements[9].Name;
                Achievement.GetComponent<Animator>().SetBool("unlock",true);
                StartCoroutine("StopAnimation");
                 Achievements[9].Unlocked = true;
                break;
            default:
                break;
        }
    }
    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(5);
        Achievement.GetComponent<Animator>().SetBool("unlock", false);

    }


#endif
}
