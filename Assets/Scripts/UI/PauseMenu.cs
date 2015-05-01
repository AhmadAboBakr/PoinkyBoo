using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    public static PauseMenu menu;

	// Use this for initialization
    void Awake()
    {
        menu = this;
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
        Time.timeScale = HUDManager.manager.currentTimeScale;
        Application.LoadLevel(Application.loadedLevel);
        HUDManager.manager.ispaused = false;
    }
    public void ContinuePlay() 
    {
        Time.timeScale = HUDManager.manager.currentTimeScale;
        gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        HUDManager.manager.gameObject.SetActive(true);
        HUDManager.manager.ispaused = false;
    }
    public void exit() 
    {
        Application.Quit();
    }
}
