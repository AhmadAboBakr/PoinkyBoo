using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public enum Mode
{
    MainMode, Spiral, Desert, Water 
}
public enum PowerUps
{
    Magnet, SafetyNet, Flying, None
}

public class GameManager : MonoBehaviour
{
    private float sound, senstivity;
    public static GameManager instance;
    public string UUID;
    public bool isStarted = false;
    private bool isMoving = false;
    public Mode GameMode;
    public PowerUps Powerup;
    InputMethod input;

    public float timeScale;
    public float gravity = Physics.gravity.y;
    float modeTimer;
    public string FacebookUserId;

    public int NumOfPoinky = 1;

    public bool IsMoving
    {
        get { return isMoving; }
        set
        {
            isMoving = value;
            if (value)
            {
                Move();
            }
        }
    }
    public float Senstivity
    {
        get
        {
            return senstivity;
        }
        set
        {
            senstivity = value;
            PlayerPrefs.SetFloat("Senstivity", value);
        }
    }
    public float Sound
    {
        get
        {
            return sound;
        }
        set
        {
            sound = value;
            PlayerPrefs.SetFloat("MusicVol", value);

        }
    }
    public InputMethod Input
    {
        get { return input; }
        set
        {
            input = value;
            PlayerPrefs.SetInt("InputMode", (int)value);
        }
    }
    private int coinsCollectedWithMagnet = 0;

    public int CoinsCollectedWithMagnet
    {
        get { return coinsCollectedWithMagnet; }
        set { 
            coinsCollectedWithMagnet = value; 
            if(value>10){
                //AchievementsHandler.instance.
                }
        }
    }
    public delegate void emptyEventHandler();
    public static event emptyEventHandler Move;
    public static event emptyEventHandler clear;
    public Vector3 poinkySpeed = new Vector3(0, 10, 0);

    void Awake()
    {
#if UNITY_ANDROID
        Debug.Log("Unity Editor");
        FB.Init(SetInit, OnHideUnity);
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = false;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
#endif
        
        Social.localUser.Authenticate((bool success) =>
        {

        });
        GameManager.instance = this;
        isStarted = false;
        isMoving = false;
        if (!PlayerPrefs.HasKey("v14"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetFloat("v14", 0);
            UUID = System.Guid.NewGuid().ToString();
            Senstivity = 1;
            Sound = .5f;
            PlayerPrefs.SetString("UUID", UUID);
            Input = InputMethod.buttons;
            PlayerPrefs.SetInt("BestScore", 0);
            PlayerPrefs.SetInt("Collectables", 0);
        }
        else
        {

        }
        timeScale = Time.timeScale;
        GameMode = Mode.MainMode;
    }
    void Start()
    {
    }
    void Update()
    {
        //game modes
        //if (modeTimer > 1)
        //{
        //    GameMode = Mode.Desert;
        //}
    }
    public void Clear()
    {
        this.GameMode = Mode.MainMode;
        clear();
    }
    private void SetInit()
    {
       // Debug.Log("SetInit");
        //enabled = true; // "enabled" is a property inherited from MonoBehaviour                  
        if (FB.IsLoggedIn)
        {
            Debug.Log("Already logged in");
            FacebookIntegration.instance.OnLoggedIn();
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        Debug.Log("OnHideUnity");
        //if (!isGameShown)
        //{
        //    // pause the game - we will need to hide  
        //    timeScale = Time.timeScale;                              
        //    Time.timeScale = 0;
        //}
        //else
        //{
        //    // start the game back up - we're getting focus again                                
        //    Time.timeScale =timeScale ;
        //}
    }    
}
