using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{
    public static MainMenu menu;
    public GameObject Options;
    public GameObject HUD;
    float timeScale;
    bool tutorialsflag;
    public GameObject tutorials;

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
        Tutorials.Manager.gameObject.SetActive(false);
        WinningScreen.screen.gameObject.SetActive(false);
        HUDManager.manager.gameObject.SetActive(false);
        OptionsMenu.menu.gameObject.SetActive(false);
        HUDManager.manager.gameObject.SetActive(false);
        PauseMenu.instance.gameObject.SetActive(false);
        // timeScale = Time.timeScale;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void BtnPlayPressed()
    {
        Time.timeScale = GameManager.instance.timeScale;
        this.gameObject.SetActive(false);
        HUD.SetActive(true);
        //if (!tutorialsflag)
        //{
        Tutorials.Manager.gameObject.SetActive(true);
        GameObject[] ARR = GameObject.FindGameObjectsWithTag("Tutorials");
        foreach (var item in ARR)
        {
            item.gameObject.SetActive(true);

        }
        Tutorials.Manager.tutorials();
        //tutorialsflag = true;
        //}
        //else
        //GameManager.manager.isStarted = true;
    }

    public void BtnOptionsPressed()
    {
        this.gameObject.SetActive(false);
        GameManager.instance.IsMoving = false;
        Options.SetActive(true);
    }


    public void BtnStorePressed()
    {
    }

    public void BtnMorePressed()
    {
        //FacebookIntegration.instance.Login();
    }
    


    public void BtnLeaderboardPressed()
    {
    }



    public void BtnQuitPressed()
    {
        Application.Quit();
    }
}
