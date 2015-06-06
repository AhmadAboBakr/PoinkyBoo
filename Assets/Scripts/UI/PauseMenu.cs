using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    public static PauseMenu instance;

	// Use this for initialization
    void Awake()
    {
        instance = this;
    }
    void Start () {

        if (HUDManager.instance.score > PlayerPrefs.GetInt("BestScore"))
        {
           // score.color = Color.red;
           // bestScore.color = Color.red;
            //score.text = HUDManager.instance.score.ToString();
           // bestScore.text = HUDManager.instance.score.ToString();
            PlayerPrefs.SetInt("BestScore", HUDManager.instance.score);
            // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
            Social.ReportScore(HUDManager.instance.score, "CgkInbf4694CEAIQAQ", (bool success) =>
            {
                // handle success or failure
            });

        }
        else
        {
            //score.color = startColor;
           // bestScore.color = startColor;
            //score.text = HUDManager.instance.score.ToString();
           // bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
        }
        PlayerPrefs.SetInt("Collectables", PlayerPrefs.GetInt("Collectables") + HUDManager.instance.collectables);
        AchievementsHandler.instance.ReportTotalCoins(HUDManager.instance.collectables);
        collectablesTotal.instance.Start(); 
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ContinuePlay();
        }
	}
    public void restart() 
    {
        collectablesTotal.instance.add(HUDManager.instance.collectables);
        if (HUDManager.instance.score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", HUDManager.instance.score);
            // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
            Social.ReportScore(HUDManager.instance.score, "CgkInbf4694CEAIQAQ", (bool success) =>
            {
                // handle success or failure
            });
        }

        //GameManager.Move -= TileGenerator.instance.Move;

        ////commented after pasting
        Time.timeScale = TileGenerator.instance.currentTimeScale;
       // Application.LoadLevel(Application.loadedLevel);
      //  HUDManager.instance.ispaused = false;

        /////////////////////////////////////////////////////pasted from winning screen
        RoomGenerator.generator.Clear();

        this.gameObject.SetActive(false);
        HUDManager.instance.Clear();
        MainMenu.menu.gameObject.SetActive(true);

        TileGenerator.instance.Clear();
        HUDManager.instance.ispaused = false;
        GameManager.instance.isStarted = false;
        Time.timeScale = 0;
    }
    public void ContinuePlay() 
    {
        Time.timeScale = TileGenerator.instance.currentTimeScale;
        gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        HUDManager.instance.gameObject.SetActive(true);
        HUDManager.instance.ispaused = false;
    }
    public void exit() 
    {
        Application.Quit();
    }
}
