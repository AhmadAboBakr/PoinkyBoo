using UnityEngine;
using System.Collections;

public enum Mode
{
    MainMode, Spiral
}
public enum PowerUps
{
    Magnit,Sliding,Flying,None
}

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public string UUID;
    public bool isStarted=false;
    private bool isMoving = false;
    public Mode GameMode;
    public PowerUps Powerup;
    public float timeScale;
    public float gravity = Physics.gravity.y;
    float timer;

    public bool IsMoving
    {
        get { return isMoving; }
        set { 
            isMoving = value;
            if(value)
                Move();
        }
    }
    public delegate void emptyEventHandler();
    public static event emptyEventHandler Move;
    public static event emptyEventHandler clear;
    public Vector3 poinkySpeed= new Vector3(0,10,0);

    void Awake()
    {
        FB.Init(SetInit, OnHideUnity);  
        GameManager.instance = this;
        isStarted=false;
        isMoving = false;
        timeScale = Time.timeScale;
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("UID"))
        {
            PlayerPrefs.SetString("UUID", System.Guid.NewGuid().ToString());
        }
    }
    void Update()
    {
        if(Powerup==PowerUps.Magnit)
        {
            timer += Time.deltaTime;
        }
        if(timer>15)
        {
            Powerup = PowerUps.None;
            timer = 0;
        }
    }
    public void Clear()
    {
        this.GameMode = Mode.MainMode;
        clear();
    }
    private void SetInit()
    {
        Debug.Log("SetInit");
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
