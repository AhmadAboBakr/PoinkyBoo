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
        GameManager.Move -= TileGenerator.generator.Move;
        Time.timeScale = HUDManager.instance.currentTimeScale;
        Application.LoadLevel(Application.loadedLevel);
        HUDManager.instance.ispaused = false;

    }
    public void ContinuePlay() 
    {
        Time.timeScale = HUDManager.instance.currentTimeScale;
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
