using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorials : MonoBehaviour {
    public static Tutorials instance;
    public GameObject tabandhold;
    public GameObject swip;
    public GameObject tilt;
    public Text tabandholdText;
    public Text swipText;
    public Text tiltText;
    /// 
	// Use this for initialization
    void Awake()
    {
        Debug.Log(this.name);
        if (!Tutorials.instance)
            Tutorials.instance = this;

       
    }
    void Start()
    {
        
    }
	// Update is called once per frame
	void Update () {
         
	if(Input.GetMouseButtonDown(0))
    {
        //if (InputManager.method == InputMethod.Touch)
        //{
            GameManager.instance.isStarted = true;
            PowerUpGenerator.generator.Generate();
            if (GameManager.instance.Input == InputMethod.Touch)
            {
                swip.gameObject.SetActive(false);
                swipText.enabled = false;
            }
            else if (GameManager.instance.Input == InputMethod.Accelerometer)
            {
                tilt.gameObject.SetActive(false);
                tiltText.enabled = false;
            }
            else if (GameManager.instance.Input == InputMethod.buttons)
            {
                //tabandhold.gameObject.SetActive(false);
                //tabandholdText.enabled = false;

                //if (HUDManager.instance.score == 5)
                //{
                //    TutorialImages.instance.FadeOut();
                //}
            }
            this.gameObject.SetActive(false);
            foreach (var item in GameObject.FindGameObjectsWithTag("Tutorials"))
            {
                if (item)
                {
                    item.SetActive(false);
                }
            }

        //}
    }
	}
    public void tutorials()
    {
        //if (GameManager.manager.isStarted == true)
        //{
        if (GameManager.instance.Input == InputMethod.Touch)
            {
                tabandhold.gameObject.SetActive(false);
                tilt.gameObject.SetActive(false);
                tiltText.enabled = false;
                tabandholdText.enabled = false;
                swip.gameObject.SetActive(true);
                swipText.enabled = true;

            }
        else if (GameManager.instance.Input == InputMethod.Accelerometer)
            {
                tabandhold.gameObject.SetActive(false);
                swip.gameObject.SetActive(false);
                swipText.enabled = false;
                tabandholdText.enabled = false;
                tilt.gameObject.SetActive(true);
                tiltText.enabled = true;
            }
        else if (GameManager.instance.Input == InputMethod.buttons)
            {
                tilt.gameObject.SetActive(false);
                swip.gameObject.SetActive(false);
                tiltText.enabled = false;
                swipText.enabled = false;
                tabandhold.gameObject.SetActive(true);
                tabandholdText.enabled = true;
            }
        //}
    }
}
