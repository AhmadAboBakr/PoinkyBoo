using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour {

    public static HUDManager instance;
    public GameObject canvasMainMenu;
	public Animator myAnimator;
    public int score;
    public int collectables;
    public bool ispaused = false;
    float startTimeScale, EndTimeScale=2;
    Text scoreText;
    void Awake()
    {
        instance = this;
        GameManager.clear += Clear;
    }
    void Start()
    {
		myAnimator = this.GetComponent<Animator> ();
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
    }
    public void Clear()
    {
        score = 0;
        collectables=0;
		myAnimator.SetTrigger ("restart");
		scoreText.text = "" + score;
        GlossingScript.glosser.reset();

    }

    public void increaseScore(int multiplier)
    {
        score += multiplier;
        scoreText.text = "" + score;
 
    }
    public void increaseCollectables()
    {
        collectables ++;
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
       // scoreText.text = PlayerPrefs.GetInt("Collectables").ToString();
    }
}
