using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class WinningScreen : MonoBehaviour
{
  
    public static WinningScreen screen;
    public ButtonsHandle rightbutton;
    public ButtonsHandle leftButton;
    float timeScale;
    bool GameJustStarted = true;
    Text score;
    Text bestScore;
    Color startColor;
    void Awake()
    {
        screen = this;
    }

	void Start () {
	}
    void OnEnable()
    {
        //PauseMenu.instance.gameObject.SetActive(false);

        if (!score)
        {
            score = GameObject.FindGameObjectWithTag("CurrentScore").GetComponent<Text>();   
            bestScore = GameObject.FindGameObjectWithTag("BestScore").GetComponent<Text>();
            startColor=score.color;
        }
        if (!GameJustStarted)
        {
            Debug.Log("lol");
            GameManager.instance.isStarted = false;
            
            HUDManager.instance.gameObject.SetActive(false);

            GameManager.instance.isStarted = false;
            Time.timeScale = 0;
       
            if (HUDManager.instance.score> PlayerPrefs.GetInt("BestScore"))
            {
                score.color = Color.red;
                bestScore.color = Color.red;
                score.text = HUDManager.instance.score.ToString();
                bestScore.text = HUDManager.instance.score.ToString();
                PlayerPrefs.SetInt("BestScore", HUDManager.instance.score);
                // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
                Social.ReportScore(HUDManager.instance.score, "CgkInbf4694CEAIQAQ", (bool success) =>
                {
                    // handle success or failure
                });

            }
            else
            {
                score.color = startColor;
                bestScore.color = startColor;
                score.text = HUDManager.instance.score.ToString();
                bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
            }
            PlayerPrefs.SetInt("Collectables",PlayerPrefs.GetInt("Collectables")+HUDManager.instance.collectables);
            AchievementsHandler.instance.ReportTotalCoins(HUDManager.instance.collectables);
            collectablesTotal.instance.Start(); 
        }
        else
        {
            GameJustStarted = false;
        }

    }

    //this should be.
    void saveScores()
    {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BtnBackPressed();
        }
	}

    public void BtnPlayPressed()
    {
        if (GameManager.instance.Input == InputMethod.buttons)
        {
            rightbutton.pressed = leftButton.pressed = false;
        }
        RoomGenerator.generator.Clear();
        Time.timeScale = timeScale;
        GameManager.instance.Clear();
        this.gameObject.SetActive(false);
        HUDManager.instance.gameObject.SetActive(true);
        //GameManager.instance.isStarted = true;
        //PowerUpGenerator.generator.Generate();
        //Dictionary<string, object> dict = new Dictionary<string, object>();
       // dict.Add("replay", true);
        //UnityAnalytics.CustomEvent("gameOver", dict);
        //GA.API.Design.NewEvent("Game:replay", 1);
        Tutorials.instance.gameObject.SetActive(true);
        Tutorials.instance.tutorialsPanel.SetActive(true);
        GameObject[] ARR = GameObject.FindGameObjectsWithTag("Tutorials");
        foreach (var item in ARR)
        {
            item.gameObject.SetActive(true);
        }
        Tutorials.instance.tutorials();
       

        //TileGenerator.instance.CurrentTile.Move(-1800);
    }   

    //back to main menu
    public void BtnBackPressed()
    {
        RoomGenerator.generator.Clear();

        this.gameObject.SetActive(false);
        //HUD.SetActive(false);
        HUDManager.instance.Clear();
        MainMenu.menu.gameObject.SetActive(true);

        TileGenerator.instance.Clear();
       // Application.LoadLevel(Application.loadedLevel);
        GameManager.instance.isStarted = false;
        Time.timeScale = 0;
    }
    public void Share()
    {
        FacebookIntegration.instance.Login();
        if (FB.IsLoggedIn)
        {
            FacebookIntegration.instance.Share(HUDManager.instance.score.ToString());
        }
        else
        {
            string link = "https://play.google.com/store/apps/details?id=com.ITI.poinky";
            string pictureLink = "http://i.imgur.com/WD7nqDE.png";
            string name = "I'm Playing Poinky";
            string caption = "a new High score";
            string description = "just scored : " + HUDManager.instance.score;
            Debug.Log(HUDManager.instance.score);
            string redirectUri = "http://facebook.com/";
            ShareToFacebook(link, name, caption, description, pictureLink, redirectUri);

        }
    }
    void ShareToFacebook(string linkParameter, string nameParameter, string captionParameter, string descriptionParameter, string pictureParameter, string redirectParameter)
    {

        string AppId = "387389954782154";
        string ShareUrl = "http://www.facebook.com/dialog/feed";
        Application.OpenURL(ShareUrl + "?app_id=" + AppId +
        "&link=" + WWW.EscapeURL(linkParameter) +
        "&name=" + WWW.EscapeURL(nameParameter) +
        "&caption=" + WWW.EscapeURL(captionParameter) +
        "&description=" + WWW.EscapeURL(descriptionParameter) +
        "&picture=" + WWW.EscapeURL(pictureParameter) +
        "&redirect_uri=" + WWW.EscapeURL(redirectParameter));
    }
    void sendAnalyticsData()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict.Add("score", HUDManager.instance.score);
        dict.Add("game", HUDManager.instance.score);
        dict.Add("Collectables",HUDManager.instance.collectables/(HUDManager.instance.score/3.0f));
        //UnityAnalytics.CustomEvent("gameOver", dict);
    //    GA.API.Design.NewEvent("Game:score", HUDManager.manager.score);
    //    GA.API.Design.NewEvent("Game:Collectables", HUDManager.manager.collectables / (HUDManager.manager.score / 3.0f));
    }
}
