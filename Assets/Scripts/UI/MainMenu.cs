using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SocialPlatforms;

public class MainMenu : MonoBehaviour
{
    public static MainMenu menu;
    public GameObject Options;
    public GameObject HUD;
    public GameObject credits;
    float timeScale;
    bool tutorialsflag;
    public GameObject tutorials;
    public GameObject Achievements;
    
     void Awake()
    {
        menu = this;
        tutorialsflag = false;
    }
    void OnEnable()
    {
        Time.timeScale = 1.6f;
    }
    void Start()
    {
        Tutorials.instance.gameObject.SetActive(false);
        WinningScreen.screen.gameObject.SetActive(false);
        HUDManager.instance.gameObject.SetActive(false);
        OptionsMenu.menu.gameObject.SetActive(false);
        HUDManager.instance.gameObject.SetActive(false);
        PauseMenu.instance.gameObject.SetActive(false);
        Store.instance.gameObject.SetActive(false);
        Achievements.SetActive(false);
        // authenticate user:for google play
        
         timeScale = Time.timeScale;
    }
   void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Achievements.gameObject.activeSelf)
            {
                Achievements.gameObject.SetActive(false);
            }
            else if (credits.gameObject.activeSelf)
            {
                credits.gameObject.SetActive(false);
            }
            else
            {
                Application.Quit();
            }
        }
    }
    public void BtnPlayPressed()
    {
        Time.timeScale = GameManager.instance.timeScale;
        this.gameObject.SetActive(false);
        HUD.SetActive(true);
        if (!tutorialsflag)
        {
            Tutorials.instance.gameObject.SetActive(true);
            Tutorials.instance.TapandHoldPanel.SetActive(true);
            GameObject[] ARR = GameObject.FindGameObjectsWithTag("Tutorials");
            foreach (var item in ARR)
            {
                item.gameObject.SetActive(true);
            }
            TutorialImages.instance.FadeReset();
            Tutorials.instance.tutorials();

            //tutorialsflag = true;
            //}
            //else
            GameManager.instance.isStarted = false;
        }
    }

    public void BtnOptionsPressed()
    {
        this.gameObject.SetActive(false);
        GameManager.instance.IsMoving = false;
        Options.SetActive(true);
    }


   public void BtnStorePressed()
    {
        
        this.gameObject.SetActive(false);
        GameManager.instance.IsMoving = false;
        Store.instance.gameObject.SetActive(true);
    }
    public void BtnMorePressed()
   {
#if UNITY_ANDROID
        Social.ShowAchievementsUI();
#else
       Achievements.SetActive(true);
        #endif
    }

    public void BtnLeaderboardPressed()
    {

        Social.localUser.Authenticate((bool success) =>
        {
           
        });
        //PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkInbf4694CEAIQAQ");
    }

    public void BtnQuitPressed()
    {
        Application.Quit();
    }
    public void BtnCredits()
    {
        
        credits.SetActive(true);
       
    }
}



    

    

