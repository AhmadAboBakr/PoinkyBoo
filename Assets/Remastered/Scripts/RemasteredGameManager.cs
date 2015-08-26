using UnityEngine;
using System.Collections;
public enum GameState
{
    MainMenu,
    otherMenu,
    InGame,
    Paused,
    death
}
public delegate void emptyEventHandler();
public class RemasteredGameManager : MonoBehaviour {

    public static RemasteredGameManager instance;
    public static event emptyEventHandler Move;
    public static event emptyEventHandler reset;
    public static event emptyEventHandler pause;
    public static event emptyEventHandler resume;

    GameState currentState;
    private float senstivity;
    private float volume;

    public GameState CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            switch (value)
            {
                case GameState.MainMenu:
                    break;
                case GameState.otherMenu:
                    break;
                case GameState.InGame:
                    resume();
                    break;
                case GameState.Paused:
                    pause();
                    break;
                default:
                    break;
            }
            currentState = value;
            
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
            PlayerPrefs.SetFloat("senstivity", senstivity);
        }
    }
    public float Volume
    {
        get
        {
            return volume;
        }

        set
        {
            volume = value;
            PlayerPrefs.SetFloat("volume", volume);
        }
    }

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        CurrentState = GameState.MainMenu;
        #region initialize playerprefs
        if (!PlayerPrefs.HasKey("senstivity"))
        {
            PlayerPrefs.SetFloat("senstivity", 1);
        }
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1);
        }
        Senstivity = PlayerPrefs.GetFloat("senstivity");
        Volume = PlayerPrefs.GetFloat("volume");
        #endregion
    }
    void Start()
    {
    }
}
