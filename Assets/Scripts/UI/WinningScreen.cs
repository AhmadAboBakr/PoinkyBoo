using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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
        if (!score)
        {
            score = GameObject.FindGameObjectWithTag("CurrentScore").GetComponent<Text>();   
            bestScore = GameObject.FindGameObjectWithTag("BestScore").GetComponent<Text>();
            startColor=score.color;
        }

        if (!GameJustStarted)
        {
            GameManager.instance.isStarted = false;
            
            RoomGenerator.generator.Clear();
            HUDManager.manager.gameObject.SetActive(false);

            GameManager.instance.isStarted = false;
            Time.timeScale = 0;
       
            if (HUDManager.manager.score> PlayerPrefs.GetInt("BestScore"))
            {
                score.color = Color.red;
                bestScore.color = Color.red;
                score.text = HUDManager.manager.score.ToString();
                bestScore.text = HUDManager.manager.score.ToString();
                PlayerPrefs.SetInt("BestScore", HUDManager.manager.score);
            }
            else
            {
                score.color = startColor;
                bestScore.color = startColor;
                score.text = HUDManager.manager.score.ToString();
                bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
            }
            PlayerPrefs.SetInt("Collectables",PlayerPrefs.GetInt("Collectables")+HUDManager.manager.collectables);
            collectablesTotal.instance.Start();
        }
        else
        {
            GameJustStarted = false;
        }

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
        GameManager.instance.isStarted = true;
        Time.timeScale = timeScale;
        GameManager.instance.Clear();
        this.gameObject.SetActive(false);
        HUDManager.manager.gameObject.SetActive(true);
        GameManager.instance.isStarted = true;
        //Dictionary<string, object> dict = new Dictionary<string, object>();
       // dict.Add("replay", true);
        //UnityAnalytics.CustomEvent("gameOver", dict);
        //GA.API.Design.NewEvent("Game:replay", 1);

        if (GameManager.instance.Input == InputMethod.buttons)
        {
            rightbutton.pressed = leftButton.pressed = false;
        }
    }   

    //back to main menu
    public void BtnBackPressed()
    {
        this.gameObject.SetActive(false);
        //HUD.SetActive(false);
        HUDManager.manager.Clear();
        MainMenu.menu.gameObject.SetActive(true);

        TileGenerator.generator.Clear();
       // Application.LoadLevel(Application.loadedLevel);
        GameManager.instance.isStarted = false;
        Time.timeScale = 0;
    }
    public void Share()
    {
        string link="http://games.senetStudios.com/Poinky";
        string pictureLink = "https://lh5.googleusercontent.com/b5VYY1rqU-yR5_LUPjNUXXy2FP5aXzltE1So8OHvzB3WjszCqzXO7qwiHdZVVyV7kVF67z0u=w1884-h717";
        string name="I'm Playing Poinky";
        string caption="a new High score";
        string description="just scored : "+HUDManager.manager.score;
        Debug.Log(HUDManager.manager.score);
        string redirectUri = "http://facebook.com/";
        ShareToFacebook(link,name,caption,description,pictureLink,redirectUri);
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
        dict.Add("score", HUDManager.manager.score);
        dict.Add("game", HUDManager.manager.score);
        dict.Add("Collectables",HUDManager.manager.collectables/(HUDManager.manager.score/3.0f));
        //UnityAnalytics.CustomEvent("gameOver", dict);
    //    GA.API.Design.NewEvent("Game:score", HUDManager.manager.score);
    //    GA.API.Design.NewEvent("Game:Collectables", HUDManager.manager.collectables / (HUDManager.manager.score / 3.0f));
    }
}
