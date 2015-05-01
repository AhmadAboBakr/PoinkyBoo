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
        GameManager.instance = this;
        isStarted=false;
        isMoving = false;
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
}
