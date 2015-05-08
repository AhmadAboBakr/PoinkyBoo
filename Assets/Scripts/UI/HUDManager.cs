using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour {

    public static HUDManager manager;
    public GameObject canvasMainMenu;
    public int score;
    public int collectables;
    public bool ispaused = false;
    public float currentTimeScale;
    float startTimeScale, EndTimeScale=2;
    Text scoreText;
    Text collectablesText;
    void Awake()
    {
        manager = this;
        GameManager.clear += Clear;
    }
    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        collectablesText = GameObject.FindGameObjectWithTag("Collectables").GetComponent<Text>();
        currentTimeScale= startTimeScale = Time.timeScale;

    }
    public void Clear()
    {
        score = 0;
        collectables=0;
        scoreText.text = "" + score;
        collectablesText.text = "" + collectables;
        GlossingScript.glosser.reset();
        currentTimeScale= Time.timeScale = startTimeScale;

    }

    public void increaseScore(int multiplier)
    {
        score += multiplier;
        scoreText.text = "" + score;
        if (score % 10 == 0)
        {
            GlossingScript.glosser.changeColor();
            Time.timeScale += .1f;
            if (Time.timeScale >= EndTimeScale) Time.timeScale = EndTimeScale;
            currentTimeScale = Time.timeScale;
        }
    }
    public void increaseCollectables()
    {
        collectables ++;
        collectablesText.text = "" + collectables;
    }
    public void pause() 
    {
        if (!ispaused)
        {
            Time.timeScale = 0;
            PauseMenu.instance.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            ispaused = true;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }
    }
}
